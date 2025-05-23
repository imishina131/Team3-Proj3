using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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


    public Image stars01;
    public Image stars02;
    public Image stars03;
    public Image stars04;
    public Image stars05;
    public Image stars06;
    public Sprite zeroStars;
    public Sprite oneStar;
    public Sprite twoStars;
    public Sprite threeStars;

    public static int level01Stars;
    public static int level02Stars;
    public static int level03Stars;
    public static int level04Stars;
    public static int level05Stars;
    public static int level06Stars;


    public GameObject[] locks;

    public Animator boneAnim;
    public GameObject bone;

    public Animator fade;

    public static bool crownPlayed;

    public GameObject introButton;
    public GameObject outroButton;

    public GameObject crown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;

        if(SceneManager.GetActiveScene().name == "LevelChoiceMenu")
        {
            SetStars();
            Unlock();
            if(tutorialComplete)
            {
                introButton.SetActive(true);
            }

            if(level06Complete)
            {
                outroButton.SetActive(true);
            }
        }

        DisplayCrown();
    }

    void DisplayCrown()
    {
        if(crownPlayed)
        {
            if(SceneManager.GetActiveScene().name == "Level01" || SceneManager.GetActiveScene().name == "Level02" || SceneManager.GetActiveScene().name == "Level03" || SceneManager.GetActiveScene().name == "Level04" || SceneManager.GetActiveScene().name == "Level05" || SceneManager.GetActiveScene().name == "Level06" || SceneManager.GetActiveScene().name == "Tutorial")
            {
                crown.SetActive(true);
            }
        }
    }

    void Unlock()
    {
        if(tutorialComplete)
        {
            locks[0].SetActive(false);
        }
        if(level01Complete)
        {
            locks[1].SetActive(false);
        }
        if(level02Complete)
        {
            locks[2].SetActive(false);
        }
        if(level03Complete)
        {
            locks[3].SetActive(false);
        }
        if(level04Complete)
        {
            locks[4].SetActive(false);
        }
        if(level05Complete)
        {
            locks[5].SetActive(false);
        }
    }

    void SetStars()
    {
        switch(level01Stars)
        {
            case 3:
            stars01.sprite = threeStars;
            break;

            case 2:
            stars01.sprite = twoStars;
            break;

            case 1:
            stars01.sprite = oneStar;
            break;
        }

        switch(level02Stars)
        {
            case 3:
            stars02.sprite = threeStars;
            break;

            case 2:
            stars02.sprite = twoStars;
            break;

            case 1:
            stars02.sprite = oneStar;
            break;
        }

        switch(level03Stars)
        {
            case 3:
            stars03.sprite = threeStars;
            break;

            case 2:
            stars03.sprite = twoStars;
            break;

            case 1:
            stars03.sprite = oneStar;
            break;
        }

        switch(level04Stars)
        {
            case 3:
            stars04.sprite = threeStars;
            break;

            case 2:
            stars04.sprite = twoStars;
            break;

            case 1:
            stars04.sprite = oneStar;
            break;
        }

        switch(level05Stars)
        {
            case 3:
            stars05.sprite = threeStars;
            break;

            case 2:
            stars05.sprite = twoStars;
            break;

            case 1:
            stars05.sprite = oneStar;
            break;
        }

        switch(level06Stars)
        {
            case 3:
            stars06.sprite = threeStars;
            break;

            case 2:
            stars06.sprite = twoStars;
            break;

            case 1:
            stars06.sprite = oneStar;
            break;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "LevelChoiceMenu")
        {
            if(level01Stars == 3 && level02Stars == 3 && level03Stars == 3 && level04Stars == 3 && level05Stars == 3 && level06Stars == 3)
            {
                if(!crownPlayed)
                {
                    boneAnim.SetTrigger("Shrink");
                    Invoke("PlayEasterEgg", 1.5f);
                    crownPlayed = true;
                }
            }
        }
    }

    void PlayEasterEgg()
    {
        SceneManager.LoadScene("3Stars");
    }

    public void PlayIntro()
    {
        StartCoroutine(Load("IntroCutScene"));
    }

    public void PlayOutro()
    {
        StartCoroutine(Load("FinalCutScene"));
    }

    public void GoToMainMenu()
    {
        StartCoroutine(Load("MainMenu"));
    }

    public void GoToCreditsMenu()
    {
        StartCoroutine(Load("CreditsMenu"));
    }

    public void GoToHelpMenu()
    {
        StartCoroutine(Load("HelpMenu"));
    }

    public void StartGame()
    {
        if(!tutorialComplete)
        {
            StartCoroutine(Load("IntroCutScene"));
        }
        else
        {
            if(SceneManager.GetActiveScene().name != "CreditsMenu" && SceneManager.GetActiveScene().name != "HelpMenu" && SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "LevelChoiceMenu")
            {
                bone.SetActive(true);
            }
            Time.timeScale = 1f;
            StartCoroutine(Load("LevelChoiceMenu"));
        }
    }

    public void GoToLevel01()
    {
        if(tutorialComplete)
        {
            StartCoroutine(Load("Level01"));
        }
    }

    public void GoToLevel02()
    {
        if(level01Complete)
        {
            StartCoroutine(Load("Level02"));
        }
    }

    public void GoToLevel03()
    {
        if(level02Complete)
        {
            StartCoroutine(Load("Level03"));
        }
    }

    public void GoToLevel04()
    {
        if(level03Complete)
        {
            StartCoroutine(Load("Level04"));
        }
    }

    public void GoToLevel05()
    {
        if(level04Complete)
        {
            StartCoroutine(Load("Level05"));
        }
    }

    public void GoToLevel06()
    {
        if(level05Complete)
        {
            StartCoroutine(Load("Level06"));
        }
    }

    public void GoToTutorial()
    {
        if(SceneManager.GetActiveScene().name == "IntroCutScene")
        {
            fade.SetTrigger("Leave");
            Invoke("Bone", 1.5f);
        }
        else if(SceneManager.GetActiveScene().name == "FinalCutScene")
        {
            fade.SetTrigger("Leave");
            Invoke("BoneFinal", 1.5f);
        }
        else
        {
            StartCoroutine(Load("Tutorial"));
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Load(string name)
    {
        boneAnim.SetTrigger("Shrink");
        fade.SetTrigger("Leave");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(name);
    }

    void Bone()
    {
        boneAnim.SetTrigger("Shrink");
        Invoke("Skip", 1.5f);
    }

    void BoneFinal()
    {
        boneAnim.SetTrigger("Shrink");
        Invoke("SkipFinal", 1.5f);
    }

    void Skip()
    {
        if(!tutorialComplete)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            SceneManager.LoadScene("LevelChoiceMenu");
        }
    }

    void SkipFinal()
    {
        SceneManager.LoadScene("LevelChoiceMenu");
    }
}
