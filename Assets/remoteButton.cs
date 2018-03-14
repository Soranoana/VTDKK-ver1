using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class remoteButton : MonoBehaviour {

    private GameObject netSystemOpe;
    /* センサー上下用 */
    private GameObject senser;
    private float senserF;
    private float senserFmoto;

	void Start () {
        senser = GameObject.Find("Camera").transform.Find("LMHeadMountedRig").gameObject;
        senserF = senser.transform.position.y;
        senserFmoto = senser.transform.position.y;
    }
	
	void Update () {
        if (GameObject.Find("operetion") != null && netSystemOpe ==null) {
            netSystemOpe = GameObject.Find("operetion").gameObject;
        }
        senserF = senser.transform.position.y;
        GameObject.Find("sencer").gameObject.GetComponent<Text>().text = ((senserFmoto-senserF).ToString("N2")+"cm");
    }

    public void OnClickingButton() {
        if (transform.name == "remoteDelete") {
            netSystemOpe.GetComponent<networkSystemOperation>().flgToTrue(transform.name);
            
        } else if (transform.name == "remoteSetToStart") {
            netSystemOpe.GetComponent<networkSystemOperation>().flgToTrue(transform.name);
        } else if (transform.name == "remotePoseMode") {
            netSystemOpe.GetComponent<networkSystemOperation>().flgToTrue(transform.name);
        } else if (transform.name == "remoteActiveMode") {
            netSystemOpe.GetComponent<networkSystemOperation>().flgToTrue(transform.name);
        } else if (transform.name == "remoteDemoInput") {
            netSystemOpe.GetComponent<networkSystemOperation>().flgToTrue(transform.name);
        } else if (transform.name == "senserUP") {
            senser.transform.position += new Vector3(0, 0.01f, 0);
        } else if (transform.name == "senserDOWN") {
            senser.transform.position += new Vector3(0, -0.01f, 0);
        }
    }
}
