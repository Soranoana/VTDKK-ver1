using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fingerBottun : MonoBehaviour {

	public GameObject LeapHandController;
    public GameObject finger;
    private GameObject currentHand;
    private int missFrame=0;          //コマ落ちカウント
    private bool fingerGetted = false;
    //矢印キー用
    private float idou = 0.02f;
    private Vector3 currentPositionR = new Vector3(0, 0, 0.1f);
    private Vector3 currentPositionL = new Vector3(0, 0, 0.1f);
    private bool arrowCotroll=false;
    //Hand操作用
    private bool handControll = false;

    //ComputeShader用
    [SerializeField]
    private ComputeShader m_ComputeShader1;
    [SerializeField]
    private ComputeShader m_ComputeShader2;
    private ComputeBuffer m_Buffer1;
    private ComputeBuffer m_Buffer2;
    public bool enableComputeShader;

    void Start () {
        transform.position=new Vector3(0, 100, 0);
        GetVectorFromLeapHands();
		StartForComputeShader();
    }
	
	void FixedUpdate () {
        if (!handControll) {
            GetVecrtorFromArrowKey();
        }
        if (!arrowCotroll) {
            if(!enableComputeShader)GetVectorFromLeapHands();
            else UpdateForComputeShader();
        }
    }

    private void GetVectorFromLeapHands() {
        if (!fingerGetted) {
            if(transform.name== "fingerBottunL") currentHand = LeapHandController.transform.Find("RigidRoundHand_L").gameObject;
            if(transform.name== "fingerBottunR") currentHand = LeapHandController.transform.Find("RigidRoundHand_R").gameObject;
            currentHand = currentHand.transform.Find("index").transform.Find("bone3").gameObject;
            finger = currentHand;
            //transform.parent = currentHand.transform;
            //transform.localPosition = Vector3.zero;
            fingerGetted = true;
        }/*
        if (transform.name == "fingerBottunL" && !fingerGetted) {
//			currentHand = LeapHandController.transform.Find("RigidRoundHand_L").gameObject;
//            finger = currentHand.transform.Find("index").transform.Find("bone3").gameObject;
            transform.parent = finger.transform;
            transform.localPosition = Vector3.zero;
            fingerGetted = true;
        } else if (transform.name == "fingerBottunR" && !fingerGetted) {
//			currentHand =  LeapHandController.transform.Find("RigidRoundHand_R").gameObject;
//            finger = currentHand.transform.Find("index").transform.Find("bone3").gameObject;
            transform.parent = finger.transform;
            transform.localPosition=Vector3.zero;
            fingerGetted = true;
        }*/
        if (currentHand.activeInHierarchy) {
            transform.position = finger.transform.position;
            missFrame = 0;
            handControll=true;
        } else {
            handControll=false;
            if (missFrame <= 100) { 
                missFrame++;
            }else if (missFrame > 5) {
                transform.position = new Vector3(0, 10, 0);
            }
        }

    }

    private void GetVecrtorFromArrowKey() {
        if (transform.name == "fingerBottunR") {
            if (Input.GetKey(KeyCode.UpArrow)) {
                arrowCotroll = true;
                transform.position = currentPositionR;
                transform.position += new Vector3(0, idou, 0);
                currentPositionR = transform.position;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                arrowCotroll = true;
                transform.position = currentPositionR;
                transform.position -= new Vector3(0, idou, 0);
                currentPositionR = transform.position;
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                arrowCotroll = true;
                transform.position = currentPositionR;
                transform.position += new Vector3(idou, 0, 0);
                currentPositionR = transform.position;
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                arrowCotroll = true;
                transform.position = currentPositionR;
                transform.position -= new Vector3(idou, 0, 0);
                currentPositionR = transform.position;
            }
        } else if (transform.name == "fingerBottunL") {
            if (Input.GetKey(KeyCode.W)) {
                arrowCotroll = true;
                transform.position = currentPositionL;
                transform.position += new Vector3(0, idou, 0);
                currentPositionL = transform.position;
            }
            if (Input.GetKey(KeyCode.S)) {
                arrowCotroll = true;
                transform.position = currentPositionL;
                transform.position -= new Vector3(0, idou, 0);
                currentPositionL = transform.position;
            }
            if (Input.GetKey(KeyCode.D)) {
                arrowCotroll = true;
                transform.position = currentPositionL;
                transform.position += new Vector3(idou, 0, 0);
                currentPositionL = transform.position;
            }
            if (Input.GetKey(KeyCode.A)) {
                arrowCotroll = true;
                transform.position = currentPositionL;
                transform.position -= new Vector3(idou, 0, 0);
                currentPositionL = transform.position;
            }
        }
    }

    private void StartForComputeShader() {
        if (transform.name=="fingerBottunL") {
            m_Buffer1=new ComputeBuffer(6, sizeof(float));
            m_ComputeShader1.SetBuffer(0, "Result", m_Buffer1);
        } else if (transform.name=="fingerBottunR") {
            m_Buffer2=new ComputeBuffer(6, sizeof(float));
            m_ComputeShader2.SetBuffer(0, "Result", m_Buffer2);
        }
    }

    private void UpdateForComputeShader() {
        if (transform.name=="fingerBottunL") {
            m_ComputeShader1.SetFloat("positionX", finger.transform.position.x);
            m_ComputeShader1.SetFloat("positionY", finger.transform.position.y);
            m_ComputeShader1.SetFloat("positionZ", finger.transform.position.z);
            m_ComputeShader1.Dispatch(0, 8, 8, 1);
            var data = new float[3];
            m_Buffer1.GetData(data);
            transform.position=new Vector3(data[0], data[1], data[2]);
        } else if (transform.name=="fingerBottunR") {
            m_ComputeShader2.SetFloat("positionX", finger.transform.position.x);
            m_ComputeShader2.SetFloat("positionY", finger.transform.position.y);
            m_ComputeShader2.SetFloat("positionZ", finger.transform.position.z);
            m_ComputeShader1.Dispatch(0, 8, 8, 1);
            var data = new float[3];
            m_Buffer2.GetData(data);
            transform.position=new Vector3(data[0], data[1], data[2]);
        }
    }

    private void OnDestroy() {
        if (transform.name=="fingerBottunL") {
            m_Buffer1.Release();
        } else if (transform.name=="fingerBottunR") {
            m_Buffer2.Release();
        }
    }
}
