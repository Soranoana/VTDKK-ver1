using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class modeSelectButton : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    /*　シーン番号　*/
    //mode_select                                   0
    //Leap_Hands_Demo_AR                            1
    //Leap_Hands_Demo_Desktop                       2
    //Leap_Hands_Demo_VR                            3
    //v1.1Leap_Hands_Demo_Desktop                   4
    //v1.2 ARChallenge Leap_Hands_Demo_Desktop      5
    //v1.3a ClientLeap_Hands_Demo_Desktop           6
    //v1.3b HostLeap_Hands_Demo_Desktop             7
    //v1.4a ClientLeap_Hands_Demo_Desktop           8
    //v1.4b HostLeap_Hands_Demo_Desktop             9

    public void GoHost() {
        SceneManager.LoadScene("v1.4b HostLeap_Hands_Demo_Desktop");
    }

    public void GoClient() {
        SceneManager.LoadScene("v1.4a ClientLeap_Hands_Demo_Desktop");
    }
}
