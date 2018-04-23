using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowKit
{
	public class LoginListener :AndroidJavaProxy
	{
		
		public LoginListener(): base("com.invision.unity.callback.LoginSystemCallback")  
		{
		}

		public void onInitSerialno(string serialno)
		{
//			Debug.Log ("LoginListener.onInitSerialno=====>" + serialno);
			UserInfo.Serialno = serialno;
		}

		public void onInitAccountName(string serialno)
		{
//			Debug.Log ("LoginListener.onInitAccountName=====>" + serialno);

			if (serialno == string.Empty ) {
				unityLoginAndRegister ();
				UserInfo.Account = string.Empty;
			} else {
				UserInfo.Account = serialno+"";
				//支付测试
//				ShadowSystem.PaySystem.pay ("10001", 1, PayType.WEIXIN, payCallback);
			}
		}

		private void payCallback(string msg)
		{			
			Debug.Log ("Paysystem.payCallback========>" + msg);
		}
		public void onLoginSuccess(string accountName)
		{
//			Debug.Log ("LoginListener.onLoginSuccess=====>" + accountName);
			UserInfo.Account = accountName;
		}

		public void onLoginFailed(string msg)
		{
//			Debug.Log ("LoginListener.onLoginFailed=====>" + msg);
			UserInfo.Account = UserInfo.Serialno;
		}


		//如果没有影创账户则调用sdk注册方法
		public void unityLoginAndRegister()
		{
//			Debug.Log ("LoginListener.unityLoginAndRegister=====>");
			AndroidConnection.Instance.Call ("unityLoginAndRegister");
		}
	}
}

