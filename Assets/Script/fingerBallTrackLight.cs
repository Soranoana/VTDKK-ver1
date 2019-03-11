using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fingerBallTrackLight : MonoBehaviour {

    public GameObject fingerball1;
    public GameObject fingerball2;
    private Vector3 startVector;

    void Start () {
        startVector = transform.position;
	}
	
	void Update () {
        if (transform.name == "SpotlightL") {
            transform.position = new Vector3(fingerball1.transform.position.x, startVector.y, fingerball1.transform.position.z);
        } else if (transform.name == "SpotlightR") {
            transform.position = new Vector3(fingerball2.transform.position.x, startVector.y, fingerball2.transform.position.z);
        }
    }
}
