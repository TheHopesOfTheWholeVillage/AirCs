using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelKz : MonoBehaviour {
    [SerializeField]
    private GameObject Obj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Models()
    {
        Obj.SetActive(true);
    }
}
