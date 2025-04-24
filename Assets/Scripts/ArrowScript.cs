using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public static bool teleported;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Invoke("Disappear", 3.0f);
    }

    void Disappear()
    {
        teleported = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if(teleported)
       {
           Invoke("Disappear", 5.0f);
       }
    }
}
