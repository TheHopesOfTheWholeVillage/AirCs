using System.Collections;
using UnityEngine;
using UnityEditor;

namespace ShadowKit
{
	[CustomEditor(typeof(SCButton)),CanEditMultipleObjects]
	public class SCButtonInspector : Editor {
		

		SCButton btn;

		void OnEnable()
		{
			btn = (SCButton)target;
		}
		//执行这一个函数来一个自定义检视面板
		public override void OnInspectorGUI()
		{
			//设置整个界面是以垂直方向来布局
			EditorGUILayout.BeginVertical ();
			EditorGUILayout.PropertyField (serializedObject.FindProperty ("autoClickTime"));
			
			EditorGUILayout.PropertyField (serializedObject.FindProperty ("transition"));
			switch (btn.transition) {
			case SCButton.Transition.None:
				break;
			case SCButton.Transition.Scale:
				EditorGUILayout.PropertyField (serializedObject.FindProperty ("scalNum"));
				EditorGUILayout.PropertyField (serializedObject.FindProperty ("transitionTime"));
				break;
			case SCButton.Transition.Position:
				EditorGUILayout.PropertyField (serializedObject.FindProperty ("forwardNum"));

				break;
			}
//			EditorGUILayout.PropertyField (serializedObject.FindProperty ("onEnter"));;
//			EditorGUILayout.PropertyField (serializedObject.FindProperty ("onExit"));
			EditorGUILayout.PropertyField (serializedObject.FindProperty ("onClick"));
//			EditorGUILayout.PropertyField (serializedObject.FindProperty ("onDrag"));

			EditorGUILayout.EndVertical ();
			serializedObject.ApplyModifiedProperties ();
		}
	}
}

