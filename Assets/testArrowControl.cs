using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testArrowControl : MonoBehaviour {

    private Vector3 startVec = new Vector3(0, 0, 0.3f);
    private float idou = 0.01f;
    public GameObject opeObj;
    private networkSystemOperation opeSys;
    public GameObject LeapHandController;

    void Start () {
        transform.position = startVec;
        opeSys = opeObj.GetComponent<networkSystemOperation>();

        if (transform.name == "fingerBottunL") {
            opeSys.finger1Vec = this.gameObject.transform.position;          //サーバー側で玉の座標を保存
        } else if (transform.name == "fingerBottunR") {
            opeSys.finger2Vec = this.gameObject.transform.position;          //サーバー側で玉の座標を保存
        }
    }
	
	void Update () {
        if (transform.name == "fingerBottunR") {
            if (Input.GetKey(KeyCode.UpArrow)) {
                transform.position += new Vector3(0, idou, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                transform.position -= new Vector3(0, idou, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                transform.position += new Vector3(idou, 0, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                transform.position -= new Vector3(idou, 0, 0);
            }
        } else if (transform.name == "fingerBottunL") {
            if (Input.GetKey(KeyCode.W)) {
                transform.position += new Vector3(0, idou, 0);
            }
            if (Input.GetKey(KeyCode.S)) {
                transform.position -= new Vector3(0, idou, 0);
            }
            if (Input.GetKey(KeyCode.D)) {
                transform.position += new Vector3(idou, 0, 0);
            }
            if (Input.GetKey(KeyCode.A)) {
                transform.position -= new Vector3(idou, 0, 0);
            }
        }
        OnServerGetVectorForLeapHands();
    }

    private void OnServerGetVectorForLeapHands() {
        if (transform.name == "fingerBottunL") {
            opeSys.finger1Vec = this.gameObject.transform.position;          //サーバー側で玉の座標を保存
        } else if (transform.name == "fingerBottunR") {
            opeSys.finger2Vec = this.gameObject.transform.position;          //サーバー側で玉の座標を保存
        }
    }
}
