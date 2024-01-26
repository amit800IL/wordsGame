using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Attributes")]
    private int wordAmount;
    private Word[] wordsAtBoard;
    private string[] wordbBank;

   private void GetWordBank(string[] words)
    {
        wordbBank = words;
    }
    private void SetWordsAtBoard()
    {

    }
}
