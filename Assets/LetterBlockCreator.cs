using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBlockCreator : MonoBehaviour
{
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
        int index = WordSpawning.ABC.IndexOf(letter);
        var block = Instantiate(blockPrefab);
        block.transform.LookAt(block.transform.position - Vector3.forward);
        block.GetComponent<MeshRenderer>().material = letterMaterials[index];

        return block;
    }
}
