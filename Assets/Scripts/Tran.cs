using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tran : MonoBehaviour {
    [SerializeField]
    GameObject Sca;
    [SerializeField]
    GameObject Pos;
    [SerializeField]
    GameObject Rot;
    [SerializeField]
    private GameObject objs;
    [SerializeField]
    private Text text;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        textUI();
    }
    /// <summary>
    /// 
    /// </summary>
    public void ScaSw()
    {
        if (Sca.activeSelf)
        {
            Sca.SetActive(false);
        }
        else
        {
            Sca.SetActive(true);
        }
        Pos.SetActive(false);
        Rot.SetActive(false);
    }
    public void ScaAdd()
    {
        objs.transform.localScale = new Vector3(objs.transform.localScale.x+0.1f, objs.transform.localScale.y+0.1f, objs.transform.localScale.z+0.1f);
    }
    public void ScaRome()
    {
        objs.transform.localScale = new Vector3(objs.transform.localScale.x-0.1f, objs.transform.localScale.y-0.1f, objs.transform.localScale.z-0.1f);
    }

    public void RotXAdd()
    {
        objs.transform.eulerAngles = new Vector3(objs.transform.eulerAngles.x+1f, objs.transform.eulerAngles.y, objs.transform.eulerAngles.z);
    }
    public void RotXRome()
    {
        objs.transform.eulerAngles = new Vector3(objs.transform.eulerAngles.x-1f, objs.transform.eulerAngles.y, objs.transform.eulerAngles.z);
    }
    public void RotYAdd()
    {
        objs.transform.eulerAngles = new Vector3(objs.transform.eulerAngles.x, objs.transform.eulerAngles.y+1f, objs.transform.eulerAngles.z);
    }
    public void RotYRome()
    {
        objs.transform.eulerAngles = new Vector3(objs.transform.eulerAngles.x, objs.transform.eulerAngles.y-1f, objs.transform.eulerAngles.z);
    }
    public void RotZAdd()
    {
        objs.transform.eulerAngles = new Vector3(objs.transform.eulerAngles.x, objs.transform.eulerAngles.y, objs.transform.eulerAngles.z+1f);
    }
    public void RotZRome()
    {
        objs.transform.eulerAngles = new Vector3(objs.transform.eulerAngles.x, objs.transform.eulerAngles.y - 1f, objs.transform.eulerAngles.z-1f);
    }

    public void RotSw()
    {
        if (Rot.activeSelf)
        {
            Rot.SetActive(false);
        }
        else
        {
            Rot.SetActive(true);
        }
        Pos.SetActive(false);
        Sca.SetActive(false);
    }
    public void PosSw()
    {
        if (Pos.activeSelf)
        {
            Pos.SetActive(false);
        }
        else
        {
            Pos.SetActive(true);
        }
        Sca.SetActive(false);
        Rot.SetActive(false);
    }

    public void posX1Add()
    {
        objs.transform.position=new Vector3(objs.transform.position.x+0.1f, objs.transform.position.y, objs.transform.position.z);
    }
    public void posX1Move()
    {
        objs.transform.position = new Vector3(objs.transform.position.x-0.1f, objs.transform.position.y, objs.transform.position.z);
    }
    public void posYAdd()
    {
        objs.transform.position = new Vector3(objs.transform.position.x, objs.transform.position.y+0.1f, objs.transform.position.z);
    }
    public void posYMove()
    {
        objs.transform.position = new Vector3(objs.transform.position.x, objs.transform.position.y-0.1f, objs.transform.position.z);
    }
    public void posZAdd()
    {
        objs.transform.position = new Vector3(objs.transform.position.x, objs.transform.position.y, objs.transform.position.z+0.1f);
    }
    public void posZMove()
    {
        objs.transform.position = new Vector3(objs.transform.position.x, objs.transform.position.y, objs.transform.position.z-0.1f);
    }
    
    public void textUI()
    {
        text.text ="pos"+ objs.transform.position.ToString()+"Rot"+objs.transform.eulerAngles.ToString()+"Sca"+objs.transform.localScale.ToString();
    }
}
