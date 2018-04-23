using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can5 : MonoBehaviour {

    [SerializeField]
    GameObject[] canNext;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OverBase()
    {
        canNext[0].SetActive(false);
        canNext[1].SetActive(true);
    }
    public void NewBase()
    {
        canNext[0].SetActive(false);
        canNext[2].SetActive(true);
    }
}
