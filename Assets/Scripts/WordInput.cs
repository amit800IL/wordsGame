using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WordInput : MonoBehaviour
{
    public InputField inputField;
    public string answer;
    private List<string> words;

    private void Start()
    {
        words = new List<string>();
    }

    public void OnButtonClick()
    {
        answer = inputField.text;
        string[] wordsBeforeProcess = answer.Split();
        ProcessWords(wordsBeforeProcess);
        string path = @".\words.txt";

        using (StreamWriter sw = File.CreateText(path))
        {
            sw.Write(JsonUtility.ToJson(words.ToArray()));
        }
    }

    private void ProcessWords(string[] allWords)
    {
        words = new List<string>();
        foreach (var word in allWords)
        {
            if(!string.IsNullOrWhiteSpace(word))
            {
                words.Add(word);
            }
        }
    }
}
