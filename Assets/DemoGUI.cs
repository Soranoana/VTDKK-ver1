using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoGUI : MonoBehaviour {

    public string textToEdit;
    public GameObject dispText;

    void Start () {
        textToEdit = "";
    }
	
	void Update () {
        if (PlayerPrefs.GetInt("HostOrClient") == 2 || PlayerPrefs.GetInt("HostOrClient") == 4 ||
            PlayerPrefs.GetInt("HostOrClient") == 5 || PlayerPrefs.GetInt("HostOrClient") == 6) {
            dispText.GetComponent<Text>().text = textToEdit;
        }
    }

    public void textClear() {
        textToEdit = "";
    }

    public void textUpadate(string text) {
        textToEdit = text;
    }
}
