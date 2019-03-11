using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class DemoHandFeelOnly : MonoBehaviour {

    DemoSystem demosystem;
    private GameObject canvas;
    public int count1;
    public int count2;
    private Renderer cubeRenderer;
    private Color materialColor;
    public Vector3[] vertices = new Vector3[8];
    Vector3 nowP;
    Vector3 nowS;
    public Vector3 ballPos1;
    public Vector3 ballPos2;
    GameObject ball1;
    GameObject ball2;
    private bool MeshCol = false;
    private int myName;
    /* transform.name               myName
     * a,b,c,d,e                    0
     * changeModeUp                 1
     * changeModeDawn               2
     * textChange,BSarea,poseButton 3
     * neutral                      4
     * Error                        6
     */

    void Start() { 
        canvas = GameObject.Find("Canvas");
        demosystem = canvas.GetComponent<DemoSystem>();
        count1 = 0;
        count2 = 0;
        cubeRenderer = GetComponent<Renderer>();
        materialColor = cubeRenderer.material.color;
        ball1 = GameObject.Find("fingerBottunL");
        ball2 = GameObject.Find("fingerBottunR");
        VerticeSet();

        if (transform.name == "a" || transform.name == "b" || transform.name == "c" || transform.name == "d" || transform.name == "e") {
            myName = 0;
        } else if (transform.name == "changeModeUp") {
            myName = 1;
        } else if (transform.name == "changeModeDawn") {
            myName = 2;
        } else if (transform.name == "textChange" || transform.name == "BSarea" || transform.name == "poseButton") {
            myName = 3;
        } else if (transform.name== "neutral") {
            myName = 4;
        } else {
            //Error
            myName = 6;
        }

        if (transform.name == "poseButton"|| transform.name == "neutral") {
            MeshCol = true;
        }
    }

    void Update() {
        if (MeshCol) {
            ballPosSet();
            //VerticeSet();
            OnMeshStay();   
            OnMeshEnter();
            OnMeshExit();
        }
    }
	
    /* 各頂点座標の初期化 */
    /*
         1_______2          Y     Z
         /      /|          ↑　↗　
        /______/ |          ｜/
       0|     |3 |         ーーー→X
        |     | / 6        /｜
        |_____|/
        4      7
        */
    private void VerticeSet() {
        nowP = transform.position;
        nowS = transform.lossyScale;

        vertices[0] = nowP + new Vector3(-nowS.x / 2,  nowS.y / 2, -nowS.z / 2);
        vertices[1] = nowP + new Vector3(-nowS.x / 2,  nowS.y / 2,  nowS.z / 2);
        vertices[2] = nowP + new Vector3( nowS.x / 2,  nowS.y / 2,  nowS.z / 2);
        vertices[3] = nowP + new Vector3( nowS.x / 2,  nowS.y / 2, -nowS.z / 2);
        vertices[4] = nowP + new Vector3(-nowS.x / 2, -nowS.y / 2, -nowS.z / 2);
        vertices[5] = nowP + new Vector3(-nowS.x / 2, -nowS.y / 2,  nowS.z / 2);
        vertices[6] = nowP + new Vector3( nowS.x / 2, -nowS.y / 2,  nowS.z / 2);
        vertices[7] = nowP + new Vector3( nowS.x / 2, -nowS.y / 2, -nowS.z / 2);
    }

    private void ballPosSet() {
        if (myName == 0) {
            //a,b,c,d,e
            ballPos1 = ball1.transform.position - new Vector3(0, ball1.transform.localScale.y / 2.0f, 0);
            ballPos2 = ball2.transform.position - new Vector3(0, ball2.transform.localScale.y / 2.0f, 0);
        } else if (myName == 1) {
            //changeModeUp
            ballPos1 = ball1.transform.position - new Vector3(ball1.transform.localScale.x / 2.0f, 0, 0);
            ballPos2 = ball2.transform.position - new Vector3(ball2.transform.localScale.x / 2.0f, 0, 0);
        } else if (myName == 2) {
            //changeModeDawn
            ballPos1 = ball1.transform.position + new Vector3(ball1.transform.localScale.x / 2.0f, 0, 0);
            ballPos2 = ball2.transform.position + new Vector3(ball2.transform.localScale.x / 2.0f, 0, 0);
        } else if (myName == 3) {
            //textChange,BSarea,poseButton
            ballPos1 = ball1.transform.position + new Vector3(0, ball1.transform.localScale.y / 2.0f, 0);
            ballPos2 = ball2.transform.position + new Vector3(0, ball2.transform.localScale.y / 2.0f, 0);
        } else if (myName == 4) {
            ballPos1 = ball1.transform.position;
            ballPos2 = ball2.transform.position;
        } else {
            //Error
            return;
        }
    }

    /* 座標判定 */
    /* 自分の座標内にballが入ったらdemosystemにフラグを送る
     * まずはデバッグログを流す
     * 判定は個別に必要かも
     */
    private void OnMeshEnter() {
        /* ball1についての当たり判定 */
        /* X方向 */
        if (ballPos1.x > vertices[0].x) {
            if (ballPos1.x < vertices[3].x) {
                /* Y方向 */
                if (ballPos1.y < vertices[0].y) {
                    if (ballPos1.y > vertices[4].y) {
                        /* 最初の一回だけ反応 */
                        if (count1 == 0) {
                            //demosystemに引数transform.nameでカウントを送る
                            demosystem.GUIchange(transform.name,1);
                            //Debug.Log(transform.name + " touch");
                            //手に触った
                            count1 = 1;
                            colorChange(true);
                        }
                    }
                }
            }
        }
        /* ball2についての当たり判定 */
        /* X方向 */
        if (ballPos2.x > vertices[0].x) {
            if (ballPos2.x < vertices[3].x) {
                /* Y方向 */
                if (ballPos2.y < vertices[0].y) {
                    if (ballPos2.y > vertices[4].y) {
                        /* 最初の一回だけ反応 */
                        if (count2 == 0) {
                            //demosystemに引数transform.nameでカウントを送る
                            demosystem.GUIchange(transform.name,2);
                            Debug.Log(transform.name + " touch");
                            //手に触った
                            count2 = 1;
                            colorChange(true);
                        }
                    }
                }
            }
        }
    }

    private void OnMeshStay() {
        /* ball1についての当たり判定 */
        /* X方向 */
        if (ballPos1.x>vertices[0].x) {
            if (ballPos1.x<vertices[3].x) {
                /* Y方向 */
                if (ballPos1.y<vertices[0].y) {
                    if (ballPos1.y>vertices[4].y) {
                        if (count1==1) {
                            //demosystemに引数transform.nameでカウントを送る
                            demosystem.GUIchange(transform.name+"Include", 1);
                            Debug.Log(transform.name+" include");
                        }
                    }
                }
            }
        }
        /* ball2についての当たり判定 */
        /* X方向 */
        if (ballPos2.x>vertices[0].x) {
            if (ballPos2.x<vertices[3].x) {
                /* Y方向 */
                if (ballPos2.y<vertices[0].y) {
                    if (ballPos2.y>vertices[4].y) {
                        if (count2==1) {
                            //demosystemに引数transform.nameでカウントを送る
                            demosystem.GUIchange(transform.name + "Include", 2);
                            //Debug.Log(transform.name+" include");
                        }
                    }
                }
            }
        }
    }

    private void OnMeshExit() {
        /* ball1についての当たり判定 */
        /* X方向 */
        if (ballPos1.x < vertices[0].x|| ballPos1.x > vertices[3].x) {
            if (count1 >= 1) {
                //demosystemに引数transform.nameでカウントを送る
                demosystem.GUIchange(transform.name + "Exit",1);
                //手に触った状態終了
                count1 = 0;
                colorChange(false);
            }
        }
        /* Y方向 */
        if (ballPos1.y > vertices[0].y || ballPos1.y < vertices[4].y) {
            if (count1 >= 1) {
                //demosystemに引数transform.nameでカウントを送る
                demosystem.GUIchange(transform.name + "Exit",1);
                //手に触った状態終了
                count1 = 0;
                colorChange(false);
            }
        }

        /* ball2についての当たり判定 */
        /* X方向 */
        if (ballPos2.x < vertices[0].x || ballPos2.x > vertices[3].x) {
            if (count2 >= 1) {
                //demosystemに引数transform.nameでカウントを送る
                demosystem.GUIchange(transform.name + "Exit",2);
                //手に触った状態終了
                count2 = 0;
                colorChange(false);
            }
        }
        /* Y方向 */
        if (ballPos2.y > vertices[0].y || ballPos2.y < vertices[4].y) {
            if (count2 >= 1) {
                //demosystemに引数transform.nameでカウントを送る
                demosystem.GUIchange(transform.name + "Exit",2);
                //手に触った状態終了
                count2 = 0;
                colorChange(false);
            }
        }
    }

    /* 触れられたら色を変える。終わったら戻す *
     * stringはtrue時のオブジェクトによっての色変え用
     * trueは色を変える
     * falseは色を戻す
     */
    private void colorChange(bool change) {
        if (change) {
            cubeRenderer.material.SetColor("_Color", new Color(1f, 0, 0, 0.5f));
        } else {
            cubeRenderer.material.color = materialColor;
        }
    }

    public void MeshColChange(bool change) {
        MeshCol = change;
    }
}
