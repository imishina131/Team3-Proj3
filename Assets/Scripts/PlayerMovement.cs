using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float playerSpeed = 2.0f;
    private Vector3 playerVelocity;
    private bool movingUp;
    private bool movingDown;
    private bool movingRight;
    private bool movingLeft;
    private GameObject[] bones;


    public TMP_Text boneCounter;
    private int numberOfBones;

    public Slider healthBar;
    private int health = 100;

    public Animator blockToMove;

    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bones = GameObject.FindGameObjectsWithTag("Bone");
        healthBar.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        ChoosingDirection();

        healthBar.value = health;

        if(SceneManager.GetActiveScene().name != "Tutorial")
        {
            boneCounter.text = "Bones Collected: " + numberOfBones + "/" + bones.Length;
        }

    }

    void OnCollisionEnter(Collision other)
    {
        /*if(other.gameObject.tag == "Wall")
        {
            animator.SetBool("Walking", false);
            Debug.Log("WALL");
            
            if(movingUp)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.43f);
            }
            else if(movingDown)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.43f);
            }
            else if(movingLeft)
            {
                transform.position = new Vector3(transform.position.x - 0.43f, transform.position.y, transform.position.z);
            }
            else if(movingRight)
            {
                transform.position = new Vector3(transform.position.x + 0.43f, transform.position.y, transform.position.z);
            }
            
            movingUp = false;
            movingDown = false;
            movingLeft = false;
            movingRight = false;
        }
        */

        if(other.gameObject.tag == "Bone")
        {
            numberOfBones += 1;
            Destroy(other.gameObject);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Finish")
        {
            if(SceneManager.GetActiveScene().name == "Tutorial")
            {
                SceneChange.tutorialComplete = true;
                SceneManager.LoadScene("Level01");
            }     
            else if(SceneManager.GetActiveScene().name == "Level01")
            {
                SceneChange.level01Complete = true;
                SceneManager.LoadScene("Level02");
            }
            else if(SceneManager.GetActiveScene().name == "Level02")
            {
                SceneChange.level02Complete = true;
                SceneManager.LoadScene("Level03");
            }
        }

        if(other.gameObject.tag == "Move")
        {
            blockToMove.SetTrigger("Show");
        }
    }

    void ChoosingDirection()
    {
        if(Input.GetKeyDown(KeyCode.W) && !movingUp && !movingDown && !movingLeft && !movingRight && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0,180,0);
            movingUp = true;
        }
        else if(Input.GetKeyDown(KeyCode.A) && !movingUp && !movingDown && !movingLeft && !movingRight && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0,90,0);
            movingLeft = true;
        }
        else if(Input.GetKeyDown(KeyCode.D) && !movingUp && !movingDown && !movingLeft && !movingRight && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0,-90,0);
            movingRight = true;
        }
        else if(Input.GetKeyDown(KeyCode.S) && !movingUp && !movingDown && !movingLeft && !movingRight && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            movingDown = true;
        }

        Moving();
    }

    void Moving()
    {
        if(movingUp)
        {
            animator.SetBool("Walking", true);
            transform.Translate(Vector3.back * playerSpeed * Time.deltaTime);
        }
        else if(movingDown)
        {
            animator.SetBool("Walking", true);
            transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
        }
        else if(movingLeft)
        {
            animator.SetBool("Walking", true);
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        }
        else if(movingRight)
        {
            animator.SetBool("Walking", true);
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }
    }
}
