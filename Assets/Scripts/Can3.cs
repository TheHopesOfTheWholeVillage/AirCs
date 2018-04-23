using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can3 : MonoBehaviour {
    [SerializeField]
    GameObject[] CanNext;
    [SerializeField]
    GameObject[] Images;
    [SerializeField]
    GameObject[] Botons;
    int leven;
	// Use this for initialization
	void Start () {
        leven = 0;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ArrCos1()
    {
        if (leven == 0)
        {
            Botons[2].SetActive(true);
            Images[0].SetActive(false);
            Images[1].SetActive(true);
            leven++;
        }else if (leven == 1)
        {
            Images[1].SetActive(false);
            Images[2].SetActive(true);
            leven++;
        }
        else if (leven == 2)
        {
            Images[2].SetActive(false);
            Images[3].SetActive(true);
            leven++;
        }
        else if (leven == 3)
        {
            Images[3].SetActive(false);
            Images[4].SetActive(true);
            leven++;
        }
        else if (leven == 4)
        {
            Botons[0].SetActive(false);
            Botons[2].SetActive(false);
            Images[4].SetActive(false);
            Images[5].SetActive(true);
            Botons[3].SetActive(true);
            leven++;
        }



    }
    public void ArrBose()
    {
        if (leven == 1)
        {

            Botons[2].SetActive(false);
            Images[0].SetActive(true);
            Images[1].SetActive(false);
            leven--;
        }
        else if (leven == 2)
        {
            Images[1].SetActive(true);
            Images[2].SetActive(false);
            leven--;
        }
        else if (leven == 3)
        {
            Images[2].SetActive(true);
            Images[3].SetActive(false);
            leven--;
        }
        else if (leven ==4)
        {
            Images[3].SetActive(true);
            Images[4].SetActive(false);
            leven--;
        }

    }
    public void OverBase()
    {
        CanNext[0].SetActive(false);
        CanNext[1].SetActive(true);
    }
    public void NewBase()
    {
        
        Images[0].SetActive(true);
        Images[1].SetActive(false);
        Images[2].SetActive(false);
        Images[3].SetActive(false);
        Images[4].SetActive(false);
        Images[5].SetActive(false);
        Botons[0].SetActive(true);
        Botons[1].SetActive(true);
        Botons[2].SetActive(false);
        Botons[3].SetActive(false);
        leven = 0;
        CanNext[0].SetActive(false);
        CanNext[2].SetActive(true);
    }
}
