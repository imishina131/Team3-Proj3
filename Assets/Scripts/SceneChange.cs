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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        SceneManager.LoadScene("Level01");
    }

    public void GoToLevel02()
    {
        SceneManager.LoadScene("Level02");
    }

    public void GoToLevel03()
    {
        SceneManager.LoadScene("Level03");
    }

    public void GoToLevel04()
    {
        SceneManager.LoadScene("Level04");
    }

    public void GoToLevel05()
    {
        SceneManager.LoadScene("Level05");
    }

    public void GoToLevel06()
    {
        SceneManager.LoadScene("Level06");
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
