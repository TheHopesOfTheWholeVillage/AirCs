using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowKit;

public class ToolBarTab : MonoBehaviour {
	public GameObject item;
	// Use this for initialization
	void Start () {
		onEnd ();
	}

	public void onBegin()
	{
		item.SetActive (true);
	}

	public void onEnd()
	{
		item.SetActive (false);
	}
}
