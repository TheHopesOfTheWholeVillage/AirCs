//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Makes the hand act as an input module for Unity's event system
//
//=============================================================================

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace ShadowKit
{
	//-------------------------------------------------------------------------
	public class SCInput : PointerInputModule
	{
		public GameObject target{ private set; get;}

		public delegate void AnyKeyDown();
		public static event AnyKeyDown AnyKeyDownEvent;

		public static Vector3 Normal;
		public static Vector3 Position;

		public static Vector3 MousePosition;
		//-------------------------------------------------
		private static SCInput _instance;
		public static SCInput Instance
		{
			get
			{
				if ( _instance == null )
					_instance = GameObject.FindObjectOfType<SCInput>();

				return _instance;
			}
		}
			
		//-------------------------------------------------
//		public override bool ShouldActivateModule()
//		{
//			if ( !base.ShouldActivateModule() )
//				return false;
//
//			return submitObject != null;
//		}
		/// <summary>
		/// 执行焦点进入
		/// </summary>
		/// <param name="gameObject">Game object.</param>
		public void PointerEnter( GameObject gameObject )
		{
			PointerEventData pointerEventData = new PointerEventData( eventSystem );
			ExecuteEvents.Execute( gameObject, pointerEventData, ExecuteEvents.pointerEnterHandler );
		}
			
		/// <summary>
		/// 执行焦点退出
		/// </summary>
		/// <param name="gameObject">Game object.</param>
		public void PointerExit( GameObject gameObject )
		{
			PointerEventData pointerEventData = new PointerEventData( eventSystem );
			ExecuteEvents.Execute( gameObject, pointerEventData, ExecuteEvents.pointerExitHandler );
		}
			
		/// <summary>
		/// 按下
		/// </summary>
		/// <param name="gameObject">Game object.</param>
		public void PointDown(GameObject gameObject = null)
		{
			if (gameObject == null) {
				gameObject = target;
			}
			if (gameObject == null) {
				return;
			}
			PointerEventData pointerEventData = new PointerEventData (eventSystem);
			ExecuteEvents.Execute (gameObject, pointerEventData, ExecuteEvents.pointerDownHandler);
			ExecuteEvents.Execute(gameObject, pointerEventData, ExecuteEvents.pointerClickHandler);
		}

		/// <summary>
		/// 抬起
		/// </summary>
		/// <param name="gameObject">Game object.</param>
		public void PointUp(GameObject gameObject)
		{
			if (gameObject == null) {
				gameObject = target;
			}
			if (gameObject == null) {
				return;
			}
			PointerEventData pointerEventData = new PointerEventData (eventSystem);
			ExecuteEvents.Execute( gameObject, pointerEventData, ExecuteEvents.pointerUpHandler );
		}

		/// <summary>
		/// 拖动
		/// </summary>
		/// <param name="gameObject">Game object.</param>
		public void Drag(GameObject gameObject)
		{
			PointerEventData pointerEventData = new PointerEventData (eventSystem);
			ExecuteEvents.Execute( gameObject, pointerEventData, ExecuteEvents.dragHandler );
		}

		public void anyKeyDown()
		{
			if (AnyKeyDownEvent != null) {
				AnyKeyDownEvent ();
			}
		}

		//-------------------------------------------------
		public void SetTarget( GameObject gameObject )
		{
			target = gameObject;
		}
			
		//-------------------------------------------------
		public override void Process()
		{
		}
	}
}
