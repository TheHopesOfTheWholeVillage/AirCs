using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;


namespace ShadowKit{
	//[RequireComponent(typeof(BoxCollider))]
	[AddComponentMenu("ShadowCreator/SCButton")]
	public class SCButton :MonoBehaviour,  IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IEventSystemHandler,IDragHandler, IPointerEnterHandler, IPointerExitHandler{
		public enum Transition
		{
			None,
			Scale,
			Position,
		}

		public Transition transition = Transition.None;
		public float scalNum = 1.1f;
		public float transitionTime = 0.2f;
		public float forwardNum = 0.05f;


		public float autoClickTime = 0;//是否需要长注视

		public UnityEvent onClick;
//		public UnityEvent onDrag;
		public UnityEvent onEnter;
		public UnityEvent onExit;


		private Vector3 initScal;
		private Vector3 initPosition;
		private Quaternion initRotation;
		private float curDelayTime;//当前执行时间
		void  Start () {
			init ();
		}
		public virtual void init()
		{
			curDelayTime = autoClickTime;
			initPosition = transform.localPosition;
			initRotation= transform.localRotation;
			initScal= transform.localScale;
		}
		void  Update () {
			autoClick ();
		}

		void autoClick()
		{
			if (autoClickTime > 0 ) {
				curDelayTime = curDelayTime - Time.deltaTime;
				curDelayTime = curDelayTime < 0 ? 0 : curDelayTime;
				if (curDelayTime <= 0) {
					if (SCInput.Instance.target == gameObject) {
						SCInput.Instance.PointDown (gameObject);
					}
					curDelayTime = autoClickTime;
				}
			}
		}

		public virtual void OnPointerDown(PointerEventData data)
		{
			
		}

		public virtual void OnPointerUp(PointerEventData data)
		{
			
		}

		public virtual void OnPointerClick(PointerEventData data)
		{
			onClick.Invoke ();
		}

		public virtual void OnDrag(PointerEventData data)
		{
//			onDrag.Invoke ();
		}


		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			curDelayTime = autoClickTime;
			onMouseBeginAnimation ();
		}

		public virtual void OnPointerExit (PointerEventData eventData)
		{
			onMouseOutAnimation ();
		}

		void onMouseBeginAnimation()
		{
			switch (transition) {
			case Transition.Scale:
				transform.DOScale (initScal*scalNum, transitionTime).SetEase (Ease.InOutExpo).SetAutoKill(true);
				break;
			case Transition.Position:
				transform.DOLocalMove(initPosition+new Vector3(0,0,forwardNum*-1), transitionTime).SetEase (Ease.InOutExpo).SetAutoKill(true);
				break;
			}
		}

		void onMouseOutAnimation()
		{
			switch (transition) {
			case Transition.Scale:
				transform.DOScale (initScal, transitionTime).SetEase (Ease.InOutExpo).SetAutoKill(true);
				break;
			case Transition.Position:
				transform.DOLocalMove(initPosition, transitionTime).SetEase (Ease.InOutExpo).SetAutoKill(true);
				break;
			}
		}
	}
}

