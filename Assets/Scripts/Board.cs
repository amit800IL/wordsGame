using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour //Responsible for word bank management and deciding which words to spawn
{
    [Header("Attributes")]
    private int letterCap;
    [SerializeField] private int maxLettersAmountOnBoard;

    //[SerializeField]private string[] wordBank;
    [SerializeField] private List<string> wordBank;
    private List<string> wordsOnBoard = new List<string>();

    [SerializeField]private WordSpawner wordSpawner;

    private void Start()
    {
        SetWordBank(wordBank);
        SetWordsAtBoard();
        initializeBoards();
    }
    private void SetWordBank(ICollection<string> words)
    {
        wordBank = words.ToList();
        Comparison<string> lengthSorter = new System.Comparison<string>((A, B) => B.Length.CompareTo(A.Length));
        wordBank.Sort(lengthSorter);
    }

    private void SetWordsAtBoard()
    {
        int allWordsLetterCount = TotalLength(wordBank);//checkLetterCount();
        //int wordsOnBoardLetterCount = TotalLength(wordsOnBoard);
        //int randomIndex;
        //List<int> usedIndexes = new List<int>();

        if (allWordsLetterCount > maxLettersAmountOnBoard)
            letterCap = maxLettersAmountOnBoard;
        else
            letterCap = allWordsLetterCount;

        while (TotalLength(wordsOnBoard) < letterCap)
        {
            int remainingLettersCount = letterCap - TotalLength(wordsOnBoard);

            if (wordBank[0].Length > remainingLettersCount) break;

            int maxIndiciesRange = 0;
            for (int i = wordBank.Count - 1; i >= 0; i--)
            {
                if (wordBank[i].Length <= remainingLettersCount)
                {
                    maxIndiciesRange = i + 1;
                    break;
                }
            }

            int randomValidIndex = UnityEngine.Random.Range(0, maxIndiciesRange);

            string randomValidWord = wordBank[randomValidIndex];
            wordsOnBoard.Add(randomValidWord);
            wordBank.Remove(randomValidWord);

            if (wordBank.Count == 0) break;
        }

        /*
        while (wordsOnBoardLetterCount < letterCap)
        {
            randomIndex = Random.Range(0, (wordBank.Length - 1));
            if (usedIndexes.Contains(randomIndex) || (wordsOnBoardLetterCount + wordBank[randomIndex].Length) > letterCap)
                continue;

            usedIndexes.Add(randomIndex);
            wordsOnBoard.Add(wordBank[randomIndex]);
            wordsOnBoardLetterCount += wordBank[randomIndex].Length;

            if(usedIndexes.Count== wordBank.Length)
            {
                Debug.Log("All words are on board");
                break;
            }
        }
        */
    }
    /*
    private int checkLetterCount()
    {
        int totalLenght = 0;
        foreach(var word in wordBank)
        {
            totalLenght += word.Length;
        }
        return totalLenght;
    }*/

    private int TotalLength(ICollection<string> strings)
    {
        int totalLength = 0;
        foreach(var word in strings)
        {
            totalLength += word.Length;
        }
        return totalLength;
    }

    private void initializeBoards()
    {
        Debug.Log("Board initialized");
        for (int i = 0; i < wordsOnBoard.Count; i++)
            wordSpawner.SpawnWord(wordsOnBoard[i]);
    }
}
