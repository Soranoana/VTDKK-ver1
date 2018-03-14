using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetWorkSystemOnGui : NetworkBehaviour {

    public string netStatus;
    private GUIStyle style;
    
    void Start () {
        netStatus = "none";
        style = new GUIStyle();
        style.fontSize = 20;
    }
	
	void Update () {
//        Debug.Log(netStatus);
    }
    /*
    //ホストが起動したのを含む、サーバーが起動したときに呼び出されるコールバックです
    public override void OnStartServer() {
        Debug.Log("Start the Server");
        netStatus = "Start the Server";
    }

    //クライアントが起動したときに呼び出されるコールバックです
    [ClientCallback]
    public override void OnStartClient()
    {
        netStatus = "Start the Client";
        Debug.Log("go");
    }

    //	新しいクライアントが接続に成功したときサーバー上で呼び出されます
    void OnServerConnect() {
        netStatus = "サーバー　新しいクライアントが接続に成功";
    }

    //クライアントの接続が失われたか切断されたときにサーバー上で呼び出されます
    void OnServerDisconnect() {
        netStatus = "サーバー　クライアントの接続が失われました";
    }

    //クライアント接続のネットワークエラーが発生したときサーバー上で呼び出されます
    void OnServerError() {
        netStatus = "サーバー　クライアント接続のネットワークエラー";
    }

    //クラインアントの準備が整ったときサーバー上で呼び出されます
    void OnServerReady() {
        netStatus = "サーバー　クラインアント準備完了";
    }

    //サーバーに接続したときクライアント上で呼び出されます
    void OnClientConnect(NetworkConnection conn) {
        netStatus = "クライアント　サーバーに接続";
    }

    //サーバーが切断されたときにクライアント上で呼び出されます
    void OnClientDisconnect(NetworkConnection conn) {
        netStatus = "クライアント　サーバーが切断されました";
    }

    //ネットワークエラーが発生したときにクライアント上で呼び出されます
    void OnClientError() {
        netStatus = "クライアント　ネットワークエラー";
    }

    //サーバーがまだ準備完了していないということを伝えるためにクライアント上で呼び出されます
    void OnClientNotReady() {
        netStatus = "サーバー準備　未完了";
    }

    //ClientScene.AddPlayer によってクライアントで新しいプレイヤーが追加されたときにサーバー上で呼び出されます
    void OnServerAddPlayer() {
        netStatus = "クライアント　新しいプレイヤー追加";
    }*/

    //外部からステータスを変更する用
    public void statusChange(string nextStatus) {
        netStatus = "" + nextStatus;
    }

//    [Server] //サーバー（PC及びLeapMotion側）のみで起動
    void OnGUI(){
//       GUI.TextField(new Rect(10, 430, 300, 100), "Network Status : " + netStatus, style);
    }
}
