using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagerScr : MonoBehaviour {

    public GameObject[] TargetObjectCol;
	// Use this for initialization
	void Start () {

        StateManager.State_Curr = 1;

        Reset();

	}

    
    // 进入鼠标正状态
    public void EnterTop() {

        Reset();
        if (TargetObjectCol[0].active != true) {

            TargetObjectCol[0].SetActive(true);
        }
    }

    // 进入鼠标反状态
    public void EnterBottom() {


        Reset();
        if (TargetObjectCol[1].active != true) {
            
            TargetObjectCol[1].SetActive(true);
        }

    }
    // 清空识别对象

    void Reset() {

        for (int i = 0; i < TargetObjectCol.Length; i++) {

            if (TargetObjectCol[i].active != false) {

                TargetObjectCol[i].SetActive(false);
            }
        }

    }

    // 进入没有电池盖识别
    public void EnterTarget_WuGai() {

        Reset();

        if (TargetObjectCol[2].active != true) {

            TargetObjectCol[2].SetActive(true);
        }
        
    }
    // 进入没有电池识别状态
    public void EnterTarget_WuDianChi() {

        Reset();

        if (TargetObjectCol[3].active != true) {

            TargetObjectCol[3].SetActive(true);
        }

    }
    // 进入主板识别状态
   public void EnterTarget_NeiBu() {

        Reset();

        if (TargetObjectCol[4].active != true) {

            TargetObjectCol[4].SetActive(true);
        }


    }
    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.A)) {

            EnterTarget_WuGai();
        }
        if (Input.GetKeyDown(KeyCode.S)) {

            EnterTarget_WuDianChi();
        }

        if (Input.GetKeyDown(KeyCode.D)) {

            EnterTarget_NeiBu();
        }

        //switch (StateManager.State_Curr) {

        //    case 3:
        //        // 此部分表示识别状态是：无电池盖
        //        EnterTarget_WuGai();
        //        break;
        //    case 4:
        //        // 此部分表示状态：无电池盖无电池
        //        EnterTarget_WuDianChi();
        //        break;
        //    case 5:

        //        // 此部分表示状态：鼠标内部
        //        EnterTarget_NeiBu();
        //        break;
        //    default:
        //        break;

        //}
		
	}
}
