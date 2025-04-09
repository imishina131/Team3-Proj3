using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckForMusic : MonoBehaviour
{
    AudioSource music;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        music = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Level02")
        {
            if(GameObject.FindGameObjectWithTag("Music01") == null)
            {
                music.volume = 0.12f;
            }
            else if(GameObject.FindGameObjectWithTag("Music01") != null)
            {
                music.volume = 0;
            }
        }
        
        if(SceneManager.GetActiveScene().name == "Level04")
        {
            if(GameObject.FindGameObjectWithTag("Music03") == null)
            {
                music.volume = 0.12f;
            }
            else if(GameObject.FindGameObjectWithTag("Music03") != null)
            {
                music.volume = 0;
            }
        }
    }
}
