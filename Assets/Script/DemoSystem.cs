using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSystem : MonoBehaviour {

    /* ボール非表示問題解消用 */
    public GameObject ball1;
    public GameObject ball2;
    public int set;     //小文字、大文字、数字、記号切り替え用関数
    /* set 0 小文字
     * set 1 大文字
     * set 2 数字記号
     * set 3 エラー
     * */
    public GameObject Canvas;
    DemoGUI demogui;
    public int subset;
    public AudioClip deside;
    public AudioClip delete;
    public AudioClip change;
    public AudioClip clear;
    public AudioClip pose;
    private AudioSource audioSource;
    private logSave logS;
    private TimeCount timeC;

    public int ball1In;
    public int ball2In;

    /* subset 入力の段一覧*/
    //set→ 0       1       2
    //0     abcde   ABCDE   12345    
    //1     fghij   FGHIJ   67890
    //2     klmno   KLMNO   +-*/.
    //3     pqrst   PQRST   @#$%&
    //4     uvwxy   UVWXY   =~^|\
    //5     z'.,_   Z!?()   ;{}`"
    //6     Enter   Enter   :[]<>
    //7     Error   Error   Enter

    /* ログ出力用回数カウント */
    private int BScount = 0;
    private int LockCount = 0;
    private int chageCount = 0;

    /* 一意入力用bool */
    private bool inputtingNow1;
    private bool inputtingNow2;

    /* 機能制限 */
    public bool isAutoSetChange;

    /* CS/AC長押しカウント */
    public int inputtingCSAC = 0;

    /* set配列 */
    public string[,,] stringSet = new string[3, 8, 5] { { {"a","b","c","d","e"},{"f","g","h","i","j"},{"k","l","m","n","o"},{"p","q","r","s","t"},{"u","v","w","x","y"}, {"z","'",".",",","_"},  {"↵\n","↵\n","↵\n","↵\n","↵\n"},{"Error","Error","Error","Error","Error"} },
                                                        { {"A","B","C","D","E"},{"F","G","H","I","J"},{"K","L","M","N","O"},{"P","Q","R","S","T"},{"U","V","W","X","Y"}, {"Z","!","?","(",")"},  {"↵\n","↵\n","↵\n","↵\n","↵\n"},{"Error","Error","Error","Error","Error"} },
                                                        { {"1","2","3","4","5"},{"6","7","8","9","0"},{"+","-","*","/","."},{"@","#","$","%","&"},{"=","~","^","|","\\"},{";","{","}","`","\""}, {":","[","]","<",">"},          {"↵\n","↵\n","↵\n","↵\n","↵\n"}           } };
    /// <summary>
    /// stringSet [ set, subset, text ]
    /// </summary>

    /* ジェスチャー認識用 */
    /* つまむ */
    public GameObject LeapHandController;
    private GameObject rightIndex;
    private GameObject rightThumb;
    private GameObject leftIndex;
    private GameObject leftThumb;
    private bool rightPinch = false;
    private bool leftPinch = false;
    private Vector3 oldRightPosition;
    private Vector3 rightPositionDistance;
    private Vector3 oldLeftPosition;
    private Vector3 leftPositionDistance;

    void Start() {
        demogui = Canvas.GetComponent<DemoGUI>();
        set = 0;
        subset = 0;
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.clip = deside;
        logS = GetComponent<logSave>();
        timeC = GetComponent<TimeCount>();
        inputtingNow1 = false;
        inputtingNow2 = false;
        /* 機能制限 */
        isAutoSetChange = true;
    }

    void Update() {
        ball1.SetActive(true);
        ball2.SetActive(true);
        getFingerFromLeapHands();
        getPinchGesture();
    }

    public void GUIchange(string other, int ballNum) {
        /* BackSpace処理 + 音 */
        if (other == "BSarea") {
            if (demogui.textToEdit.Length >= 2) {
                if (demogui.textToEdit.Substring(demogui.textToEdit.Length - 2, 2) == "↵\n") {
                    demogui.textToEdit = subStringTextToEdit(2);
                } else {
                    demogui.textToEdit = subStringTextToEdit(1);
                }
            } else if (demogui.textToEdit.Length >= 1) {
                demogui.textToEdit = subStringTextToEdit(1);
            }
            audioSource.clip = delete;
            audioSource.Play();
            logS.logSetter(logCreate("BS"));
            BScount++;
        }
        
        /* 段変更処理 + 音 */
        if (other == "changeModeUp") {  //段を一つ上げる   ex. abcde → Enter
            if (subset <= 0) {     //最上段なら最下段へ移動
                if (set <= 1) subset = 6;
                else if (set >= 2) subset = 7;
            } else {
                subset--;
            }
            audioSource.clip = change;
            audioSource.Play();
            logS.logSetter(logCreate("SubSetUP"));
            chageCount++;
        } else if (other == "changeModeDawn") {    //段を一つ下げる   ex. abcde → fghij
            if (set <= 1 && subset >= 6) {
                //最下段なら最上段へ移動
                subset = 0;
            } else if (set >= 2 && subset >= 7) {
                //最下段なら最上段へ移動
                subset = 0;
            } else {
                subset++;
            }
            audioSource.clip = change;
            audioSource.Play();
            logS.logSetter(logCreate("SubSetDOWN"));
            chageCount++;
        }

        /* Change + 音 */
        if (other == "textChange") {
            if (set < 2) {
                set++;
            } else {
                set = 0;
            }
            audioSource.clip = clear;
            audioSource.Play();
            logS.logSetter(logCreate("CSAC"));
            nowSubsetErrorOrNot();
        }
        /* CS/ACカウント及び実行 + 音 */
        if (other=="textChangeInclude") {
            if (inputtingCSAC==0&&demogui.textToEdit.Length>0) {
                inputtingCSAC=1;
            } else {
                inputtingCSAC++;
            }
            if (inputtingCSAC>=30) {
                demogui.textToEdit="";
                logS.logSetter(logCreate("all clear"));
                inputtingCSAC=0;
            }
        }
        /* CS/AC終了 + 音 */
        if (other=="textChangeExit") {
            inputtingCSAC=0;
        }

        /* pose + 音 */
        if (other == "poseButton") {
            audioSource.clip = pose;
            audioSource.Play();
            logS.logSetter(logCreate("Lock"));
            LockCount++;
        }

        /* neutral */
        if (other == "neutral") {
            if (ballNum == 1) {
                inputtingNow1 = false;
            } else if (ballNum == 2) {
                inputtingNow2 = false;
            }
        }

        /* typeKey + 音 */
        if ((!inputtingNow1 && ballNum == 1) || (!inputtingNow2 && ballNum == 2)) {
            if (other == "a") {
                demogui.textToEdit += stringSet[set, subset, 0];
                inputOrNot(true, ballNum);
                if (isAutoSetChange && stringSet[set, subset, 0] == "↵\n") {
                    autoSetChange(1);
                }
                audioSource.clip = deside;
                audioSource.Play();
                logS.logSetter(logCreate(stringSet[set, subset, 0]));
            } else if (other == "b") {
                demogui.textToEdit += stringSet[set, subset, 1];
                inputOrNot(true, ballNum);
                if (isAutoSetChange && stringSet[set, subset, 1] == "↵\n") {
                    autoSetChange(1);
                }
                audioSource.clip = deside;
                audioSource.Play();
                logS.logSetter(logCreate(stringSet[set, subset, 1]));
            } else if (other == "c") {
                demogui.textToEdit += stringSet[set, subset, 2];
                inputOrNot(true, ballNum);
                if (isAutoSetChange && stringSet[set, subset, 2] == "↵\n") {
                    autoSetChange(1);
                }
                audioSource.clip = deside;
                audioSource.Play();
                logS.logSetter(logCreate(stringSet[set, subset, 2]));
            } else if (other == "d") {
                demogui.textToEdit += stringSet[set, subset, 3];
                inputOrNot(true, ballNum);
                if (isAutoSetChange && stringSet[set, subset, 3] == "↵\n") {
                    autoSetChange(1);
                }
                audioSource.clip = deside;
                audioSource.Play();
                logS.logSetter(logCreate(stringSet[set, subset, 3]));
            } else if (other == "e") {
                demogui.textToEdit += stringSet[set, subset, 4];
                inputOrNot(true, ballNum);
                if (isAutoSetChange && stringSet[set, subset, 4] == "↵\n") {
                    autoSetChange(1);
                }
                audioSource.clip = deside;
                audioSource.Play();
                logS.logSetter(logCreate(stringSet[set, subset, 4]));
            }
        }
    }

    public void modeChange(int changer) {
        subset = changer;
    }

    public void inputOrNot(bool Now, int ballNums) {
        if (ballNums == 1) {
            inputtingNow1 = Now;
        } else if (ballNums == 2) {
            inputtingNow2 = Now;
        }
    }

    public string logCreate(string logData) {
        string date, log = "";
        if (demogui.textToEdit.Length > 0) {
            if (demogui.textToEdit.Substring(demogui.textToEdit.Length - 1, 1) == "\n") log = subStringTextToEdit(1);
            else log = demogui.textToEdit;
        }
        date = "Input:\t" + logData + "\tTime:\t" + timeC.getTime() + "\tChageUse:\t" + chageCount + "\tBS Use:\t" + BScount.ToString() + "\tLockUse:\t" + LockCount + "\tLockTime:\t" + timeC.getLockTime() + "\n" + "\tWord:\t" + log;
        return date;
    }

    public int getSubSetNum(int Num) {
        int temp = Num;
        //セットの長さによる処理の違い
        if (set <= 1) {
            while (temp >= 7) {
                temp -= 7;
            }
            return temp;
        } else if (set >= 2) {
            while (temp >= 8) {
                temp -= 8;
            }
            return temp;
        } else {
            return 100;
        }
    }

    public string subStringTextToEdit(int sub) {
        return demogui.textToEdit.Substring(0, demogui.textToEdit.Length - sub);
    }

    private void autoSetChange(int nextSet) {
        /* nextSet Next is..
         * 0       set0 小文字
         * 1       set1 大文字
         * 2       set2 数字
         */
        set = nextSet;
        nowSubsetErrorOrNot();
    }

    private void nowSubsetErrorOrNot(){
        for (int i = 0; i < 5; i++) {
            if (stringSet[set, subset, i] == "Error") {
                subset--;
                nowSubsetErrorOrNot();
                return;
            }
        }
    }

    /* 以下ジェスチャー認識 */
    /* 両手の親指と人差し指取得 */
    private void getFingerFromLeapHands() {
        if (rightIndex==null) {
            rightIndex=LeapHandController.transform.Find("RigidRoundHand_R").transform.Find("index").transform.Find("bone3").gameObject;
        }
        if (rightThumb==null) {
            rightThumb=LeapHandController.transform.Find("RigidRoundHand_R").transform.Find("thumb").transform.Find("bone3").gameObject;
        }
        if (leftIndex==null) {
            leftIndex=LeapHandController.transform.Find("RigidRoundHand_L").transform.Find("index").transform.Find("bone3").gameObject;
        }
        if (leftThumb==null) {
            leftThumb=LeapHandController.transform.Find("RigidRoundHand_L").transform.Find("thumb").transform.Find("bone3").gameObject;
        }
    }

    private void getPinchGesture() {
        if (isNearPosition(rightIndex.transform.position, rightThumb.transform.position)) {
            Debug.Log("right hand is pinchi now");
            if (!rightPinch) {

            }
        }
        if (isNearPosition(leftIndex.transform.position, leftThumb.transform.position)) {
            Debug.Log("left hand is pinchi now");
        }
    }

    private bool isNearPosition(Vector3 point1, Vector3 point2) {
        if (Vector3.Distance(point1, point2)<=0.02f) {
            return true;
        } else {
            return false;
        }
    }
}
