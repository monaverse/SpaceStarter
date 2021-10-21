using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxMaterialScript : MonoBehaviour
{
    public Material mat;
    private void Awake()
    {

    }
    void Start()
    {
        //Set skybox
        RenderSettings.skybox = mat;

        Debug.Log("Awake");
    }
}
