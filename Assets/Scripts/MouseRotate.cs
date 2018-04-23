using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour {

    public GameObject SphereObj;

    public GameObject CubeObj;
   // public 
    private bool isRotate = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (SphereObj != null) {
            if (
                
                
                
                SphereObj.active == true)
            {

                CubeObj.transform.Rotate(0, 30, 0);
            }
        }

        
		
	}
}
