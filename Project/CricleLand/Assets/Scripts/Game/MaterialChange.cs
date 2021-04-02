using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<MeshRenderer>().material.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }
}
