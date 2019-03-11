using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTransSync : MonoBehaviour {

    public GameObject targetCamera;
	void Start () {
		
	}
	
	void Update () {
        transform.rotation = targetCamera.transform.rotation;
        transform.position = targetCamera.transform.position;
	}
}
