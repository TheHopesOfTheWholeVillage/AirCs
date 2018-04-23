using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using UnityEngine.UI;
namespace ShadowKit{
	public class  NetWorkManager: MonoBehaviour {
		private List<Network> queue=new List<Network>();

		void checkQueue()
		{
			if (queue.Count == 0) {
				return;
			}
			if (queue [0].active) {
				return;
			}
			StartCoroutine (post(queue[0]));
		}

		IEnumerator post(Network net)
		{
			net.active = true;

			WWWForm form = new WWWForm ();
			form.AddField ("param", queue [0].data);
			WWW www = new WWW (queue[0].url,form);
			Debug.Log("url:   " +  queue[0].url);
			Debug.Log("param:  " + queue[0].data);
			yield return www;//等待Web服务器的反应
			string result = string.Empty;
			if (www.error != null) {            
				result = www.error;
				Debug.Log ("networkError: " + result);
				postError ("1009");
			} else {
				result = www.text;
				Debug.Log ("networkSuccess: " + result);
				postSuccess (result);
			}

			queue.RemoveAt (0);
			checkQueue ();
		}

		void postSuccess(string postData)
		{
			JsonData data = null;
			string code = "";

			try
			{
				data = JsonMapper.ToObject (postData);
				code = data ["code"].ToString();

			}
			catch(Exception e)
			{
				Debug.Log (e.Data);
				postError ("1009");
			}

			switch (code) {
			case "200":
				queue [0].scuess (data ["data"]);

				break;
			default :
				queue [0].failed (code);
				break;
			}
		}

		void postError(string errorData)
		{
			queue [0].error (errorData);
		}

		/// <summary>
		/// Adds the network.
		/// </summary>
		/// <param name="action">Action.</param>
		/// <param name="data">Data.</param>
		/// <param name="type">Type.</param>
		/// <param name="successFun">Success fun.</param>
		/// <param name="failedFun">Failed fun.</param>
		public void addNetwork(string url, string action, string data,Action<JsonData> successFun = null ,Action<string> failedFun = null)
		{
			Network net = new Network ();
			net.init (url, action, data, successFun, failedFun);
			queue.Add (net);
			checkQueue ();
		}

		public void netWorkComplete()
		{
			netWorkComplete ();
		}
	}
}
