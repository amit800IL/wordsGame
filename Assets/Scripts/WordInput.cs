using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[System.Serializable]
public class Words
{
    public string[] words;
    public Words(string[] words)
    {
        this.words = words;
    }
}

public class WordInput : MonoBehaviour
{
    public InputField inputField;
    public Text submitButtonText;
    public Text promptText;
    string path = @".\words.txt";
    private List<string> words;
    private int playerIndex = 0;
    public string[] prompts;
    public string[] selectedPrompts;

    private void Awake()
    {
        File.Delete(path);
    }

    private void Start()
    {
        words = new List<string>();
        GetAllPrompts();
        SetRandomPrompts();
        promptText.text = selectedPrompts[0];
    }

    private void SetRandomPrompts()
    {
        int numberOfIndices = 4; // Number of indices to generate

        // Create a HashSet to store generated indices
        HashSet<int> generatedIndices = new HashSet<int>();

        selectedPrompts = new string[numberOfIndices];

        // Generate unique indices
        while (generatedIndices.Count < numberOfIndices)
        {
            int index = Random.Range(0, prompts.Length);

            // Add the index to the HashSet if it's not already present
            if (!generatedIndices.Contains(index))
            {
                selectedPrompts[generatedIndices.Count] = prompts[index];
                generatedIndices.Add(index);
                
            }
        }
    }

    public void OnButtonClick()
    {
        if(playerIndex >= 4)
        {
            return;
        }
        promptText.text = selectedPrompts[playerIndex];
        string answer = inputField.text;
        if (string.IsNullOrWhiteSpace(answer))
        {
            //Handle error 
            //return;
        }
        string[] wordsBeforeProcess = answer.Split();
        ProcessWords(wordsBeforeProcess);
        if(playerIndex == 2)
        {
            //submitButtonText.text = "Submit and play";
            //submitButtonText.transform.
        }
        if (playerIndex == 3)
        {
            WriteWordsToJsonFile(words);
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int numberOfScenes = SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene((currentSceneIndex + 1) % numberOfScenes);
        }
        playerIndex++;
        inputField.text = "";
    }

    public void WriteWordsToJsonFile(List<string> wordsToWrite)
    {
        Words wordsObject = new Words(wordsToWrite.ToArray());
        using (StreamWriter sw = File.CreateText(path))
        {
            string wordsInJson = JsonUtility.ToJson(wordsObject);
            sw.Write(wordsInJson);
        }
    }

    private void ProcessWords(string[] allWords)
    {
        foreach (var word in allWords)
        {
            if(!string.IsNullOrWhiteSpace(word))
            {
                words.Add(word);
            }
        }
    }

    public string[] GetWordsFromJsonFile()
    {
        string wordsInJson;
        using (StreamReader sr = new StreamReader(path))
        {
            wordsInJson = sr.ReadToEnd();
        }
        Words words = JsonUtility.FromJson<Words>(wordsInJson);
        return words.words;
    }

    public string[] GetAllPrompts()
    {
        string promptsPath = @".\Prompts.txt";
        prompts = new string[0];
        if (!File.Exists(promptsPath))
        {
            return prompts;
        }
        prompts = File.ReadAllLines(promptsPath);
        return prompts;
    }
}
