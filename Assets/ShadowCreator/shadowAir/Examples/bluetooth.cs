using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowKit.Air;
namespace ShadowKit.Air.Example
{
	public class bluetooth : MonoBehaviour {
		public int deviceId;
		// Use this for initialization
		void Start () {
			BluetoothHandleDevice.Instance.enable3Dof (true);
//			BluetoothHandleDevice.Instance.enableAcc (true);

		}
		// Update is called once per frame
		void Update () {
			if (deviceId == 0) {
				this.transform.rotation = AirInput.BluetoothHandleRotation1;
			} else {
				this.transform.rotation = AirInput.BluetoothHandleRotation2;
			}
		}

		void OnDestroy()
		{
//			BluetoothHandleDevice.Instance.enable3Dof (false);
//			BluetoothHandleDevice.Instance.enableAcc (false);
		}
	}
}

