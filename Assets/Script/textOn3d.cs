using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textOn3d : MonoBehaviour {

    public GameObject canvas;
    DemoSystem demosystem;
    
	void Start () {
        demosystem = canvas.GetComponent<DemoSystem>();
    }

    void Update () {
        if (transform.name == "aText") {
            transform.GetComponent<Text>().text = demosystem.stringSet[demosystem.set, demosystem.subset, 0];
        } else if (transform.name == "bText") {
            transform.GetComponent<Text>().text = demosystem.stringSet[demosystem.set, demosystem.subset, 1];
        } else if (transform.name == "cText") {
            transform.GetComponent<Text>().text = demosystem.stringSet[demosystem.set, demosystem.subset, 2];
        } else if (transform.name == "dText") {
            transform.GetComponent<Text>().text = demosystem.stringSet[demosystem.set, demosystem.subset, 3];
        } else if (transform.name == "eText") {
            transform.GetComponent<Text>().text = demosystem.stringSet[demosystem.set, demosystem.subset, 4];
        }
    }
}
