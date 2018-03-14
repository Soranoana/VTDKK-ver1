using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
/*      重要　　   */
using UnityEngine.Networking;

public class networkSystemOperation : NetworkBehaviour {
    //実行フラグ
	[SyncVar]
	public int remote;
    /*0	Null
	 *1	remoteDelete
	 *2	remoteSetToStart
	 *3	remotePoseMode
	 *4	remoteActiveMode
	 *5	remoteDemoInput
	 */

    private DemoGUI demogui;
    private DemoSystem demosystem;
    private poseMode posemode;
    private GameObject remoteText;

	/* ネットワーク同期変数 */
	[SyncVar]
	public Vector3 finger1Vec;
	[SyncVar]
	public Vector3 finger2Vec;

    void Start () {
		remote = 1;

        if (PlayerPrefs.GetInt("HostOrClient") == 2 || PlayerPrefs.GetInt("HostOrClient") == 4 ||
            PlayerPrefs.GetInt("HostOrClient") == 5 || PlayerPrefs.GetInt("HostOrClient") == 6) {
            demogui = GameObject.Find("Canvas").GetComponent<DemoGUI>();
            demosystem = GameObject.Find("Canvas").GetComponent<DemoSystem>();
            posemode = GameObject.Find("poseButton").GetComponent<poseMode>();
        }
        if (PlayerPrefs.GetInt("HostOrClient") == 1 || PlayerPrefs.GetInt("HostOrClient") == 3) {
            remoteText = GameObject.Find("Canvas").transform.Find("remoteInputField").transform.Find("Text").gameObject;
        }

    }
	
	void Update () {
		if (PlayerPrefs.GetInt("HostOrClient")==2) {
			if (remote==1) {
                deleteText();
			}
			if (remote==2) {
				setToStart();
			}
			if (remote==3) {
				poseMode();
			}
			if (remote==4) {
				activeMode();
			}
			if (remote==5) {
				demoInput();
			}
        }
    }

    public void flgToTrue(string name) {
		if (PlayerPrefs.GetInt("HostOrClient") == 2) {
            if (name == "remoteDelete") {
				remote = 1;
            } else if (name == "remoteSetToStart") {
				remote = 2;
            } else if (name == "remotePoseMode") {
				remote = 3;
            } else if (name == "remoteActiveMode") {
				remote = 4;
            } else if (name == "remoteDemoInput") {
				remote = 5;
            }
        }
    }

    private void deleteText() {
		remote = 0;
        demogui.textClear();
    }

    private void setToStart() {
		remote = 0;
		demosystem.modeChange(0);
    }

    private void poseMode() {
		remote = 0;
		posemode.modeChange(true);
    }

    private void activeMode() {
		remote = 0;
        posemode.modeChange(false);
    }

    private void demoInput() {
		remote = 0;
        demogui.textUpadate(remoteText.GetComponent<Text>().text);
        remoteText.GetComponent<Text>().text = "";
    }
}
