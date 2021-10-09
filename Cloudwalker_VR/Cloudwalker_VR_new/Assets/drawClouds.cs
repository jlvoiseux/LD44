using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawClouds : MonoBehaviour
{
    public int stackSize = 20;    
    public Mesh planeMesh;
    public Material cloudMat;
    float offset;

    public int layer;
    public Camera camera;
    private Matrix4x4 matrix1;
    private Matrix4x4 matrix2;

    public float cloudHeight = 2f;
    public float noise2Scale = 10f;
    public float noise1Scale = 50f;
    public float taperPower = 2.2f;
    public float cloudCutoff = 0.12f;
    public float cloudSaturate = 0.009f;
    public float timeScale = 0.4f;
    public float fresnelPower = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cloudMat.SetFloat("_midYValue", transform.position.y);
        cloudMat.SetFloat("_cloudHeight", cloudHeight);
        cloudMat.SetFloat("_noise2Scale", noise2Scale);
        cloudMat.SetFloat("_noise1Scale", noise1Scale);
        cloudMat.SetFloat("_taperPower", taperPower);
        cloudMat.SetFloat("_cloudCutoff", cloudCutoff);
        cloudMat.SetFloat("_cloudSaturate", cloudSaturate);
        cloudMat.SetFloat("_timeScale", timeScale);
        cloudMat.SetFloat("_fresnelPower", fresnelPower);

        offset = cloudHeight / stackSize / 2f;
        Vector3 startPosition = transform.position + (Vector3.up * (offset * stackSize / 2f));
        for(int i = 0; i < stackSize; i++) {
            matrix1 = Matrix4x4.TRS(startPosition - (Vector3.up * offset * i), transform.rotation, transform.localScale);
            Graphics.DrawMesh(planeMesh, matrix1, cloudMat, layer, camera, 0, null, true, false, false);
        }
        
    }
}
