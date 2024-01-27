using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBox : MonoBehaviour
{
    [SerializeField] private Shader shader;
    [SerializeField] private Material material;

    [SerializeField] private Texture2D texture;

    [ContextMenu("Apply")]
    public void AssignMaterial()
    {
        material = new Material(shader);
        material.SetTexture("_MainTex", texture);   
        
        GetComponent<MeshRenderer>().material = material;
    }
}
