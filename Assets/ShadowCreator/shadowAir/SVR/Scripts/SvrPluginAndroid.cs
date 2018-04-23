using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

class SvrPluginAndroid : SvrPlugin
{
	[DllImport("svrplugin")]
	private static extern IntPtr GetRenderEventFunc();

    [DllImport("svrplugin")]
    private static extern bool SvrIsInitialized();

    [DllImport("svrplugin")]
    private static extern bool SvrIsRunning();

    [DllImport("svrplugin")]
    private static extern bool SvrCanBeginVR();

    [DllImport("svrplugin")]
	private static extern void SvrInitializeEventData(IntPtr activity);

	[DllImport("svrplugin")]
	private static extern void SvrSubmitFrameEventData(int frameIndex, float fieldOfView, int frameType);

    [DllImport("svrplugin")]
    private static extern void SvrSetupLayerCoords(int typeEyeOrOverlay, int layerIndex, float[] lowerLeft, float[] lowerRight, float[] upperLeft, float[] upperRight);
    [DllImport("svrplugin")]
    private static extern void SvrSetupLayerData(int typeEyeOrOverlay, int layerIndex, int sideMask, int textureId, int textureType);

    [DllImport("svrplugin")]
	private static extern void SvrSetTrackingModeEventData(int mode);

	[DllImport("svrplugin")]
	private static extern void SvrSetPerformanceLevelsEventData(int newCpuPerfLevel,
	                                                            int newGpuPerfLevel);

    [DllImport("svrplugin")]
    private static extern void SvrSetFrameOption(uint frameOption);

    [DllImport("svrplugin")]
    private static extern void SvrUnsetFrameOption(uint frameOption);

    [DllImport("svrplugin")]
    private static extern void SvrSetVSyncCount(int vSyncCount);

    [DllImport("svrplugin")]
	private static extern int SvrGetPredictedPose(ref float rx,
	                                               ref float ry,
	                                               ref float rz,
	                                               ref float rw,
												   ref float px,
												   ref float py,
												   ref float pz,
                                                   int frameIndex);

    [DllImport("svrplugin")]
    private static extern bool SvrRecenterTrackingPose();

    [DllImport("svrplugin")]
    private static extern int SvrGetTrackingMode();

    [DllImport("svrplugin")]
    private static extern void SvrGetDeviceInfo(ref int displayWidthPixels,
	                                            ref int displayHeightPixels,
	                                            ref float displayRefreshRateHz,
	                                            ref int targetEyeWidthPixels,
	                                            ref int targetEyeHeightPixels,
	                                            ref float targetFovXRad,
	                                       		ref float targetFovYRad,
                                                ref float leftFrustumLeft, ref float leftFrustumRight, ref float leftFrustumBottom, ref float leftFrustumTop, ref float leftFrustumNear, ref float leftEyeFrustumFar,
                                                ref float rightFrustumLeft, ref float rightFrustumRight, ref float rightFrustumBottom, ref float rightFrustumTop, ref float rightFrustumNear, ref float rightFrustumFar);

	[DllImport("svrplugin")]
	private static extern void SvrSetFoveationParameters(float focalPointX, float focalPointY, float foveationGainX, float foveationGainY, float foveationArea);

    [DllImport("svrplugin")]
    private static extern bool SvrPollEvent(ref int eventType, ref uint deviceId, ref float eventTimeStamp, int eventDataCount, ref uint eventData);
    
	//---------------------------------------------------------------------------------------------
	// Conotroller Apis
	//---------------------------------------------------------------------------------------------
    [DllImport("svrplugin")]
    private static extern int SvrControllerStartTracking(string desc);
    
    [DllImport("svrplugin")]
    private static extern void SvrControllerStopTracking(int handle);
    
    [DllImport("svrplugin")]
	private static extern void SvrControllerGetState(int handle, ref SvrControllerState state);

	[DllImport("svrplugin")]
	private static extern void SvrControllerSendMessage (int handle, int what, int arg1, int arg2);

	[DllImport("svrplugin")]
	private static extern int SvrControllerQuery (int handle, int what, IntPtr mem, int size);
	//---------------------------------------------------------------------------------------------

    private enum RenderEvent
	{
		Initialize,
		BeginVr,
		EndVr,
		BeginEye,
		EndEye,
		SubmitFrame,
		Shutdown,
		RecenterTracking,
		SetTrackingMode,
		SetPerformanceLevels
	};

	public static SvrPluginAndroid Create()
	{
		if(Application.isEditor)
		{
			Debug.LogError("SvrPlugin not supported in unity editor!");
			throw new InvalidOperationException();
		}

		return new SvrPluginAndroid ();
	}


	private SvrPluginAndroid() {}

	private void IssueEvent(RenderEvent e)
	{
		// Queue a specific callback to be called on the render thread
		GL.IssuePluginEvent(GetRenderEventFunc(), (int)e);
	}

    public override bool IsInitialized() { return SvrIsInitialized(); }

    public override bool IsRunning() { return SvrIsRunning(); }

    public override IEnumerator Initialize()
	{
        //yield return new WaitUntil(() => SvrIsInitialized() == false);  // Wait for shutdown

        yield return base.Initialize();

#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

		SvrInitializeEventData(activity.GetRawObject());
#endif
        IssueEvent(RenderEvent.Initialize);
		yield return new WaitUntil (() => SvrIsInitialized () == true);

        yield return null;  // delay one frame - fix for re-init w multi-threaded rendering

        deviceInfo = GetDeviceInfo();
    }

	public override IEnumerator BeginVr(int cpuPerfLevel, int gpuPerfLevel)
	{
        //yield return new WaitUntil(() => SvrIsRunning() == false);  // Wait for EndVr

        yield return base.BeginVr(cpuPerfLevel, gpuPerfLevel);

        // float[6]: x, y, z, w, u, v
        float[] lowerLeft = { -1f, -1f, 0f, 1f, 0f, 0f };
        float[] upperLeft = { -1f,  1f, 0f, 1f, 0f, 1f };
        float[] upperRight = { 1f,  1f, 0f, 1f, 1f, 1f };
        float[] lowerRight = { 1f, -1f, 0f, 1f, 1f, 0f };
        SvrSetupLayerCoords(0, -1, lowerLeft, lowerRight, upperLeft, upperRight);    // Eye/All
        SvrSetupLayerCoords(1, -1, lowerLeft, lowerRight, upperLeft, upperRight);    // Overlay/All

        SvrSetPerformanceLevelsEventData(cpuPerfLevel, gpuPerfLevel);

        yield return new WaitUntil(() => SvrCanBeginVR() == true);
        IssueEvent (RenderEvent.BeginVr);
        yield return new WaitUntil(() => SvrIsRunning() == true);
    }

    public override void EndVr()
	{
        base.EndVr();

		IssueEvent (RenderEvent.EndVr);
	}

	public override void BeginEye()
	{
		IssueEvent (RenderEvent.BeginEye);
	}

	public override void EndEye()
	{
		IssueEvent (RenderEvent.EndEye);
	}

    public override void SetTrackingMode(TrackingMode mode)
    {
        SvrSetTrackingModeEventData((int)mode);
		IssueEvent (RenderEvent.SetTrackingMode);
    }

	public override void SetFoveationParameters(float focalPointX, float focalPointY, float foveationGainX, float foveationGainY, float foveationArea)
	{
		SvrSetFoveationParameters(focalPointX, focalPointY, foveationGainX, foveationGainY, foveationArea);
	}

    public override int GetTrackingMode()
    {
        return SvrGetTrackingMode();
    }

    public override void SetPerformanceLevels(int newCpuPerfLevel, int newGpuPerfLevel)
    {
        SvrSetPerformanceLevelsEventData((int)newCpuPerfLevel, (int)newGpuPerfLevel);
		IssueEvent (RenderEvent.SetPerformanceLevels);
    }

    public override void SetFrameOption(FrameOption frameOption)
    {
        SvrSetFrameOption((uint)frameOption);
    }

    public override void UnsetFrameOption(FrameOption frameOption)
    {
        SvrUnsetFrameOption((uint)frameOption);
    }

    public override void SetVSyncCount(int vSyncCount)
    {
        SvrSetVSyncCount(vSyncCount);
    }

    public override bool RecenterTracking()
	{
        //IssueEvent (RenderEvent.RecenterTracking);
        return SvrRecenterTrackingPose();
	}

	public override int GetPredictedPose(ref Quaternion orientation, ref Vector3 position, int frameIndex)
	{
        orientation.z = -orientation.z;
        position.x = -position.x;
        position.y = -position.y;

        int rv = SvrGetPredictedPose(ref orientation.x, ref orientation.y, ref orientation.z, ref orientation.w,
							ref position.x, ref position.y, ref position.z, frameIndex);

		orientation.z = -orientation.z;
        position.x = -position.x;
        position.y = -position.y;

        return rv;
	}

	public override DeviceInfo GetDeviceInfo()
	{
		DeviceInfo info = new DeviceInfo();

		SvrGetDeviceInfo (ref info.displayWidthPixels,
		                  ref info.displayHeightPixels,
		                  ref info.displayRefreshRateHz,
		                  ref info.targetEyeWidthPixels,
		                  ref info.targetEyeHeightPixels,
		                  ref info.targetFovXRad,
		                  ref info.targetFovYRad,
                          ref info.targetFrustumLeft.left, ref info.targetFrustumLeft.right, ref info.targetFrustumLeft.bottom, ref info.targetFrustumLeft.top, ref info.targetFrustumLeft.near, ref info.targetFrustumLeft.far,
                          ref info.targetFrustumRight.left, ref info.targetFrustumRight.right, ref info.targetFrustumRight.bottom, ref info.targetFrustumRight.top, ref info.targetFrustumRight.near, ref info.targetFrustumRight.far);

		return info;
	}

    public override void SubmitFrame(int frameIndex, float fieldOfView, int frameType)
	{
        int i;
        int eyeCount = 0;
        if (eyes != null) for (i = 0; i < eyes.Length; i++)
        {
            var eye = eyes[i];
            if (eyes[i].isActiveAndEnabled == false || eye.TextureId == 0 || eye.Side == 0) continue;
            if (eye.imageTransform != null && eye.imageTransform.gameObject.activeSelf == false) continue;
            SvrSetupLayerData(0, eyeCount, (int)eye.Side, eye.TextureId, eye.ImageType == SvrEye.eType.EglTexture ? 1 : 0);
            float[] lowerLeft = { eye.clipLowerLeft.x, eye.clipLowerLeft.y, eye.clipLowerLeft.z, eye.clipLowerLeft.w, eye.uvLowerLeft.x, eye.uvLowerLeft.y };
            float[] upperLeft = { eye.clipUpperLeft.x, eye.clipUpperLeft.y, eye.clipUpperLeft.z, eye.clipUpperLeft.w, eye.uvUpperLeft.x, eye.uvUpperLeft.y };
            float[] upperRight = { eye.clipUpperRight.x, eye.clipUpperRight.y, eye.clipUpperRight.z, eye.clipUpperRight.w, eye.uvUpperRight.x, eye.uvUpperRight.y };
            float[] lowerRight = { eye.clipLowerRight.x, eye.clipLowerRight.y, eye.clipLowerRight.z, eye.clipLowerRight.w, eye.uvLowerRight.x, eye.uvLowerRight.y };
            SvrSetupLayerCoords(0, eyeCount, lowerLeft, lowerRight, upperLeft, upperRight);
            eyeCount++;
        }
        for (i = eyeCount; i < SvrManager.EyeLayerMax; i++)
        {
            SvrSetupLayerData(0, i, 0, 0, 0);
        }

        int overlayCount = 0;
        if (overlays != null) for (i = 0; i < overlays.Length; i++)
        {
            var overlay = overlays[i];
            if (overlay.isActiveAndEnabled == false || overlay.TextureId == 0 || overlay.Side == 0) continue;
            if (overlay.imageTransform != null && overlay.imageTransform.gameObject.activeSelf == false) continue;
            SvrSetupLayerData(1, overlayCount, (int)overlay.Side, overlay.TextureId, overlay.ImageType == SvrOverlay.eType.EglTexture ? 1 : 0);
            float[] lowerLeft = { overlay.clipLowerLeft.x, overlay.clipLowerLeft.y, overlay.clipLowerLeft.z, overlay.clipLowerLeft.w, overlay.uvLowerLeft.x, overlay.uvLowerLeft.y };
            float[] upperLeft = { overlay.clipUpperLeft.x, overlay.clipUpperLeft.y, overlay.clipUpperLeft.z, overlay.clipUpperLeft.w, overlay.uvUpperLeft.x, overlay.uvUpperLeft.y };
            float[] upperRight = { overlay.clipUpperRight.x, overlay.clipUpperRight.y, overlay.clipUpperRight.z, overlay.clipUpperRight.w, overlay.uvUpperRight.x, overlay.uvUpperRight.y };
            float[] lowerRight = { overlay.clipLowerRight.x, overlay.clipLowerRight.y, overlay.clipLowerRight.z, overlay.clipLowerRight.w, overlay.uvLowerRight.x, overlay.uvLowerRight.y };
            SvrSetupLayerCoords(1, overlayCount, lowerLeft, lowerRight, upperLeft, upperRight); overlayCount++;
        }
        for (i = overlayCount; i < SvrManager.OverlayLayerMax; i++)
        {
            SvrSetupLayerData(1, i, 0, 0, 0);
        }

        SvrSubmitFrameEventData(frameIndex, fieldOfView, frameType);
		IssueEvent (RenderEvent.SubmitFrame);
	}

	public override void Shutdown()
	{
        IssueEvent (RenderEvent.Shutdown);

        base.Shutdown();
	}

    public override bool PollEvent(ref SvrManager.SvrEvent frameEvent)
    {
        int dataCount = Marshal.SizeOf(frameEvent.eventData) / sizeof(uint);
		int eventType = 0;
        bool isEvent = SvrPollEvent(ref eventType, ref frameEvent.deviceId, ref frameEvent.eventTimeStamp, dataCount, ref frameEvent.eventData);
		frameEvent.eventType = (SvrManager.svrEventType)(eventType);
        return isEvent;
    }

	//---------------------------------------------------------------------------------------------
	//Controller Apis
	//---------------------------------------------------------------------------------------------

	/// <summary>
	/// Controllers the start tracking.
	/// </summary>
	/// <returns>The start tracking.</returns>
	/// <param name="desc">Desc.</param>
	//---------------------------------------------------------------------------------------------
    public override int ControllerStartTracking(string desc)
    {
        return SvrControllerStartTracking(desc);
    }
    
	/// <summary>
	/// Controllers the stop tracking.
	/// </summary>
	/// <param name="handle">Handle.</param>
	//---------------------------------------------------------------------------------------------
	public override void ControllerStopTracking(int handle)
    {
        SvrControllerStopTracking(handle);
    }

	/// <summary>
	/// Dumps the state.
	/// </summary>
	/// <param name="state">State.</param>
	//---------------------------------------------------------------------------------------------
	private void dumpState(SvrControllerState state)
	{
		String s = "{" + state.rotation + "}\n";
		s += "[" + state.position + "]\n";
		s += "<" + state.timestamp + ">\n";

		Debug.Log (s);
	}
    
	/// <summary>
	/// Controllers the state of the get.
	/// </summary>
	/// <returns>The get state.</returns>
	/// <param name="handle">Handle.</param>
	//---------------------------------------------------------------------------------------------
	public override SvrControllerState ControllerGetState(int handle)
    {
		SvrControllerState state = new SvrControllerState();
		SvrControllerGetState (handle, ref state);
		//dumpState (state);
 		return state;
    }

	/// <summary>
	/// Controllers the send event.
	/// </summary>
	/// <param name="handle">Handle.</param>
	/// <param name="what">What.</param>
	/// <param name="arg1">Arg1.</param>
	/// <param name="arg2">Arg2.</param>
	//---------------------------------------------------------------------------------------------
	public override void ControllerSendMessage(int handle, SvrController.svrControllerMessageType what, int arg1, int arg2)
	{
		SvrControllerSendMessage (handle, (int)what, arg1, arg2);
	}

	/// <summary>
	/// Controllers the query.
	/// </summary>
	/// <returns>The query.</returns>
	/// <param name="handle">Handle.</param>
	/// <param name="what">What.</param>
	/// <param name="mem">Mem.</param>
	/// <param name="size">Size.</param>
	//---------------------------------------------------------------------------------------------
	public override object ControllerQuery(int handle, SvrController.svrControllerQueryType what)
	{
		int memorySize = 0;
		IntPtr memory = IntPtr.Zero;
		object result = null;

		System.Type typeOfObject = null;

		switch(what)
		{
			case SvrController.svrControllerQueryType.kControllerBatteryRemaining:
				{
					typeOfObject = typeof(int);
					memorySize = System.Runtime.InteropServices.Marshal.SizeOf (typeOfObject);
					memory = System.Runtime.InteropServices.Marshal.AllocHGlobal (memorySize);	
	}
				break;
}

		int writtenBytes = SvrControllerQuery (handle, (int)what, memory, memorySize);

		if (memorySize == writtenBytes) {
			result = System.Runtime.InteropServices.Marshal.PtrToStructure (memory, typeOfObject);
		}
			
		if (memory != IntPtr.Zero) {
			Marshal.FreeHGlobal (memory);
		}
			
		return result;
	}
}
