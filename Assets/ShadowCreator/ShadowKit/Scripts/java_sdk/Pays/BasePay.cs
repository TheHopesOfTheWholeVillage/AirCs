using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ShadowKit{
	public class BasePay : MonoBehaviour {

		protected  string goodsID="10000";//支付商品id后期读配置文件
		protected int count = 0;
		protected  string goodsDescribe="商品描述 后期读配置文件";//商品描述 后期读配置文件
		protected  string outTradeNo;//订单号
		protected  string totalFee;//支付金额
		protected Action onSuccess;
		protected Action<string> onFailed;



		virtual public void init (string goodsID, int count, Action onSuccess, Action<string> onFailed)
		{
			this.goodsID = goodsID;
			this.count = count;
			this.onSuccess = onSuccess;
			this.onFailed = onFailed;
		}

		virtual public void start()
		{

		}

	}
}

