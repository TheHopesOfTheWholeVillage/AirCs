using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShadowKit
{
	public class InputSystem : MonoBehaviour {

		public delegate void OnUpdate();
		public static  OnUpdate OnUpdateEvent;

		public float MaxRaycastDistance = 20.0f;//射线最远距离

		public LayerMask RaycastLayerMask = Physics.DefaultRaycastLayers;

		void Start () {
		}


		void Update () {
			updateGaze ();
		}

		void updateGaze()
		{
			Vector3 gazeOrigin = ShadowSystem.Head.transform.position;
			Vector3 gazeDirection =ShadowSystem.Head.transform.forward;
			RaycastHit hitinfo; 
			if (Physics.Raycast (gazeOrigin, gazeDirection, out hitinfo, MaxRaycastDistance, RaycastLayerMask)) {//从摄像机发出到点击坐标的射线
				SCInput.Position = hitinfo.point - new Vector3(0,0,0.005f);
				SCInput.Normal = hitinfo.normal;
				GameObject newObj = hitinfo.collider.gameObject;

				if (newObj != SCInput.Instance.target) {
					if (SCInput.Instance.target != null) {
						SCInput.Instance.PointerExit (SCInput.Instance.target);
					}
					SCInput.Instance.PointerEnter (newObj);
					SCInput.Instance.SetTarget(newObj);
				}
			} else {
				if (SCInput.Instance.target != null) {
					SCInput.Instance.PointerExit (SCInput.Instance.target);
				}
				SCInput.Instance.SetTarget(null);
				SCInput.Position = gazeOrigin + (gazeDirection * 2.0f);
				SCInput.Normal = gazeDirection;
			}
		}

		// Update is called once per frame
		void LateUpdate() {
			if (OnUpdateEvent != null) {
				OnUpdateEvent ();
			}
		}
	}
}

