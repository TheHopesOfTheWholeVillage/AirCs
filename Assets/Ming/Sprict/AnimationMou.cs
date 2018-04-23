using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMou : MonoBehaviour {
     public  Animation animamou;
	// Use this for initialization
	void Start () {
        animamou = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void AnimaSw(string name)
    {
        animamou.CrossFade(name);
    }
}
