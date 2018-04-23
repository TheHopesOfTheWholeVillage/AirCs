using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShadowKit{
	public class AndroidConnection {
		public AndroidJavaObject mAndroidActivity;

		public AndroidConnection()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			mAndroidActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
			#endif
		}

		public void addListener<T>(string listenerName, AndroidJavaProxy listener)  where T:AndroidJavaProxy
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			mAndroidActivity.Call (listenerName , (T)listener);
			#endif
		}

		public void Call(string callName, params object[] args)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			mAndroidActivity.Call (callName, args);
			#endif
		}

		public ReturnType Call<ReturnType> (string callName, params object[] args)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			return mAndroidActivity.Call<ReturnType> (callName, args);
			#endif
			return default(ReturnType);
		}

		private static AndroidConnection _instance;
		public static AndroidConnection Instance { get {
				if (_instance == null) {
					_instance = new AndroidConnection ();
				}
				return _instance;
			}
		}
	}
}

