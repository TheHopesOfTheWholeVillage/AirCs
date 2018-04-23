using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

abstract class SvrPlugin
{
	private static SvrPlugin instance;

	public static SvrPlugin Instance
	{
		get
		{
			if (instance == null)
			{
				if(!Application.isEditor && Application.platform == RuntimePlatform.Android)
				{
					instance = SvrPluginAndroid.Create();
				}
				else
				{
					instance = SvrPluginWin.Create();
				}
			}
			return instance;
		}
	}

    public SvrManager svrCamera = null;
    public SvrEye[] eyes = null;
    public SvrOverlay[] overlays = null;
    public DeviceInfo deviceInfo;

    public enum PerfLevel
	{
        kPerfSystem = 0,
        kPerfMaximum = 1,
		kPerfNormal = 2,
		kPerfMinimum = 3
	}

    public enum TrackingMode
    {
        kTrackingOrientation = 1,
        kTrackingPosition = 2
    }

    public enum FrameOption
    {
        kDisableDistortionCorrection = (1 << 0),    //!< Disables the lens distortion correction (useful for debugging)
        kDisableReprojection = (1 << 1),            //!< Disables re-projection
        kEnableMotionToPhoton = (1 << 2),           //!< Enables motion to photon testing 
        kDisableChromaticCorrection = (1 << 3)      //!< Disables the lens chromatic aberration correction (performance optimization)
    };

    public struct ViewFrustum
    {
        public float left;           //!< Left Plane of Frustum
        public float right;          //!< Right Plane of Frustum
        public float top;            //!< Top Plane of Frustum
        public float bottom;         //!< Bottom Plane of Frustum

        public float near;           //!< Near Plane of Frustum
        public float far;            //!< Far Plane of Frustum (Arbitrary)
    }

    public struct DeviceInfo
	{
		public int 		displayWidthPixels;
		public int    	displayHeightPixels;
		public float  	displayRefreshRateHz;
		public int    	targetEyeWidthPixels;
		public int    	targetEyeHeightPixels;
		public float  	targetFovXRad;
		public float  	targetFovYRad;
        public ViewFrustum targetFrustumLeft;
        public ViewFrustum targetFrustumRight;

	}

    public enum eEventType
    {
        kEventNone = 0,
        kEventSdkServiceStarting = 1,
        kEventSdkServiceStarted = 2,
        kEventSdkServiceStopped = 3,
        kEventControllerConnecting = 4,
        kEventControllerConnected = 5,
        kEventControllerDisconnected = 6,
        kEventThermal = 7,
        kEventSensorError
    };

    public virtual bool PollEvent(ref SvrManager.SvrEvent frameEvent) { return false; }

    public virtual bool IsInitialized() { return false; }
    public virtual bool IsRunning() { return false; }
    public virtual IEnumerator Initialize ()
    {
        svrCamera = SvrManager.Instance;
        if (svrCamera == null)
        {
            Debug.LogError("SvrManager object not found!");
            yield break;
        }

        yield break;
    }
	public virtual IEnumerator BeginVr(int cpuPerfLevel =0, int gpuPerfLevel =0)
    {
        if (eyes == null)
        {
            eyes = SvrEye.Instances.ToArray();
            if (eyes == null)
            {
                Debug.Log("Components with SvrEye not found!");
            }

            Array.Sort(eyes);
        }

        if (overlays == null)
        {
            overlays = SvrOverlay.Instances.ToArray();
            if (overlays == null)
            {
                Debug.Log("Components with SvrOverlay not found!");
            }

            Array.Sort(overlays);
        }

        yield break;
    }
    public virtual void EndVr()
    {
        eyes = null;
        overlays = null;
    }
	public virtual void BeginEye() { }
    public virtual void EndEye() { }
    public virtual void SetTrackingMode(TrackingMode mode) { }
	public virtual void SetFoveationParameters(float focalPointX, float focalPointY, float foveationGainX, float foveationGainY, float foveationArea) {}
    public virtual int GetTrackingMode() { return 0; }
    public virtual void SetPerformanceLevels(int newCpuPerfLevel, int newGpuPerfLevel) { }
    public virtual void SetFrameOption(FrameOption frameOption) { }
    public virtual void UnsetFrameOption(FrameOption frameOption) { }
    public virtual void SetVSyncCount(int vSyncCount) { }
    public virtual bool RecenterTracking() { return true; }
    public virtual void SubmitFrame(int frameIndex, float fieldOfView, int frameType) { }
	public virtual int GetPredictedPose (ref Quaternion orientation, ref Vector3 position, int frameIndex = -1)
	{
		orientation =  Quaternion.identity;
		position = Vector3.zero;
        return 0;
	}
	public abstract DeviceInfo GetDeviceInfo ();
	public virtual void Shutdown()
    {
        SvrPlugin.instance = null;
    }

	//---------------------------------------------------------------------------------------------
	public virtual int ControllerStartTracking(string desc) {
		return -1;
	}

	//---------------------------------------------------------------------------------------------
	public virtual void ControllerStopTracking(int handle) {
	}

	//---------------------------------------------------------------------------------------------
	public virtual SvrControllerState ControllerGetState(int handle) {
		return new SvrControllerState();
	}

	//---------------------------------------------------------------------------------------------
	public virtual void ControllerSendMessage(int handle, SvrController.svrControllerMessageType what, int arg1, int arg2) {
	}

	//---------------------------------------------------------------------------------------------
	public virtual object ControllerQuery(int handle, SvrController.svrControllerQueryType what) {
		return null;
	}
}

