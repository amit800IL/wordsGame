using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour //Responsible for word bank management and deciding which words to spawn
{
    [Header("Attributes")]
    private int wordAmount;

    private string[] wordBank;
    private Word[] wordsOnBoard;

    private WordSpawner wordSpawner;

    private void Awake()
    {
        wordSpawner = GetComponent<WordSpawner>();
    }

    private void GetWordBank(string[] words)
    {
        wordBank = words;
    }

    private void SetWordsAtBoard()
    {

    }
}
