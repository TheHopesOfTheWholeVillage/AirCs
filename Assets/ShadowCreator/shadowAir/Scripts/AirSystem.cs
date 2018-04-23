using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace ShadowKit.Air
{
	public class AirSystem : MonoBehaviour {
		public GameObject Head;
		public Camera Camera;

		public static AirSystem Instance;
		private static bool init;
		private SvrManager svrManager = null;
		private int sceneIndex = 0;
		void Awake()
		{
			if (!init) {
				
				Instance = this;
				DontDestroyOnLoad(this.gameObject);
				init = true;
				ShadowSystem.Head = Head;
				ShadowSystem.Camera = Camera;
				try{
					BluetoothHandleDevice.Instance.init ();
				}
				catch{
					Debug.Log ("蓝牙手柄初始化出错");
				}
			} else {
				Destroy (this.gameObject);
				return;
			}
		}

		void Start()
		{
			svrManager = SvrManager.Instance;
			ShadowSystem.Quit = doQuit;
//			ShadowSystem.ReLocate = ReLocate;
//			ShadowSystem.Reset = Reset;
			Input.backButtonLeavesApp = false;

			var activeScene = SceneManager.GetActiveScene();
			sceneIndex = activeScene.buildIndex;
		}

		/// <summary>
		/// 重置环境
		/// </summary>
		public void Reset()
		{
			StartCoroutine (doReset());
		}

		/// <summary>
		/// 重置场景坐标
		/// </summary>
		public void ReLocate()
		{
			SvrPlugin.Instance.RecenterTracking();
		}

		public void doQuit()
		{
			StartCoroutine (quit());
		}

		/// <summary>
		/// 重置环境
		/// </summary>
		IEnumerator doReset()
		{
			svrManager.SetOverlayFade (SvrManager.eFadeState.FadeOut);
			yield return new WaitUntil (() => svrManager.IsOverlayFading () == false);

			svrManager.Shutdown ();
			yield return new WaitUntil (() => svrManager.Initialized == false);
			SceneManager.LoadScene (sceneIndex);
			System.GC.Collect ();
		}


		IEnumerator changeScene()
		{
			svrManager.SetOverlayFade(SvrManager.eFadeState.FadeOut);
			yield return new WaitUntil(() => svrManager.IsOverlayFading() == false);

			// Load next scene in build settings, quit when done
			svrManager.Shutdown();
			yield return new WaitUntil(() => svrManager.Initialized == false);
			sceneIndex=sceneIndex+1;
			sceneIndex = sceneIndex > 2 ? 0 : sceneIndex;
			//_senceIndexText.text = "新加载"+sceneIndex.ToString ();
			SceneManager.LoadScene(sceneIndex);

			System.GC.Collect();
		}

		IEnumerator quit()
		{
			svrManager.Shutdown();
			yield return new WaitUntil(() => svrManager.Initialized == false);

			SceneManager.LoadScene(0);

			System.GC.Collect();
			sceneIndex = -1;
			Application.Quit();
		}

	}
}
