using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Selects : MonoBehaviour {
    [SerializeField]
    GameObject[] Canvens;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Celect(int i)
    {
        Canvens[i].SetActive(true);
        Canvens[7].SetActive(false);
    }
    
    

}
