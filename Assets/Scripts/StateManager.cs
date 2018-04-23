using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    

    // 鼠标起始状态识别 3 无电池盖有电池 4 无电池盖无电池 5 螺丝识别 6 内部识别
    private static int state_Curr;

    public static int State_Curr {

        get { return state_Curr;  }

        set { state_Curr = value; }

    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
