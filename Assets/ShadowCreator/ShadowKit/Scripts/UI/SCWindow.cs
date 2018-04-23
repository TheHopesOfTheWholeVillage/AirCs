using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace ShadowKit
{
	public class SCWindow : MonoBehaviour {

//		public Vector3 initialPosition = new Vector3 (0, 0, 1.0f);
		public GameObject toolBar;
		public GameObject panel;
		private GameObject border;
		private List<Transform> corners;
		private List<Transform> lines;
		public bool inScale = false;

		void Start () {
			border = transform.Find ("border").gameObject;
			corners = new List<Transform> ();
			lines = new List<Transform> ();
			Transform c;
			c = border.transform.Find ("c0");
			corners.Add (c);
			c = border.transform.Find ("c1");
			corners.Add (c);
			c = border.transform.Find ("c2");
			corners.Add (c);
			c = border.transform.Find ("c3");
			corners.Add (c);

			c = border.transform.Find ("l0");
			lines.Add (c);
			c = border.transform.Find ("l1");
			lines.Add (c);
			c = border.transform.Find ("l2");
			lines.Add (c);
			c = border.transform.Find ("l3");
			lines.Add (c);
			border.SetActive (false);
		}

		// Update is called once per frame
		void Update () {
//			EventSystem.current.currentInputModule;
		}

		public void scaleMode ()
		{
			inScale = !inScale;
			border.SetActive (inScale);

			if (!inScale) {
				return;
			}
			border.SetActive (true);
			float d = 0.02f;
			float top = toolBar.transform.localPosition.y + toolBar.transform.localScale.y / 2 + d;
			float bottom = panel.transform.localPosition.y - panel.transform.localScale.y / 2 - d;
			float left = panel.transform.localPosition.x - panel.transform.localScale.x / 2 - d;
			float right = panel.transform.localPosition.x + panel.transform.localScale.x / 2 + d;
			float centerX = (left + right) / 2;
			float centerY = (top + bottom) / 2;
			float width = right - left;
			float height = top - bottom;
			corners[0].localPosition = new Vector3 (left,top,0);
			corners[1].localPosition = new Vector3 (right,top,0);
			corners[2].localPosition = new Vector3 (left,bottom,0);
			corners[3].localPosition = new Vector3 (right,bottom,0);
			lines[0].localPosition = new Vector3 (centerX, top, 0);
			lines[1].localPosition = new Vector3 (left, centerY, 0);
			lines[2].localPosition = new Vector3 (centerX, bottom, 0);
			lines[3].localPosition = new Vector3 (right, centerY, 0);
			lines[0].localScale = lines[2].localScale = new Vector3 (width, 0.02f, 0.02f);
			lines[1].localScale = lines[3].localScale = new Vector3 (0.02f, height, 0.02f);
		}
	}
}

