using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fingerTest : MonoBehaviour {

    //座標を移動させるためのComputeShader
    [SerializeField]
    private ComputeShader m_ComputeShader;
    [SerializeField]
    private GameObject sphere;
    private Transform m_Sphere;

    private ComputeBuffer m_Buffer;

	void Start () {
        //ComputeShaderの結果を受け取るためのバッファー（要素float１個分）を用意
        m_Buffer=new ComputeBuffer(1, sizeof(float));
        //そのバッファーをComputeShaderに設定しています
        //これでCPUとGPUが繋がりました！
        m_ComputeShader.SetBuffer(0, "Result", m_Buffer);

        m_Sphere=sphere.transform;
	}
	
	void Update () {
        //ComputeShaderにFloatをSetする関数を使って、positionXにm_Cubeのposition.Xを渡している
        //GPU側がCPU側のSphere座標について知ることができた
        m_ComputeShader.SetFloat("positionX", m_Sphere.position.x);
        //ComputeShaderのDispatchを使うことで処理を開始できます
        //Dispatchを呼んだことでGPU側でCubeを移動させる処理が完了
        m_ComputeShader.Dispatch(0, 8, 8, 1);
        //しかしCPU側にそのデータは存在していないため、m_Sphereに適用することができません

        var data = new float[1];
        //用意した配列にm_Bufferに入っているデータを入れます
        //これでGPU側のデータをCPU側で使えるようになりました！
        m_Buffer.GetData(data);
        Debug.Log(data);
        m_Sphere.position=new Vector3(data[0], 0,0);
    }

    //いらなくなったBufferを解放する
    private void OnDestroy() {
        //m_Buffer.Release();
    }
}
