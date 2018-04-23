using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MianCam : MonoBehaviour
{
    [SerializeField]
    private GameObject[] canMian;
    Camera can;
    // Use this for initialization
    void Start()
    {
        can = gameObject.GetComponent<Camera>();
        canMian = GameObject.FindGameObjectsWithTag("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CanModel()
    {
        for (int i = 0; i < canMian.Length; i++)
        {
            canMian[i].GetComponent<Canvas>().worldCamera = can;
        }
    }
}