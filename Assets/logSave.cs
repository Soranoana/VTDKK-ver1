using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class logSave : MonoBehaviour {

    public GameObject Operation;
    private string logText;
    private StreamWriter sw;
    FileInfo fi;
    private string filePath;
    private string dateTimeStr;
    private bool writton = false;
    private float deltaTime;
    private string nowdate;

    void Start() {
        nowdate = Nowdate();
        /* プラットホーム依存コンパイル */
#if UNITY_EDITOR
        filePath = "C:/logSaveTK/log" + nowdate + ".txt";
#endif
#if UNITY_STANDALONE_WIN
        filePath = "C:/logSaveTK/log" + nowdate + ".txt";
#endif
#if UNITY_ANDROID
		filePath = Application.persistentDataPath + "/log" + nowdate + ".txt";
#endif
        /* プラットホーム依存コンパイル ここまで*/

        logText = "";
        sw = new StreamWriter(filePath, true);
        sw.WriteLine(nowdate);
        sw.Write("\tRealtime\t\tGameTime\t\t\t\tEvent");
        if (PlayerPrefs.GetInt("HostOrClient") == 5) {
            sw.WriteLine("\tTask.");
        } else if (PlayerPrefs.GetInt("HostOrClient") == 6) {
            sw.WriteLine("\tPrac.");
        } else {
            sw.WriteLine("\tnon.");
        }
        sw.Flush();
        sw.Close();
        deltaTime = NowTimeNum();
    }

    void Update() {
        if (writton) {
            LogSaving();
        }
    }

    public void LogSaving() {
        sw = new StreamWriter(filePath, true);
        sw.Write("\t" + NowTime() + "\t\t");
        sw.Write(Time.fixedTime.ToString() + "\t");
        sw.Write("DeltaTime\t" + (NowTimeNum() - deltaTime).ToString("N2") + "\t");
        deltaTime = NowTimeNum();
        sw.WriteLine(logText);
        sw.Flush();
        sw.Close();
        writton = false;
    }

    public string NowTime(){
		dateTimeStr = System.DateTime.Now.Hour.ToString ()   + "-"
					+ System.DateTime.Now.Minute.ToString () + "-"
					+ System.DateTime.Now.Second.ToString ();
		return dateTimeStr;
	}

	public string Nowdate(){
		dateTimeStr = System.DateTime.Now.Year.ToString ()   + "-"
					+ System.DateTime.Now.Month.ToString ()  + "-"
			        + System.DateTime.Now.Day.ToString ()    + " "
					+ System.DateTime.Now.Hour.ToString ()   + "-"
			        + System.DateTime.Now.Minute.ToString () + "-"
			        + System.DateTime.Now.Second.ToString ();
		return dateTimeStr;
	}

    public float NowTimeNum() {
        return System.DateTime.Now.Hour * 3600
                    + System.DateTime.Now.Minute * 60
                    + System.DateTime.Now.Second
                    + System.DateTime.Now.Millisecond * 0.001f;
    }

    public void logSetter(string log) {
        logText = log;
        writton = true;
    }
}
