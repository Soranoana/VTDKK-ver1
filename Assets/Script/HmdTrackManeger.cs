using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HmdTrackManeger : MonoBehaviour {

    [SerializeField] Camera camera;
    [SerializeField] bool HMDTrackSwitchDisable;

	void Start () {
        XRDevice.DisableAutoXRCameraTracking(camera, HMDTrackSwitchDisable);
        camera.gameObject.transform.localPosition = Vector3.zero;
        camera.gameObject.transform.localEulerAngles = Vector3.zero;
    }
	
	void Update () {
        XRDevice.DisableAutoXRCameraTracking(camera, HMDTrackSwitchDisable);
    }
}
