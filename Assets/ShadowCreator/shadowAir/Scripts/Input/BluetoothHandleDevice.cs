using UnityEngine;
using System.Collections;
using System;
using ShadowKit;

namespace ShadowKit.Air
{
	public class BluetoothHandleDevice{

		public static BluetoothHandleDevice _instance;
		public static BluetoothHandleDevice Instance {
			get {
				if (_instance == null) {
					_instance = new BluetoothHandleDevice ();
				}
				return _instance;
			}
		}

		private bool _enable3Dof = false;
		private GameObject target;
		private int curDeviceId = -1;
//		private bool _enableAcc = false;

		private static readonly  Matrix4x4 FLIP_Z = Matrix4x4.Scale(new Vector3(1, 1, -1));
		Matrix4x4 mPoseMatrix1;
		Matrix4x4 mPoseMatrix2;
		public void init()
		{
			#if UNITY_ANDROID &&!UNITY_EDITOR
			AndroidConnection.Instance.addListener<HandShankConnStateListener> ("setHandShankConnStateCallback", new HandShankConnStateListener());
			AndroidConnection.Instance.addListener<HandShankKeyEventListener> ("setHandShankKeyEventCallback", new HandShankKeyEventListener (begin, end));
			onApplicationFocus(true);
			ShadowSystem.onApplicationFocusEvent += onApplicationFocus;
			InputSystem.OnUpdateEvent += update;
			#endif
		}

//		public void enableAcc(bool value)
//		{
//			_enableAcc = value;
//			if(!value)
//			{
//				AirInput.BluetoothHandleAcc = new Vector3 (0, 0, 0);
//			}
//		}

		public void enable3Dof(bool value)
		{
			_enable3Dof = value;
			if(!value)
			{
				AirInput.BluetoothHandleAngles2 = new Vector3 (0, 0, 0);
				AirInput.BluetoothHandleRotation2 = new Quaternion (0, 0, 0, 0);
				AirInput.BluetoothHandleAngles1 = new Vector3 (0, 0, 0);
				AirInput.BluetoothHandleRotation1 = new Quaternion (0, 0, 0, 0);
			}
		}

		/// <summary>
		/// 唤醒代码
		/// </summary>
		/// <param name="hasFocus">If set to <c>true</c> has focus.</param>
		void onApplicationFocus(bool hasFocus)
		{
			if (hasFocus) {
				int BluetoothConnect = AndroidConnection.Instance.Call<int> ("unityBTConnected",0);
				if (BluetoothConnect == 0) {
					AirInput.unConnected (0);
				} else {
					AirInput.connected (0);
				}

				BluetoothConnect = AndroidConnection.Instance.Call<int> ("unityBTConnected",1);
				if (BluetoothConnect == 0) {
					AirInput.unConnected (1);
				} else {
					AirInput.connected (1);
				}
			}
		}
			
		void update()
		{
			if (Input.GetKeyDown(KeyCode.Escape)) {
				if (!AirInput.escapeDown()) {
					ShadowSystem.Quit ();
				}
			}
				
			if (target != null) {
				//持续按鼠标
				//				SCInput.MousePosition = getMousePosition();
				drag ();
				return;
			}
			get3Dof ();

//			if (_enableAcc) {
//				float[] array = AndroidConnection.Instance.Call<float[]> ("unityACCMatrix");
//				if (array == null) {
//					return;
//				}
//
//				AirInput.BluetoothHandleAcc = new Vector3 (array [0], array [1], array [2]);
//				Debug.Log ("wjh.BluetoothHandleAcc====>" + AirInput.BluetoothHandleAcc.ToString ());
//			}
		}

		void begin(int deviceId)
		{
			if (SCInput.Instance.target != null && target == null && curDeviceId == -1) {
				target = SCInput.Instance.target;
				SCInput.Instance.PointDown (target);
				deviceId = deviceId;
			}
			anyKeyDown ();
		}

		void drag()
		{
			if (target == null) {
				return;
			}
			SCInput.Instance.Drag (target);
		}

		void end(int deviceId)
		{
			if (deviceId != curDeviceId) {
				return;
			}
			SCInput.Instance.PointUp (target);
			target = null;
			deviceId = -1;
		}

		//		Vector3 getMousePosition()
		//		{
		//			Ray ray = ShadowSystem.Camera.ScreenPointToRay (Input.mousePosition);
		//			Vector3 res;
		//			res = ray.GetPoint(100.0f);
		//			return res;
		//		}

		void get3Dof()
		{
			if (_enable3Dof) {
				float[] array = AndroidConnection.Instance.Call<float[]> ("unity3DofMatrix", 0);
				if (array == null) {
					Debug.Log ("wjh.unity3Dof2Matrix=====>null");
					return;
				} else {
					for (int index = 0; index < 16; index++) {
						mPoseMatrix1 [index] = array [index];
						//					mPoseMatrix2[index] = array[index + 16];
					}

					mPoseMatrix1 = FLIP_Z * mPoseMatrix1.inverse * FLIP_Z;
					AirInput.BluetoothHandleRotation1 = Quaternion.LookRotation (mPoseMatrix1.GetColumn (2), mPoseMatrix1.GetColumn (1));
					AirInput.BluetoothHandleAngles1 = AirInput.BluetoothHandleRotation1.eulerAngles;
				}
					
				float[] array1 = AndroidConnection.Instance.Call<float[]> ("unity3DofMatrix", 1);
				if (array1 == null) {
					Debug.Log ("wjh.unity3Dof2Matrix=====>null");
					return;
				} else {
					for (int index = 0; index < 16; index++) {
						mPoseMatrix2[index] = array1[index];
					}

					mPoseMatrix2 = FLIP_Z * mPoseMatrix2.inverse * FLIP_Z;
					AirInput.BluetoothHandleRotation2 = Quaternion.LookRotation (mPoseMatrix2.GetColumn (2), mPoseMatrix2.GetColumn (1));
					AirInput.BluetoothHandleAngles2 = AirInput.BluetoothHandleRotation2.eulerAngles;
				}


				
			}
		}

		void anyKeyDown()
		{
			SCInput.Instance.anyKeyDown ();
		}
	}
}

