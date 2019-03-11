using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poseMode : MonoBehaviour {
    /* ポーズ用int */
    public bool pose;
    // true ポーズ中
    // false ポーズ解除

    /* ポーズ用のコライダー */
    public GameObject a;
    public GameObject b;
    public GameObject c;
    public GameObject d;
    public GameObject e;
    public GameObject BSarea;
    public GameObject textClear;
    public GameObject changeModeUp;
    public GameObject changeModeDown;

	/* ポーズ用表示オブジェクト */
	private GameObject grayWall;
	private GameObject halo;

    /* DemoHandFeelOnly取得 */
    private DemoHandFeelOnly demoHandFeelOnly;
    public bool feld;

    void Start() {
        pose = true;
        grayWall = GameObject.Find ("grayWall").gameObject;
        halo = GameObject.Find("halo").gameObject;
        demoHandFeelOnly = GetComponent<DemoHandFeelOnly>();
        feld = false;
    }

    void Update() {
        boolSetEveryTime(!pose);
        FeelOnMeshEnter();
        FeelOnMeshExit();
    }

    private void boolSetEveryTime(bool set) {
        a.GetComponent<DemoHandFeelOnly>().MeshColChange(set);
        b.GetComponent<DemoHandFeelOnly>().MeshColChange(set);
        c.GetComponent<DemoHandFeelOnly>().MeshColChange(set);
        d.GetComponent<DemoHandFeelOnly>().MeshColChange(set);
        e.GetComponent<DemoHandFeelOnly>().MeshColChange(set);
        BSarea.GetComponent<DemoHandFeelOnly>().MeshColChange(set);
        textClear.GetComponent<DemoHandFeelOnly>().MeshColChange(set);
        changeModeDown.GetComponent<DemoHandFeelOnly>().MeshColChange(set);
        changeModeUp.GetComponent<DemoHandFeelOnly>().MeshColChange(set);
        grayWall.SetActive(!set);
        halo.SetActive(!set);
    }

    private void FeelOnMeshEnter() {
        if (!feld) {
            if (demoHandFeelOnly.count1 >= 1 || demoHandFeelOnly.count2 >= 1) {
                /* ポーズ処理 */
                if (pose) {
                    pose = false;
                    feld = true;
                } else if (!pose) {
                    pose = true;
                    feld = true;
                }
            }
        }
    }

    private void FeelOnMeshExit() {
        if (feld) {
            if (demoHandFeelOnly.count1 == 0 && demoHandFeelOnly.count2 == 0) {
                feld = false;
            }
        }
    }
    
    public void modeChange(bool mode) {
        pose = mode;
    }
}
