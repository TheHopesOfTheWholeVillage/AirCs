using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

    private static int i = 0;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void OnClick() {

        i++;
        if (i % 2 != 0)
        {

            changeRed();
        }
        else {

            changeGreen();
        }
    }

    public void changeRed() {

        transform.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public void changeGreen() {

        transform.GetComponent<MeshRenderer>().material.color = Color.green;

    }
}
