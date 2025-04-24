using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CinematicScript : MonoBehaviour
{
    public Animator fade;
    public VideoPlayer videoplayer;
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
    }

    IEnumerator PlayCinematic01()
    {
        yield return new WaitForSeconds(35);
        fade.SetTrigger("Leave");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Tutorial");
    }

    IEnumerator PlayCinematic02()
    {
        yield return new WaitForSeconds(12);
        fade.SetTrigger("Leave");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("LevelChoiceMenu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
