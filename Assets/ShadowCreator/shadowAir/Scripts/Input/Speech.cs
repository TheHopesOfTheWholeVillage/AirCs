using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShadowKit.Air
{
	public class Speech : MonoBehaviour {

		public static Speech _instance;
		public static Speech Instance {
			get {
				if (_instance == null) {
					_instance = new Speech ();
				}
				return _instance;
			}
		}

		/// <summary>
		/// 影创科技的唤醒
		/// </summary>
		public delegate void OnSpeechWakeup(string msg);
		public static event OnSpeechWakeup OnSpeechWakeupEvent;

		/// <summary>
		/// 语音回调
		/// </summary>
		public delegate void OnSpeechResult(int eventCode);
		public static event OnSpeechResult OnSpeechResultEvent;

		public bool Enabled {
			set {
				#if UNITY_ANDROID &&!UNITY_EDITOR
				AndroidConnection.Instance.Call ("speechEnable", value);
				#endif
			}
		}


		public void init()
		{
			#if UNITY_ANDROID &&!UNITY_EDITOR
			AndroidConnection.Instance.addListener<SpeechEventListener> ("setSpeechEventCallback", new SpeechEventListener(onWakeup,onSpeechEvent));
			#endif
		}

		

		public void onWakeup(string msg)
		{
//			Debug.Log ("onWakeup2" + msg);
			if (OnSpeechWakeupEvent != null) {
				OnSpeechWakeupEvent (msg);
			}
		}

		public void onSpeechEvent(int eventCode)
		{
//			Debug.Log ("onSpeechEvent2" + eventCode);
			if (OnSpeechResultEvent != null) {
				OnSpeechResultEvent (eventCode);
			}
		}
	}
}
