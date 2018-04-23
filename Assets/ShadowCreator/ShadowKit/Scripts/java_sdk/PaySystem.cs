using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace ShadowKit{
	public enum PayType
	{
		WEIXIN,
	}

	public class PaySystem : MonoBehaviour {
		Action<string> payCallback;
		GameObject payObj;

		void Awake()
		{
//			ShadowSystem.PaySystem = this;	
		}

		public void pay(string goodsID, int count, PayType type,Action<string> payCallback)
		{
			if (payObj != null) {
				return;
			}
			this.payCallback = payCallback;
			switch (type) {
			case PayType.WEIXIN:
				GameObject prefab = (GameObject)Resources.Load ("Prefabs/Pays/PayWeiXin");
				payObj = Instantiate (prefab);
				payObj.transform.parent = transform;
				PayWeiXin pay = payObj.GetComponent<PayWeiXin> ();
				pay.init (goodsID, count, paySuccess, payFailed);
				pay.start ();
				break;
			}
		}

		void paySuccess()
		{
			Debug.Log ("Paysystem.paySuccessFun  !!!!!!!!!!!!!!!!!");

			payCallback ("1");
			clear ();
		}

		void payFailed(string msg)
		{
			payCallback (msg);
			clear ();
		}

		void clear()
		{
			Destroy (payObj);
			payObj = null;
		}
	}
}

