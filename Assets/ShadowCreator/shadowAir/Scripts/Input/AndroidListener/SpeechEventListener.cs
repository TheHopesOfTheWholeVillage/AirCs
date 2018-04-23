using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeechEventListener : AndroidJavaProxy {
	Action<string> wakeup;
	Action<int> speech;
	public SpeechEventListener (Action<string> wakeup, Action<int> speech) : base ("com.invision.unity.callback.SpeechEventCallback")
	{
		this.wakeup = wakeup;
		this.speech = speech;
	}

	public void onWakeup(string msg)
	{
		Debug.Log ("onWakeup1" + msg);
		this.wakeup (msg);
	}

	public void onSpeechEvent(int eventCode)
	{
		Debug.Log ("onSpeechEvent1" + eventCode);
		this.speech (eventCode);
	}
}
