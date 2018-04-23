//using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System;


namespace ShadowKit
{
	public class ShadowSystem : MonoBehaviour{
		public delegate void onApplicationFocus(bool hasFocus);
		public static event onApplicationFocus onApplicationFocusEvent;

//		public static PaySystem PaySystem;
		public static LoginSystem loginSystem;
		public static GameObject Head;
		public static Camera Camera;

//		public string appID = "1";
		public static Action Quit;

		private static bool init;
	
		void Awake()
		{
			if (!init) {
				DontDestroyOnLoad(this.gameObject);
//				UserInfo.AppID = appID;
				MouseDevice.Instance.init ();
				init = true;
			} else {
				Destroy (this.gameObject);
				return;
			}
		}
		//	private Coroutine handleBackButton = null;
		//	public Text _senceIndexText;
		//	public Text clickText;

		void Start()
		{
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			Input.backButtonLeavesApp = false;
		}

		void OnApplicationFocus(bool hasFocus)
		{
			if (onApplicationFocusEvent != null) {
				onApplicationFocusEvent (hasFocus);
			}
		}
	}
}

