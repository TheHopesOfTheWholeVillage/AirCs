using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectArrow : MonoBehaviour {
    public GameObject CanvasObj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

       // transform.parent.transform.Rotate(0, 0, 120 * Time.deltaTime);

	}
    public void OnSelect() {

        GameObject.Find("AudioSource01").GetComponent<AudioSource>().Play();
        switch (ResManager.Step) {

            case 1:
                //显示精滤模型
                CanvasObj.GetComponent<UIManager>().ShowJingLvModel();
                break;
            case 2:
                //精滤拆卸动画
                CanvasObj.GetComponent<UIManager>().PlayChaiAni();
                break;
            case 3:

                break;

            case 4:
                //精滤安装动画
                CanvasObj.GetComponent<UIManager>().PlayZhuangAni();
                break;
            case 5:
                break;
            default:
                break;

        }

    }
}
