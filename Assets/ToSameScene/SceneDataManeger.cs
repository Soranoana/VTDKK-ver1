using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* スクリプトの役割
 * シーン間のシーンデータの管理
 * シーンに入った際の余分なオブジェクト、コンポーネントの削除
 * シーン移動ボタンの管理
 */
public class SceneDataManeger : MonoBehaviour {

    public GameObject finger1;
	public GameObject finger2;

	void Start () {
		if (SceneManager.GetActiveScene ().name == "mode_select v1.1") {
			PlayerPrefs.SetInt ("HostOrClient", 0);
			/* HostOrClient is ...
			 * 0 select now
			 * 1 Host now
			 * 2 Client now (demonstration)
			 * 3 Host Keyboard now
             * 4 Client No DataBack To Host now
             * 5 Client now (tasking now)
			 */
		}

		if (PlayerPrefs.GetInt("HostOrClient")==1) {	//ホスト
			GameObject[] destoryOnHost = GameObject.FindGameObjectsWithTag("DestroyOnHost");
			foreach (GameObject DesObject in destoryOnHost) {
				Destroy (DesObject);
            }
			Destroy (finger1.GetComponent<testArrowControl> ());
			Destroy (finger2.GetComponent<testArrowControl> ());
            Destroy(GetComponent<logSave>());
        } else if (PlayerPrefs.GetInt("HostOrClient")==2) {	//クライアント　バニラ
			GameObject[] destoryOnClient = GameObject.FindGameObjectsWithTag("DestroyOnClient");
			foreach (GameObject DesObject in destoryOnClient) {
				Destroy (DesObject);
            }
            GameObject[] destoryOnClientUI = GameObject.FindGameObjectsWithTag("UI");
            foreach (GameObject DesObject in destoryOnClientUI) {
                Destroy(DesObject);
            }
            Destroy(finger1.GetComponent<testArrowControl>());
            Destroy(finger2.GetComponent<testArrowControl>());
            Destroy(GameObject.Find("demoText"));
            Destroy(GameObject.Find("pracText"));
        } else if (PlayerPrefs.GetInt("HostOrClient")==3) {	//ホストがキーボード
			GameObject[] destoryOnHost = GameObject.FindGameObjectsWithTag("DestroyOnHost");
			foreach (GameObject DesObject in destoryOnHost) {
				Destroy (DesObject);
			}
            Destroy(finger1.GetComponent<fingerBottun>());
            Destroy(finger2.GetComponent<fingerBottun>());
            Destroy(GetComponent<logSave>());
        } else if (PlayerPrefs.GetInt("HostOrClient") == 4){   //クライアント　キーボード入力
            GameObject[] destoryOnClient = GameObject.FindGameObjectsWithTag("DestroyOnClient");
            foreach (GameObject DesObject in destoryOnClient) {
                Destroy(DesObject);
            }
            GameObject[] destoryOnClientUI = GameObject.FindGameObjectsWithTag("UI");
            foreach (GameObject DesObject in destoryOnClientUI) {
                Destroy(DesObject);
            }
            Destroy(GameObject.Find("demoText"));
            Destroy(GameObject.Find("pracText"));
            Destroy(GetComponent<logSave>());
        } else if (PlayerPrefs.GetInt("HostOrClient")==5) {	//クライアント　本タスク
			GameObject[] destoryOnClient = GameObject.FindGameObjectsWithTag("DestroyOnClient");
			foreach (GameObject DesObject in destoryOnClient) {
				Destroy (DesObject);
            }
            GameObject[] destoryOnClientUI = GameObject.FindGameObjectsWithTag("UI");
            foreach (GameObject DesObject in destoryOnClientUI) {
                Destroy(DesObject);
            }
            Destroy(finger1.GetComponent<testArrowControl>());
            Destroy(finger2.GetComponent<testArrowControl>());
            Destroy(GameObject.Find("pracText"));
        } else if (PlayerPrefs.GetInt("HostOrClient")==6) {	//クライアント　練習タスク
			GameObject[] destoryOnClient = GameObject.FindGameObjectsWithTag("DestroyOnClient");
			foreach (GameObject DesObject in destoryOnClient) {
				Destroy (DesObject);
            }
            GameObject[] destoryOnClientUI = GameObject.FindGameObjectsWithTag("UI");
            foreach (GameObject DesObject in destoryOnClientUI) {
                Destroy(DesObject);
            }
            Destroy(finger1.GetComponent<testArrowControl>());
            Destroy(finger2.GetComponent<testArrowControl>());
            Destroy(GameObject.Find("demoText"));
        }
    }

    public void GetButtonDownToHost(){
		PlayerPrefs.SetInt ("HostOrClient", 1);
		SceneManager.LoadScene ("v1.5 SystemPlay_Leap_Hands_Demo_Desktop");
	}

	public void GetButtonDownToClient(){
		PlayerPrefs.SetInt ("HostOrClient", 2);
		SceneManager.LoadScene ("v1.5 SystemPlay_Leap_Hands_Demo_Desktop");
	}

	public void GetButtonDownToHostKeyboard(){
		PlayerPrefs.SetInt ("HostOrClient", 3);
		SceneManager.LoadScene ("v1.5 SystemPlay_Leap_Hands_Demo_Desktop");
	}

    public void GetButtonDownToClientNoback() {
        PlayerPrefs.SetInt("HostOrClient", 4);
        SceneManager.LoadScene("v1.5 SystemPlay_Leap_Hands_Demo_Desktop");
    }

    public void GetButtonDownToClientTask() {
        PlayerPrefs.SetInt("HostOrClient", 5);
        SceneManager.LoadScene("v1.5 SystemPlay_Leap_Hands_Demo_Desktop");
    }

    public void GetButtonDownToClientPractice() {
        PlayerPrefs.SetInt("HostOrClient", 6);
        SceneManager.LoadScene("v1.5 SystemPlay_Leap_Hands_Demo_Desktop");
    }
}
