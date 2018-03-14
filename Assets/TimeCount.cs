using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//時間アクセス
using UnityEngine.UI;//textアクセス

//時間を数えて、CanvasのTimeTextを更新する
//プロジェクトが始まってからと入力が始まってからの二つをカウントする
public class TimeCount : MonoBehaviour {

    /* ポーズ状態と連動用 */
    private poseMode posemode;
    private float poseTime;//ポーズが始まった時間
    private float poseTimeTemp;//総ポーズ時間の一時保管用
    private float poseTimeSum;//総ポーズ時間
    private bool poseTimeCalc;//総ポーズ時間計算用

    //あとから初期化用
    private bool started;
    private int count;

    /* 指動いた判定用関数 */
    public GameObject fingerBottunL;
    public GameObject fingerBottunR;
    private Vector3 Lposition;
    private Vector3 Rposition;
    private bool moved;
    private bool checkIn;

    private Text timeText;
    private float startTime;
    private float inputtingStartTime;
    private float now;
    private float countTime;
    private float inputtingTime;

	void Start () {
        posemode = GameObject.Find("poseButton").GetComponent<poseMode>();
        poseTime = 0.0f;
        poseTimeTemp = 0.0f;
        poseTimeSum = 0.0f;
        poseTimeCalc = false;
        timeText = gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();

        //起動した瞬間の時間
        startTime = DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60 + DateTime.Now.Second + DateTime.Now.Millisecond * 0.001f;
        countTime = 0.0f;
        inputtingTime = 0.0f;
        count = 0;

        started = false;
        moved = false;
        checkIn = false;
	}

    void Update () {
        if (count >= 10 && !started) {  //動き始めは避けつつ、初期化を終えてない
            startUpForLate();   //遅れて初期化
        }
        count++;
        /* ボールに変化があったか？ */              //条件が長いから分割
        if (Lposition != fingerBottunL.transform.position && started){
            moved = true;//動いた
        } else if (Rposition != fingerBottunR.transform.position && started) {
            moved = true;//動いた
        }

        if (moved && !checkIn) {//動いたけど、入力し始めた時間は未設定
            inputtingStartTime = DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60 + DateTime.Now.Second + DateTime.Now.Millisecond * 0.001f;//入力し始めた時間
            checkIn = true;//入力し始めた時間は設定した
        }

        now = DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60 + DateTime.Now.Second + DateTime.Now.Millisecond *0.001f;//現在時間
        countTime = now - startTime;    //起動時間更新

        if (checkIn) {//動いた後か？
            if (!posemode.pose) {//ポーズ解除状態
                if (poseTimeCalc) {//総ポーズ時間の計算してない
                    poseTimeSum += poseTimeTemp;
                    poseTimeCalc = false;//計算した
                }
                inputtingTime = now - inputtingStartTime - poseTimeSum;   //入力時間更新
                poseTime = now;             //poseが始まるまで現在時間を代入し続ける
            } else if (posemode.pose) {//ポーズ状態
                poseTimeCalc = true;//総ポーズ時間の計算してない
                //inputtingTimeは固定のため更新せず
                if (inputtingTime > 0) {//最初からポーズ時間を計算するのを防ぐ
                    poseTimeTemp = now - poseTime;    //総ポーズ時間
                }
            }
        }
        /* 表示更新 */
        timeText.text = "起動時間：" +  countTime.ToString("N1") + "\t入力時間：" + inputtingTime.ToString("N1");//小数点以下一桁まで表示
    }

    void startUpForLate() {
        Lposition = new Vector3(0, 0, 10);
        Rposition = new Vector3(0, 0, 10);
        started = true;
    }

    public string getTime() {
        return inputtingTime.ToString("N1");
    }

    public string getLockTime() {
        return poseTimeSum.ToString("N1");
    }
}
