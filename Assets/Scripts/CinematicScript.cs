using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CinematicScript : MonoBehaviour
{
    public Animator fade;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(PlayCinematic());
    }

    IEnumerator PlayCinematic()
    {
        yield return new WaitForSeconds(34);
        fade.SetTrigger("Leave");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Tutorial");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
