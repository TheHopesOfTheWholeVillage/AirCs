using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManSwy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Invoke("KK", 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Keys(string s)
    {
        Application.LoadLevel(s);
    }
    public void KK()
    {
        Application.LoadLevel(1);
    }
    
    
}
