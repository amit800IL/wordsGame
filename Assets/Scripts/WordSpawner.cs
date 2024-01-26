using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class WordSpawner : MonoBehaviour //Actually spawns the words
{
    private const string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    [Header("Word Settings")]
    [SerializeField] private Word wordPrefab;
    [SerializeField] private GameObject[] WordObjects;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnHeight;
    [SerializeField] private float spawnAreaSize;

    private Board board;
    private Dictionary<char, GameObject> CharDictionary;

    private void Awake()
    {
        board = GetComponent<Board>();
        CharDictionary = new Dictionary<char, GameObject>();
        for (int i = 0; i < 26; i++)
        {
            //CharDictionary.Add(ABC[i], WordObjects[i]);
        }
    }

    private Word CreateWord(string wordText)
    {
        Word wordObj = Instantiate(wordPrefab, Vector3.zero, Random.rotationUniform);
        wordObj.gameObject.name = $"WordObj : {wordText}";
        //wordObj.boxCollider.size = new Vector3(wordText.Length, 1, 1);
        //wordObj.rigidbody.mass = wordText.Length;

        for (int i = 0; i < wordText.Length; i++)
        {
            char letter = wordText[i];
            Vector3 spawnPosition = Vector3.right * (i - wordText.Length * 0.5f);
            //GameObject letterObject = Instantiate(CharDictionary[letter], spawnPosition, Quaternion.identity, wordObj.transform);
            //letterObject.name = $"{i}:{letter}";
        }

        return wordObj;
    }

    public void SpawnWord(string wordText)
    {
        var wordObj = CreateWord(wordText);
        Vector2 squarePosition = new Vector2(Random.value, Random.value) - 0.5f * Vector2.one;

        Vector3 spawnPosition = new Vector3(squarePosition.x * spawnAreaSize, spawnHeight, squarePosition.y * spawnAreaSize);

        wordObj.transform.position = spawnPosition;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            SpawnWord("");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(spawnAreaSize, 1, spawnAreaSize));
    }
}
