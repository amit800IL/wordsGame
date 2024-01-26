using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private void Start()
    {
        resume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }

    }

    public void pause()
    {

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void menu(int MainMenu)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenu);
    }
}


