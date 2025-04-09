using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicLevel1and2 : MonoBehaviour
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
        if(SceneManager.GetActiveScene().name != "Level01" && SceneManager.GetActiveScene().name != "Level02")
        {
            music.volume = 0;
        }
        else
        {
            music.volume = 0.12f;
        }
    }

    void Awake()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Music01");
        if(obj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
