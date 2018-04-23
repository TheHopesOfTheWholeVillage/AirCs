using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowKit;

public class box : MonoBehaviour {

	public GameObject select;
	public GameObject normal;

	void Start()
	{
		end ();
	}
	public void begin()
	{
		normal.SetActive (false);
		select.SetActive (true);
	}

	public void end()
	{
		normal.SetActive (true);
		select.SetActive (false);
	}

	public void click(int index)
	{
		Debug.Log ("点击测试=====>" + index);
	}
}
	
