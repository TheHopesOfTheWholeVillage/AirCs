using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShadowKit.Air
{
	public enum AirKeyCode{
		// 一号手柄
		MIDDLE_1 = 10,
		UP_1 = 11,
		DOWN_1 = 12 ,
		LEFT_1 = 13,
		RIGHT_1 = 14,
		// 二号手柄
		MIDDLE_2 = 20,
		UP_2= 21,
		DOWN_2= 22,
		LEFT_2= 23,
		RIGHT_2= 24,
		// 未知按钮
		OTHER = 0
	}

	public enum AirKeyEvent{
		UP = 0,
		DOWN = 1,
	}

	public class AirInput
	{
		public static Quaternion BluetoothHandleRotation1;
		public static Vector3 BluetoothHandleAngles1;
//		public static Vector3 BluetoothHandleAcc;

		public static Quaternion BluetoothHandleRotation2;
		public static Vector3 BluetoothHandleAngles2;
//		public static Vector3 BluetoothHandleAcc1;


		public static Quaternion HeadRotation;
		public static bool isControllerConnect1{ get; private set; }
		public static bool isControllerConnect2{ get; private set; }

		public delegate void ControllerConnect(int index);
		public static event ControllerConnect ControllerConnectEvent;

		public delegate void ControllerUnConnect(int index);
		public static event ControllerUnConnect ControllerUnConnectEvent;

		public delegate void EscapeDown();
		public static event EscapeDown EscapeDownEvent;

		public delegate void ControllerClick(AirKeyCode code,AirKeyEvent type);
		public static event ControllerClick ControllerClickEvent;

		public static void connected(int index)
		{
			if (index == 0) {
				isControllerConnect1 = true;
				if (ControllerConnectEvent != null) {
					ControllerConnectEvent (1);
				}
			}

			if (index == 1) {
				isControllerConnect2 = true;
				if (ControllerConnectEvent != null) {
					ControllerConnectEvent (2);
				}
			}
		}

		public static void unConnected(int index)
		{
			if (index == 0) {
				isControllerConnect1 = false;
			}
			if (index == 1) {
				isControllerConnect2= false;
			}
			if (ControllerUnConnectEvent != null) {
				ControllerUnConnectEvent (index);
			}
		}
			
		public static bool escapeDown()
		{
			if (EscapeDownEvent != null) {
				EscapeDownEvent ();
				return true;
			}
			return false;
		}

		public static void controllerClick(int keycode, int keyevent, int deviceId)
		{
			if (ControllerClickEvent != null) {
				AirKeyCode keyCode = AirKeyCode.OTHER;
				AirKeyEvent keyEvent = (AirKeyEvent)keyevent;
			
				switch (keycode) {
				case 1:
					//上
					if (deviceId == 0) {
						keyCode = AirKeyCode.UP_1;
					}
					if (deviceId == 1) {
						keyCode = AirKeyCode.UP_2;
					}
					break;
				case 2:
					//下
					if (deviceId == 0) {
						keyCode = AirKeyCode.DOWN_1;
					}
					if (deviceId == 1) {
						keyCode = AirKeyCode.DOWN_2;
					}
					break;
				case 4:
					//左
					if (deviceId == 0) {
						keyCode = AirKeyCode.LEFT_1;
					}
					if (deviceId == 1) {
						keyCode = AirKeyCode.DOWN_2;
					}
					break;
				case 8:
					//右
					if (deviceId == 0) {
						keyCode = AirKeyCode.RIGHT_1;
					}
					if (deviceId == 1) {
						keyCode = AirKeyCode.RIGHT_2;
					}
					break;
				case 16:
					//中间
					if (deviceId == 0) {
						keyCode = AirKeyCode.MIDDLE_1;
					}
					if (deviceId == 1) {
						keyCode = AirKeyCode.MIDDLE_2;
					}
					break;
				default:
					keyCode = AirKeyCode.OTHER;
					break;
				}
				ControllerClickEvent (keyCode, keyEvent);
				if (keyEvent == AirKeyEvent.DOWN) {
					anyKeyDown ();
				}
			}
		}

		private static GameObject target = null;
		public static void PointDown()
		{
			if (SCInput.Instance.target == null) {
				anyKeyDown ();
			}
			if (SCInput.Instance.target!= null && target == null) {
				target = SCInput.Instance.target;
				SCInput.Instance.PointDown (target);
			}
		}

		public static void PointUp()
		{
			SCInput.Instance.PointUp (target);
			target = null;
		}

		static void anyKeyDown()
		{
			SCInput.Instance.anyKeyDown ();
		}

	}
}

