using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tran : MonoBehaviour {
    [SerializeField]
    private GameObject[] objs;
    [SerializeField]
    private Text text;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        textUI();
    }
    public void posX1Add()
    {
        objs[1].transform.position=new Vector3(objs[1].transform.position.x+0.1f, objs[1].transform.position.y, objs[1].transform.position.z);
    }
    public void posX1Move()
    {
        objs[1].transform.position = new Vector3(objs[1].transform.position.x-0.1f, objs[1].transform.position.y, objs[1].transform.position.z);
    }
    public void posX2Add()
    {
        objs[2].transform.position = new Vector3(objs[2].transform.position.x + 0.1f, objs[2].transform.position.y, objs[2].transform.position.z);
    }
    public void posX2Move()
    {
        objs[2].transform.position = new Vector3(objs[2].transform.position.x -0.1f, objs[2].transform.position.y, objs[2].transform.position.z);
    }
    public void posX3Add()
    {
        objs[3].transform.position = new Vector3(objs[3].transform.position.x + 0.1f, objs[3].transform.position.y, objs[3].transform.position.z);
    }
    public void posX3Move()
    {
        objs[3].transform.position = new Vector3(objs[3].transform.position.x -0.1f, objs[3].transform.position.y, objs[3].transform.position.z);
    }
    public void posYAdd()
    {
        objs[0].transform.position = new Vector3(objs[0].transform.position.x, objs[0].transform.position.y+0.1f, objs[0].transform.position.z);
    }
    public void posYMove()
    {
        objs[0].transform.position = new Vector3(objs[0].transform.position.x, objs[0].transform.position.y-0.1f, objs[0].transform.position.z);
    }
    public void posZAdd()
    {
        objs[0].transform.position = new Vector3(objs[0].transform.position.x+0.1f, objs[0].transform.position.y, objs[0].transform.position.z);
    }
    public void posZMove()
    {
        objs[0].transform.position = new Vector3(objs[0].transform.position.x - 0.1f, objs[0].transform.position.y, objs[0].transform.position.z);
    }
    public void textUI()
    {
        text.text = objs[0].transform.position.ToString()+"/"+ objs[1].transform.position.ToString() + "/"+objs[2].transform.position.ToString() + "/"+objs[3].transform.position.ToString();
    }
}
