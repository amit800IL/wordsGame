using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Board : MonoBehaviour //Responsible for word bank management and deciding which words to spawn
{
    [Header("Attributes")]
    private int letterAmount;
    [SerializeField] private int maxLettersAmount;

    private string[] wordBank;
    private List<string> wordsOnBoard = new List<string>();

    private WordSpawner wordSpawner;


    private void Awake()
    {
        wordSpawner = GetComponent<WordSpawner>();
    }

    private void SetWordBank(string[] words)
    {
        wordBank = words;
    }

    private void SetWordsAtBoard()
    {
        int allWordsLetterCount = checkLetterCount();
        int wordsOnBoardLetterCount = 0;
        int randomIndex;
        List<int> usedIndexes = new List<int>();

        if (allWordsLetterCount > maxLettersAmount)
            letterAmount = maxLettersAmount;
        else
            letterAmount = allWordsLetterCount;

        while (wordsOnBoardLetterCount < letterAmount)
        {
            randomIndex = Random.Range(0, (wordBank.Length - 1));
            if (usedIndexes.Contains(randomIndex) || (wordsOnBoardLetterCount + wordBank[randomIndex].Length) > letterAmount)
                continue;

            usedIndexes.Add(randomIndex);
            wordsOnBoard.Add(wordBank[randomIndex]);
            wordsOnBoardLetterCount += wordBank[randomIndex].Length;
        }
    }

    private int checkLetterCount()
    {
        string concatinatedWords = "";

        for (int i = 0; i < wordBank.Length; i++)
        {
            concatinatedWords += wordBank[i];
        }

        return concatinatedWords.Length;
    }

    private void initializeBoards()
    {
        for (int i = 0; i < wordsOnBoard.Count; i++)
            wordSpawner.SpawnWord(wordsOnBoard[i]);
    }
}
