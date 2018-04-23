using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMouse : MonoBehaviour
{
    private const int INT_Constant = 5;
    [SerializeField]

    private bool[] bools=new bool[INT_Constant];
    int ManId;
    [SerializeField]
    private GameObject AnimaObj;
    private AnimationMou AnimaMou;
    // Use this for initialization
    void Start () {
        ManId = 0;
        AnimaMou = AnimaObj.GetComponent<AnimationMou>();


    }
	
	// Update is called once per frame
	void Update () {
        if (ManId == 1&& bools[0]==false)
        {
            AnimaMou.AnimaSw("Overtrun");
            ManId = 0;
            bools[0]=true;
        }
        if (ManId == 2 && bools[1] == false)
        {
            if (AnimaMou.animamou.IsPlaying("Overtrun"))
            {
                AnimaMou.AnimaSw("CellCap");
            }
            else
            {
                AnimaMou.AnimaSw("Overtrun");
                StartCoroutine(Wait());
               
            }
            
            //ObjTrack[0].SetActive(false);
            ManId = 0;
            bools[1] = true;

        }
        if (ManId == 3 && bools[2] == false)
        {
          
       
            AnimaMou.AnimaSw("Cell");
            ManId = 0;
            bools[2] = true;
        }
        if (ManId == 4 && bools[3] == false)
        {
   
            AnimaMou.AnimaSw("ScrewDriver");
            ManId = 0;
            bools[3] = true;
        }
        if (ManId == INT_Constant && bools[4] == false)
        {
            
            AnimaMou.AnimaSw("CurCuit");
            bools[4] = true;
            ManId = 0;
        }

    }
    public void UIChai()
    {
       
        AnimaObj.SetActive(true);
       
    }
    public void ReceiveId(int a)
    {
        ManId = a;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.3f);
        AnimaMou.AnimaSw("CellCap");
    }
}
