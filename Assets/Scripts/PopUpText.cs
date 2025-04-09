using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PopUpText : MonoBehaviour
{
    public GameObject text;
    void Start()
    {
        text.SetActive(false);
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.name == "Player")
        {
            text.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        Destroy(text);
        Destroy(gameObject);
    }

}
