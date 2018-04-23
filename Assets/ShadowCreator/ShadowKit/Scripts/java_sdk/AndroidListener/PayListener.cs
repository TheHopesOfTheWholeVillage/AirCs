using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PayListener : AndroidJavaProxy {
	public Action<string> onSuccess;
	public Action<string> onFailed;
	public PayListener(): base("com.invision.unity.callback.PaySystemCallback")  
	{
	}

	public void onSdkWxmicropayFailded(string result)
	{
		Debug.Log ("paySystem.onSdkWxmicropaySuccess======>" + result);
		onFailed (result);
	}
	public void onSdkWxmicropaySuccess(string result)
	{
		Debug.Log ("paySystem.onSdkWxmicropayFailded======>" + result);
		onSuccess (result);
	}
}
