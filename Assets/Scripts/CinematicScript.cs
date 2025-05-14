using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CinematicScript : MonoBehaviour
{
    public Animator fade;
    public VideoPlayer videoplayer;
    public Animator bone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "IntroCutScene")
        {
            videoplayer.url = System.IO.Path.Combine (Application.streamingAssetsPath, "Intro cutscene final.mp4");
            StartCoroutine(PlayCinematic01());
        }
        else if(SceneManager.GetActiveScene().name == "FinalCutScene")
        {
            videoplayer.url = System.IO.Path.Combine (Application.streamingAssetsPath, "Final cutscene.mp4");
            StartCoroutine(PlayCinematic02());
        }
        else if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            videoplayer.url = System.IO.Path.Combine (Application.streamingAssetsPath, "main menu.mp4");
        }
        else if(SceneManager.GetActiveScene().name == "3Stars")
        {
            StartCoroutine(PlayCrown());
        }
    }

    IEnumerator PlayCinematic01()
    {
        yield return new WaitForSeconds(35);
        fade.SetTrigger("Leave");
        yield return new WaitForSeconds(1.5f);
        bone.SetTrigger("Shrink");
        yield return new WaitForSeconds(1.5f);
        if(SceneChange.tutorialComplete == false)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            SceneManager.LoadScene("LevelChoiceMenu");
        }
    }

    IEnumerator PlayCinematic02()
    {
        yield return new WaitForSeconds(12);
        fade.SetTrigger("Leave");
        yield return new WaitForSeconds(1.5f);
        bone.SetTrigger("Shrink");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("LevelChoiceMenu");
    }

    IEnumerator PlayCrown()
    {
        yield return new WaitForSeconds(8);
        fade.SetTrigger("Leave");
        yield return new WaitForSeconds(1.5f);
        bone.SetTrigger("Shrink");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Explore Village");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
