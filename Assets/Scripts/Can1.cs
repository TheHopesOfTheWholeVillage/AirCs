using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can1 : MonoBehaviour {
    [SerializeField]
    int level;
    [SerializeField]
    private GameObject jian;
    [SerializeField]
    private GameObject tzq;
    [SerializeField]
    private GameObject[] images;
    [SerializeField]
    private GameObject[] CanNext;
    [SerializeField]
    private GameObject[] Munes;
    Animation ani;
	// Use this for initialization
	void Start () {
        ani = tzq.GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void NewBase()
    {
        images[0].SetActive(false);
        images[1].SetActive(false);
        jian.SetActive(true);
        tzq.SetActive(false);
        Munes[0].SetActive(false);
        Munes[1].SetActive(false);
        Munes[2].SetActive(false);
        Munes[3].SetActive(false);
        level = 0;
       
        CanNext[2].SetActive(true);
        CanNext[0].SetActive(false);
    }
    public void Jians()
    {
        jian.SetActive(false);
        tzq.SetActive(true);
        ani.Play("tzq");
        Munes[0].SetActive(true);
        Munes[1].SetActive(true);
        level++;
    }
    public void NextCase()
    {
        if (level == 1)
        {
            tzq.SetActive(false);
            images[0].SetActive(true);
            Munes[2].SetActive(true);
            level++;
        }
        else if (level==2)
        {
            images[1].SetActive(true);
            images[0].SetActive(false);
            Munes[0].SetActive(false);
            Munes[3].SetActive(true);
            level++;
        }
    }
    public void BaseCase()
    {
        if (level == 2)
        {
            level--;
            images[0].SetActive(false);
            Munes[2].SetActive(false);
            tzq.SetActive(true);
        }
        else if(level==3)
        {
            images[1].SetActive(false);
            images[0].SetActive(true);
            Munes[0].SetActive(true);
            Munes[3].SetActive(false);
            level--;
        }
    }
    public void OverCase()
    {
        CanNext[0].SetActive(false);
        CanNext[1].SetActive(true);
    }
}
