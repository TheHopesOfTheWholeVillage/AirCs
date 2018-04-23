using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShadowKit
{
	public class SCConner : MonoBehaviour {
		const  int scaleLength = 100;
		public int index;
		public GameObject window;
		public GameObject originObject;

		SCButton button;
		private Vector3 originPosition;
		private Vector3 startPosition;
		private Vector3 anchor;
		float d_x = 0;
		float d_y = 0;

		// Use this for initialization
		void Start () {
			button = GetComponent<SCButton> ();

			switch (index) {
			case 0:
				d_x = 1;
				d_y = -1;
				break;
			case 1:
				d_x = -1;
				d_y = -1;
				break;
			case 2:
				d_x = 1;
				d_y = 1;
				break;
			case 3:
				d_x = -1;
				d_y = 1;
				break;
			}
		}

		// Update is called once per frame
		void Update () {

		}

		public void onStart()
		{
			startPosition = SCInput.MousePosition;

			float ox = startPosition.x + d_x * scaleLength / 2;
			float oy = startPosition.y + d_y * scaleLength / 2;
			anchor = new Vector3 (ox, oy, 0);
			originPosition = originObject.transform.position;
			Debug.Log ( anchor.ToString());
		}

		public void onDrag()
		{
			Vector3 pos = SCInput.MousePosition;
			Vector3 dp = pos - anchor;
			float scaleX =Mathf.Abs( dp.x * 2 / scaleLength);
			float scaleY = Mathf.Abs (dp.y * 2 / scaleLength);
			float scale = Mathf.Max (scaleX,scaleY);
			window.transform.transform.localScale = new Vector3 (scale, scale, 1);

			Vector3 dop =  originPosition - originObject.transform.position;
			window.transform.position = window.transform.position + dop;

//			Debug.Log ("start:" + startPosition.ToString());
//			Debug.Log ("cur:" +  pos.ToString());
//			Debug.Log ("an:" + anchor.ToString());
//			Debug.Log ("dp:" + dp.ToString());
//			Debug.Log ("scaleX:" + scaleX + " scaleY:" + scaleY);
//			Debug.Log ("==========================================");
		}
	}
}

