using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ShadowKit.Air
{
	public class HandShankKeyEventListener : AndroidJavaProxy {
		Action<int> begin;
		Action<int> end;
		public HandShankKeyEventListener(Action<int> begin,Action<int> end):base("com.invision.unity.callback.HandShankKeyEventCallback")
		{
			this.begin = begin;
			this.end = end;
		}

		void onKeyEventChanged(int keycode, int keyevent, int deviceId)
		{
			AirInput.controllerClick (keycode, keyevent, deviceId);
			if (keycode == 16) {
				if (keyevent == 0) {
					this.begin (deviceId);
				} else {
					this.end (deviceId);
				}
			}
		}
	}
}
