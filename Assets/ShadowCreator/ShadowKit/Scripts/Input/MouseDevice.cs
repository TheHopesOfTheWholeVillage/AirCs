using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowKit
{
	public class MouseDevice
	{
		public static MouseDevice _instance;
		public static MouseDevice Instance {
			get {
				if (_instance == null) {
					_instance = new MouseDevice ();
				}
				return _instance;
			}
		}

		public void init()
		{
			InputSystem.OnUpdateEvent += update;
		}

		GameObject target;

		void update()
		{
			if (Input.GetMouseButtonDown (0)) {
				//按下鼠标
				SCInput.MousePosition = getMousePosition();
				begin();
				return;
			}
			if (Input.GetMouseButtonUp (0)) {
				//松开鼠标
				SCInput.MousePosition = getMousePosition();
				end();
				return;
			}
			if (target != null) {
				//持续按鼠标
				SCInput.MousePosition = getMousePosition();
				drag ();
				return;
			}
		}

		void begin()
		{
			if (SCInput.Instance.target != null && target == null) {
				target = SCInput.Instance.target;
				SCInput.Instance.PointDown (target);
			}
			anyKeyDown ();
		}

		void drag()
		{
			if (target == null) {
				return;
			}
			SCInput.Instance.Drag (target);
		}

		void end()
		{
			SCInput.Instance.PointUp (target);
			target = null;
		}

		Vector3 getMousePosition()
		{
			Ray ray = ShadowSystem.Camera.ScreenPointToRay (Input.mousePosition);
			Vector3 res;
			res = ray.GetPoint(100.0f);
			return res;
		}

		void anyKeyDown()
		{
			SCInput.Instance.anyKeyDown ();
		}
	}
}

