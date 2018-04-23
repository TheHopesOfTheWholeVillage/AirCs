using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowKit;
using ShadowKit.Air;

public class SpeechText : MonoBehaviour {

	private TextMesh text;
	private string msg = "";
	private bool needUpdate = false;
	// Use this for initialization

	void Start () {
		//初始化语音系统
		Speech.Instance.init ();
		Speech.Instance.Enabled = true;
		Speech.OnSpeechWakeupEvent += Wakeup;
		Speech.OnSpeechResultEvent += SpeechResult;
		//初始化结束
		text = gameObject.GetComponent<TextMesh> ();
		msg = "待机中。。。";
		needUpdate = true;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.LookAt (ShadowSystem.Head.transform.position);
		gameObject.transform.eulerAngles = new Vector3 (0, 180 + gameObject.transform.eulerAngles.y, 0);
		if (needUpdate) {
			text.text = msg;
			needUpdate = false;
		}
	}

	void OnDestroy()
	{
		Speech.OnSpeechResultEvent -= SpeechResult;
		Speech.OnSpeechWakeupEvent -= Wakeup;
	}

	private void Wakeup(string msg)
	{
		this.msg = msg;
		needUpdate = true;
	}

	private void SpeechResult(int eventCode)
	{
		switch (eventCode) {
		case -1:
			//error
			this.msg = "无法识别语音";
			break;
		case 0:
			//非正常事件
			this.msg = "我不知道你在说些什么";
			break;
		case 4:
			//返回
			this.msg = "返回";
			break;
		case 19:
			//向上/向上滑/上边
			this.msg = "向上/向上滑/上边";
			break;
		case 20:
			//向下/向下滑/下边
			this.msg = "向下/向下滑/下边";
			break;
		case 21:
			//向左/向左滑/下左
			this.msg = "向左/向左滑/下左";
			break;
		case 22:
			//向右/向右滑/右边
			this.msg = "向右/向右滑/右边";
			break;
		case 23:
			//点击/确定
			this.msg = "点击/确定";
			break;
		}
		needUpdate = true;

	}
}
