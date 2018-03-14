using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textSetOn3d : MonoBehaviour {

    public GameObject canvas;
    private DemoSystem demosystem;
    private string dispText;

    void Start () {
        demosystem = canvas.GetComponent<DemoSystem>();
    }
	
	void Update () {
        for (int i = 1; i < 9; i++) {　
            if (transform.name == "textSetArea" + i) {
                dispText = "";
                int ii = i;
                //setが0,1のときはtextSetArea4,5は使わないので飛ばして処理
                if (demosystem.set <= 1) {
                    //iが4,5のときはそもそも処理を行わずスキップ
                    if (i == 4 || i == 5) goto breakPoint;
                    //飛ばしたらその分iを低くしておく
                    else if (i > 5) ii = i - 2;
                } else if (demosystem.set >= 2) {
                    if (i > 4) ii--;
                }

                for (int j = 0; j < 5; j++) {
                    dispText += demosystem.stringSet[demosystem.set, demosystem.getSubSetNum(demosystem.subset + ii), j];
                    if (dispText.Substring(dispText.Length - 1, 1) == "\n") {
                        dispText = dispText.Substring(0, dispText.Length - 1);
                    }
                }
                breakPoint:;
            }
        }
        transform.GetComponent<Text>().text = dispText;
    }
}
