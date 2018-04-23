using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
namespace ShadowKit
{
	public class Network {

		public bool active = false;
		public string action;
		public string data;
		public string type;
		public string url;
		private Action<string> failedFun;
		private Action<JsonData> successFun;

		/// <summary>
		/// Init the specified action, data, type, success and failed.
		/// </summary>
		/// <param name="action">Action.</param>
		/// <param name="data">Data.</param>
		/// <param name="type">Type.</param>
		/// <param name="success">Success.</param>
		/// <param name="failed">Failed.</param>
		public void init(string url, string action, string data,Action<JsonData> successFun = null ,Action<string> failedFun = null)
		{
			this.action = action;
			this.data = data;
			this.successFun = successFun;
			this.failedFun = failedFun;
			this.url = url + "?s=" + action;
		}

		public void scuess(JsonData data)
		{
			//		Debug.Log ("success:");
			if (successFun != null) {
				successFun (data);
			}
		}

		public void failed (string msg)
		{
			if (failedFun != null) {
				failedFun (msg);
			}
		}

		public void error(string msg)
		{
			if (failedFun != null) {
				failedFun (msg);
			}
		}
	}
}
