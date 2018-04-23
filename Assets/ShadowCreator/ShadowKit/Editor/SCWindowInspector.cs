using System.Collections;
using UnityEngine;
using UnityEditor;
using System;

namespace ShadowKit
{
	[CustomEditor(typeof(SCWindow)),CanEditMultipleObjects]
	public class SCWindowInspector : Editor {

		SCWindow window;
		Transform c0;
		Transform c1;
		Transform c2;
		Transform c3;
		Transform l0;
		Transform l1;
		Transform l2;
		Transform l3;

		void OnEnable()
		{
			//获取当前编辑自定义Inspector的对象
			window = (SCWindow)target;
			c0 = window.transform.Find ("border").Find("c0");
			c1 = window.transform.Find ("border").Find ("c1");
			c2 = window.transform.Find ("border").Find ("c2");
			c3 = window.transform.Find ("border").Find ("c3");
			l0 = window.transform.Find ("border").Find ("l0");
			l1 = window.transform.Find ("border").Find ("l1");
			l2 = window.transform.Find ("border").Find ("l2");
			l3 = window.transform.Find ("border").Find ("l3");
		}
		//执行这一个函数来一个自定义检视面板
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI ();
			float d = 0.02f;
			float top = window.toolBar.transform.localPosition.y + window.toolBar.transform.localScale.y / 2 + d;
			float bottom = window.panel.transform.localPosition.y - window.panel.transform.localScale.y / 2 - d;
			float left = window.panel.transform.localPosition.x - window.panel.transform.localScale.x / 2 - d;
			float right = window.panel.transform.localPosition.x + window.panel.transform.localScale.x / 2 + d;
			float centerX = (left + right) / 2;
			float centerY = (top + bottom) / 2;
			float width = right - left;
			float height = top - bottom;
			c0.localPosition = new Vector3 (left,top,0);
			c1.localPosition = new Vector3 (right,top,0);
			c2.localPosition = new Vector3 (left,bottom,0);
			c3.localPosition = new Vector3 (right,bottom,0);
			l0.localPosition = new Vector3 (centerX, top, 0);
			l1.localPosition = new Vector3 (left, centerY, 0);
			l2.localPosition = new Vector3 (centerX, bottom, 0);
			l3.localPosition = new Vector3 (right, centerY, 0);
			l0.localScale = l2.localScale = new Vector3 (width, 0.02f, 0.02f);
			l1.localScale = l3.localScale = new Vector3 (0.02f, height, 0.02f);

		}
	}}

