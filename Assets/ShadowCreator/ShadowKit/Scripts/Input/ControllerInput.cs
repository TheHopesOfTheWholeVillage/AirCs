using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowKit
{
	public class ControllerInput {
		public void update()
		{
			bool plam = false;
			//			shou.SetActive (false);

			if (Input.anyKeyDown) {  
				Event e = Event.current;  
				if (e!=null&&e.isKey) {  
					Debug.Log (e.keyCode.ToString ());
					if(e.keyCode.ToString ()=="LeftControl")
					{
						//						shou.SetActive (true);
						//						quan.SetActive (false);
					}
					if(e.keyCode.ToString ()=="RightControl")
					{
						//						shou.SetActive (false);
						//						quan.SetActive (true);
					}
					if(e.keyCode.ToString ()!="RightControl"&&e.keyCode.ToString ()!="LeftControl")
					{
						//clickText.text = e.keyCode.ToString ();
					}
					if(e.keyCode.ToString ()=="LeftBracket")
					{
						plam = true;
					}
				}  
			}

			if (Input.GetKeyDown(KeyCode.Escape)) {
				ShadowSystem.Quit ();
			}
//			if (Input.GetKeyDown (KeyCode.LeftShift)||plam) {
//				ShadowSystem.ReLocate ();
//			}
//			if (Input.GetKeyDown (KeyCode.RightShift)) {
//				ShadowSystem.Reset();
//			}
		}
	}
}

