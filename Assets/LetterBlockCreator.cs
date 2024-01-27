using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LetterBlockCreator : MonoBehaviour
{
    private const string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string abc = "abcdefghijklmnopqrstuvwxyz";

    [SerializeField] private Texture2D[] letterTextures;
    [SerializeField] private Shader letterShader;
    [SerializeField] private GameObject blockPrefab;

    private Material[] letterMaterials;

    private void Awake()
    {
        letterMaterials = new Material[letterTextures.Length];

        for (int i = 0; i < letterTextures.Length; i++)
        {
            letterMaterials[i] = new Material(letterShader);
            letterMaterials[i].SetTexture("_MainTex", letterTextures[i]);
        }
    }

    public GameObject CreateLetterBlock(char letter)
    {
        int index = ABC.IndexOf(letter);
        if(index==-1) index = abc.IndexOf(letter);        
        if(index==-1)
        {
            Debug.LogError($"Error! char {letter} is not a valid english letter!");
            return null;
        }

        var block = Instantiate(blockPrefab);
        block.transform.LookAt(block.transform.position - Vector3.forward);
        Debug.Log($"{letter}, {index}");
        block.GetComponent<MeshRenderer>().material = letterMaterials[index];

        return block;
    }
}
