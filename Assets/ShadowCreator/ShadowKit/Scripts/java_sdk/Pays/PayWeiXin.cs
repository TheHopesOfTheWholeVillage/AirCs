using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
using System;


namespace ShadowKit{
	public class PayWeiXin : BasePay {

		override public void start()
		{
			getWxmicropay ();
		}

		//获取微信支付订单
		private void getWxmicropay()
		{
			createWxmicropay (goodsID, count, createWxmicropaySuccessFun, createWxmicropayFaildFun);
		}

		//获取订单成功。。
		private void createWxmicropaySuccessFun(JsonData s)
		{
			outTradeNo=s["OutTradeNo"].ToString();
			totalFee=s["TotalFee"].ToString();
			callSdkWxmicropay ();
		}

		//获取订单失败。弹出失败原因
		private void createWxmicropayFaildFun(string s)
		{
			doFailed (s);
		}

		//与sdk交互唤起微信支付
		public void callSdkWxmicropay()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			string payInfo = UserInfo.Account + ";" + UserInfo.AppID + ";" + goodsID + ";" + goodsDescribe + ";" + outTradeNo + ";" + totalFee;
			AndroidConnection.Instance.addListener<PayListener> ("setPaySystemCallback",new PayListener(){onSuccess = onSdkWxmicropaySuccess, onFailed =onSdkWxmicropayFailded });
			AndroidConnection.Instance.Call("unityPay",payInfo);
			#endif
			//需要加一个遮罩 玩家支付等待中～～～～
		}

		//与sdk交互结束 获取支付返回结果
		private void onSdkWxmicropaySuccess(string result)
		{
			Debug.Log ("paySystem.onSdkWxmicropaySuccess======>" + result);
			//支付成功 支付等待中 像应用后端验证支付结果
			quertWxmicropay ();
		}

		public void onSdkWxmicropayFailded(string result)
		{
			//支付失败请重新支付
			doFailed(result);
		}

		//得到SDK支付请求，与后端服务器验证
		private void quertWxmicropay()
		{
			Debug.Log ("paySystem.quertWxmicropay");

			quertWxmicropay (outTradeNo,paySuccessFun,payFaildFun);
		}


		private int maxCallNum=10;//请求等待重试次数
		private int calldelay=3;//间隔回调事件
		private int currentCallNum=0;

		//支付成功 增加道具
		private void paySuccessFun(JsonData result)
		{
			Debug.Log ("Paysystem.paySuccessFun  "+result.ToJson());
			string type=result ["code"].ToString ();
			switch(type)
			{
			case "200":
				Debug.Log ("支付成功");
				onSuccess ();
				Destroy (this);
				break;
			case "500":
				currentCallNum++;
				if (currentCallNum > maxCallNum) {
					Debug.Log ("支付异常 请重试");	
					doFailed ("支付异常 请重试");
				} else {
					Invoke ("quertWxmicropay",calldelay);
				}
				break;
			}
		}
			
		//支付失败 弹出原因
		private void payFaildFun(string result)
		{
			Debug.Log ("payFaildFun  "+result);
		}

		void doFailed(string result)
		{
			onFailed (result);
		}

		void OnDestroy()
		{
			Debug.Log ("Paysystem.destroy");
			this.onSuccess = null;
			this.onFailed = null;
//			AndroidConnection.Instance.addListener<PayListener> ("setPaySystemCallback",null);
		}



		//支付获取订单
		/// <summary>
		/// pay
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="pas">Pas.</param>
		/// <param name="success">Success.</param>
		/// <param name="failed">Failed.</param>
		private void createWxmicropay(string goodsId,int goodsCount,Action<JsonData> success = null,Action<string> failed = null)
		{
			JsonData data = new JsonData ();
			data ["AppId"] = UserInfo.AppID;
			data ["Account"] = UserInfo.Account;
			data ["GoodsId"] = goodsId;
			data ["GoodsCount"] = goodsCount;
			string action = "pay/create/wxmicropay";
			string url = "http://chengyxtest.chinacloudapp.cn/sdk_app_pay/";
			GetComponent<NetWorkManager>().addNetwork (url, action, data.ToJson (), success, failed);
		}

		private void quertWxmicropay(string OutTradeNo,Action<JsonData> success = null,Action<string> failed = null)
		{
			JsonData data = new JsonData ();
			data ["AppId"] = UserInfo.AppID;
			data ["OutTradeNo"] = OutTradeNo;
			string action = "pay/query/wxmicropay";
			string url = "http://chengyxtest.chinacloudapp.cn/sdk_app_pay/";
			GetComponent<NetWorkManager>().addNetwork (url, action, data.ToJson (), success, failed);
		}
	}
}
