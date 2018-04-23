using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ShadowKit.Air
{
	public class HandShankConnStateListener : AndroidJavaProxy {
		public HandShankConnStateListener():base("com.invision.unity.callback.HandShankConnStateCallback")
		{
		}

		public void onConnectionStateChange(int index, int state)
		{
			if(state==0)
			{
				//蓝牙手柄从链接到断开
				AirInput.unConnected (index);

			}else if(state==1)
			{
				//蓝牙手柄从断开到链接
				AirInput.connected (index);
			}
		}
	}
}
