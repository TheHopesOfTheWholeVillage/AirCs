using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowKit;

public class LoginSystem : MonoBehaviour {
	void Awake()
	{
		ShadowSystem.loginSystem = this;	
	}

	public void Login()
	{
		
		#if UNITY_ANDROID && !UNITY_EDITOR
		if ( !UserInfo.isLogin()) {
			AndroidConnection.Instance.addListener<LoginListener> ("setLoginSystemCallback",new LoginListener());
			AndroidConnection.Instance.Call ("unityInitCompleted");
		}
		#else
		UserInfo.Account = "1";
		#endif
	}

	void OnApplicationFocus(bool hasFocus)
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		if (hasFocus && !UserInfo.isLogin()) {
			AndroidConnection.Instance.Call ("unityInitCompleted");
		}
		#endif
	}
}
