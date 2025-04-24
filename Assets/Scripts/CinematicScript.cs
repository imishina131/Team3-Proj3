using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CinematicScript : MonoBehaviour
{
    public Animator fade;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "IntroCutScene")
        {
            StartCoroutine(PlayCinematic01());
        }
        else if(SceneManager.GetActiveScene().name == "FinalCutScene")
        {
            StartCoroutine(PlayCinematic02());
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
        yield return new WaitForSeconds(11);
        fade.SetTrigger("Leave");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("LevelChoiceMenu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
