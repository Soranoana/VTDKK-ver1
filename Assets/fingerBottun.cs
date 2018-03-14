using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fingerBottun : MonoBehaviour {

	public GameObject opeObj;
	private networkSystemOperation opeSys;
	public GameObject LeapHandController;
    private GameObject finger;
    private GameObject hand;        //右手か左手か
    private GameObject currentHand;
    private int missFrame=0;          //コマ落ちカウント

    void Start () {
		opeSys = opeObj.GetComponent<networkSystemOperation> ();

        transform.position = new Vector3(0, 0, 0.5f);

        if (transform.name == "fingerBottunL") {
			opeSys.finger1Vec = this.gameObject.transform.position;          //サーバー側で玉の座標を保存
        } else if (transform.name == "fingerBottunR") {
			opeSys.finger2Vec = this.gameObject.transform.position;          //サーバー側で玉の座標を保存
        }
    }
	
	void Update () {
		//ホストなら
		if (PlayerPrefs.GetInt("HostOrClient")==1)
            OnServerGetVectorForLeapHands();
        if (PlayerPrefs.GetInt("HostOrClient") == 3)
            return;            //キーボード入力のために初期化不要
        //クライアントなら
        if (PlayerPrefs.GetInt("HostOrClient") == 2)
            OnClientGetVectorForServer();
        if (PlayerPrefs.GetInt("HostOrClient") == 4)
            return;
        if (PlayerPrefs.GetInt("HostOrClient") == 5)
            OnClientGetVectorForServer();
        if (PlayerPrefs.GetInt("HostOrClient") == 6)
            OnClientGetVectorForServer();
    }

    private void OnServerGetVectorForLeapHands() {
        if (transform.name == "fingerBottunL") {
			currentHand = LeapHandController.transform.Find("RigidRoundHand_L").gameObject;
            finger = currentHand.transform.Find("index").transform.Find("bone3").gameObject;
        } else if (transform.name == "fingerBottunR") {
			currentHand =  LeapHandController.transform.Find("RigidRoundHand_R").gameObject;
            finger = currentHand.transform.Find("index").transform.Find("bone3").gameObject;
        }

        if ( currentHand.activeSelf) {
            transform.position = finger.transform.position;
            missFrame = 0;
        } else {
            missFrame++;
            if (missFrame > 5) {
                transform.position = new Vector3(0, 10, 0);
            }
        }

        if (transform.name == "fingerBottunL") {
			opeSys.finger1Vec = this.gameObject.transform.position;          //サーバー側で玉の座標を保存
        } else if (transform.name == "fingerBottunR") {
			opeSys.finger2Vec = this.gameObject.transform.position;          //サーバー側で玉の座標を保存
        }
    }

    private void OnClientGetVectorForServer() {
		if (transform.name == "fingerBottunL") {
			this.gameObject.transform.position = opeSys.finger1Vec;          //クライアント側で玉の座標を保存
        } else if (transform.name == "fingerBottunR") {
			this.gameObject.transform.position = opeSys.finger2Vec;          //クライアント側で玉の座標を保存
        }
    }
}
