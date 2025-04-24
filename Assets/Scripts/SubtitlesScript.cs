using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
 
public class Subtitles : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public bool subDec = true;
    public GameObject buttonText;
    public GameObject background;
 
    void Start()
    {
        StartCoroutine(TheSequence());
    }
 
    IEnumerator TheSequence()
    {
        if(SceneManager.GetActiveScene().name == "IntroCutScene")
        {
            yield return new WaitForSeconds(4.6f);
            textBox.text = "Hey there! I'm Bonbon.";
            yield return new WaitForSeconds(2.3f);
            textBox.text = "And this is where all the skeleton magic happens";
            yield return new WaitForSeconds(4.6f);
            textBox.text = "Every year, we build a big, spooky Bone God to keep us safe.";
            yield return new WaitForSeconds(4.7f);
            textBox.text = "We call it The Great Bone Ritual. Fancy, right?";
            yield return new WaitForSeconds(4.3f);
            textBox.text = "But this year? ";
            yield return new WaitForSeconds(1.6f);
            textBox.text = "Boom — storm hits, bones go flying everywhere!";
            yield return new WaitForSeconds(4.3f);
            textBox.text = "Now guess who’s gotta run around and find every single piece? Yep...";
            yield return new WaitForSeconds(4.8f);
            textBox.text = "Me!";
            yield return new WaitForSeconds(2.2f);
            textBox.text = "No bones, no blessings...";
        }

        if(SceneManager.GetActiveScene().name == "FinalCutScene")
        {
            textBox.text = "And just like that… the ritual was saved";
            yield return new WaitForSeconds(8.3f);
            textBox.text = "Whoops. Maybe next year I’ll triple-check the sticky stuff";
        }
    }
 
    public void SwitchSubs()
    {
        if (!subDec)
        {
            textBox.gameObject.SetActive(true);
            background.SetActive(true);
            subDec = true;
            return;
        }
        else
        {
            textBox.gameObject.SetActive(false);
            background.SetActive(false);
            subDec = false;
            return;
        }
    }
}
