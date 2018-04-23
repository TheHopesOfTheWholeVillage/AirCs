 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwice : MonoBehaviour {
    public GameObject Cam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void MainGuidance()
    {
        DontDestroyOnLoad(Cam);
        Application.LoadLevelAsync("ssNew");
    }
}
