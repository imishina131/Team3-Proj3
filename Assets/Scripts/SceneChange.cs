using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //variable determining whether the level was passed or not
    public static bool tutorialComplete;
    public static bool level01Complete;
    public static bool level02Complete;
    public static bool level03Complete;
    public static bool level04Complete;
    public static bool level05Complete;
    public static bool level06Complete;

    public GameObject pauseMenu;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToCreditsMenu()
    {
        SceneManager.LoadScene("CreditsMenu");
    }

    public void GoToHelpMenu()
    {
        SceneManager.LoadScene("HelpMenu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LevelChoiceMenu");
    }

    public void GoToLevel01()
    {
        if(tutorialComplete)
        {
            SceneManager.LoadScene("Level01");
        }
    }

    public void GoToLevel02()
    {
        if(level01Complete)
        {
            SceneManager.LoadScene("Level02");
        }
    }

    public void GoToLevel03()
    {
        if(level02Complete)
        {
            SceneManager.LoadScene("Level03");
        }
    }

    public void GoToLevel04()
    {
        if(level03Complete)
        {
            SceneManager.LoadScene("Level04");
        }
    }

    public void GoToLevel05()
    {
        if(level04Complete)
        {
            SceneManager.LoadScene("Level05");
        }
    }

    public void GoToLevel06()
    {
        if(level05Complete)
        {
            SceneManager.LoadScene("Level06");
        }
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
