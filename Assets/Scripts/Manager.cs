using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    [SerializeField]
    GameObject[] CanNest;
    [SerializeField]
    GameObject[] menuss;
    [SerializeField]
    private GameObject[] images;
    [SerializeField]
    private int level;
    [SerializeField]
    private GameObject menus;
    [SerializeField]
    private GameObject[] objs;
    [SerializeField]
    private GameObject fzl;
    Animation animafzl;
	// Use this for initialization
	void Start () {
        animafzl = fzl.GetComponent<Animation>();	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Arrows1(int a)
    {
        if (fzl.activeSelf == false)
        {
            fzl.SetActive(true);
        }
        level = 1;
        for (int i = 0; i < objs.Length; i++)
        {
          objs[i].SetActive(false);
        }
        if (menus.activeSelf==false)
        {
            menus.SetActive(true);
        }
        animafzl.Play("fzl");
        menus.transform.localPosition = new Vector3(objs[a].transform.localPosition.x-0.9f, objs[a].transform.localPosition.y +1.1f, objs[a].transform.localPosition.z);
        fzl.transform.localPosition = new Vector3(objs[a].transform.localPosition.x, objs[a].transform.localPosition.y+0.7f, objs[a].transform.localPosition.z);
    }
    public void NextArr()
    {
       
        if (level == 1)
        {
            
            fzl.SetActive(false);
            images[0].SetActive(true);
            level++;
        }
        else if (level == 2)
        {
            
            images[0].SetActive(false);
            images[1].SetActive(true);
            level++;
        }
        else if (level==3)
        {
            
            images[1].SetActive(false);
            images[2].SetActive(true);
            level++;
        }
        else if (level==4)
        {
            menuss[3].SetActive(true);
            menuss[1].SetActive(false);
            images[2].SetActive(false);
            images[3].SetActive(true);
            level++;
        }

    }
    public void BackArr()
    {
        if (level == 1)
        {
            fzl.SetActive(false);
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i].SetActive(true);
            }
            menus.SetActive(false);
            level--;
        }
        else if (level == 2)
        {
            fzl.SetActive(true);
            menus.SetActive(true);
            animafzl.Play("fzl");
            images[0].SetActive(false);
            level--;
        }
        else if (level == 3)
        {
            images[0].SetActive(true);
            images[1].SetActive(false);
            level--;
        }
        else if(level==4)
        {
           
            images[1].SetActive(true);
            images[2].SetActive(false);
            level--;
        }
        else if (level==5)
        {
            menuss[3].SetActive(false);
            menuss[1].SetActive(true);
            images[2].SetActive(true);
            images[3].SetActive(false);
            level--;
        }
    }
    public void SkipArr()
    {
        CanNest[0].SetActive(false);
        CanNest[1].SetActive(true);
    }
    public void NewBase()
    {
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].SetActive(true);
        }
        for (int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(false);
        }
        menuss[0].SetActive(true);
        menuss[1].SetActive(true);
        menuss[2].SetActive(true);
        menuss[3].SetActive(false);
        menus.SetActive(false);
        fzl.SetActive(false);
        level = 0;
        CanNest[0].SetActive(false);
        CanNest[2].SetActive(true);
    }
}
