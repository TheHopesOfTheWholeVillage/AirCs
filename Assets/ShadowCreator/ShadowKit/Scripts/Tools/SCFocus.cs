using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShadowKit{
	public class SCFocus : MonoBehaviour {
		public ParticleSystem lighting;
		public bool active = false;
		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {
			if (SCInput.Instance == null) {
				return;
			}
			bool flg = SCInput.Instance.target != null;
			if (flg ) {
				if(active == false)
				{
					lighting.gameObject.SetActive (true);
					lighting.Simulate (0);
					lighting.Play ();
					active = true;
				}
			} else {
				if(active == true)
				{
					lighting.Stop ();
					lighting.gameObject.SetActive (false);

					active = false;
				}
			}
			transform.position = SCInput.Position;
		}
	}
}

