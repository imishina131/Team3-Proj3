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

    public GameObject raycastObject;

    Rigidbody rb;

    public GameObject fence;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bones = GameObject.FindGameObjectsWithTag("Bone");
        healthBar.maxValue = health;

        rb = GetComponent<Rigidbody>();
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

        if(movingUp || movingDown || movingLeft || movingRight)
        {
            CheckForHit();
        }

        if(GameObject.FindGameObjectWithTag("Bone") == null)
        {
            fence.SetActive(false);
        }

        Debug.Log("UP:" + movingUp + "DOWN:" + movingDown + "RIGHT:" + movingRight + "LEFT:" + movingLeft);

    }

    void OnCollisionEnter(Collision other)
    {
        

        if(other.gameObject.tag == "Bone")
        {
            numberOfBones += 1;
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Bomb")
        {
            health = health - 50;
            Destroy(other.gameObject);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Finish")
        {
            if(SceneManager.GetActiveScene().name == "Tutorial" && GameObject.FindGameObjectWithTag("Bone") == null)
            {
                SceneChange.tutorialComplete = true;
                SceneManager.LoadScene("Level01");
            }     
            else if(SceneManager.GetActiveScene().name == "Level01" && GameObject.FindGameObjectWithTag("Bone") == null)
            {
                SceneChange.level01Complete = true;
                SceneManager.LoadScene("Level02");
            }
            else if(SceneManager.GetActiveScene().name == "Level02" && GameObject.FindGameObjectWithTag("Bone") == null)
            {
                SceneChange.level02Complete = true;
                SceneManager.LoadScene("Level03");
            }
        }

        if(other.gameObject.tag == "Move")
        {
            Transform child = other.transform.Find("RaisingStoneWall");
            GameObject childObject = child.gameObject;
            blockToMove = childObject.GetComponent<Animator>();
            blockToMove.SetTrigger("Show");
        }

        if(other.gameObject.tag == "Spike")
        {
            health = health - 20;
            if(movingUp)
            {
                movingUp = false;
                movingDown = true;
            }
            else if(movingDown)
            {
                movingDown = false;
                movingUp = true;
            }
            else if(movingLeft)
            {
                movingLeft = false;
                movingRight = true;
            }
            else if(movingRight)
            {
                movingRight = false;
                movingLeft = true;
            }
        }

    }

    void ChoosingDirection()
    {
        if(Input.GetKeyDown(KeyCode.W) && !movingUp && !movingDown && !movingLeft && !movingRight && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D))
        {
            movingUp = true;
        }
        else if(Input.GetKeyDown(KeyCode.A) && !movingUp && !movingDown && !movingLeft && !movingRight && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D))
        {
            movingLeft = true;
        }
        else if(Input.GetKeyDown(KeyCode.D) && !movingUp && !movingDown && !movingLeft && !movingRight && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.W))
        {
            movingRight = true;
        }
        else if(Input.GetKeyDown(KeyCode.S) && !movingUp && !movingDown && !movingLeft && !movingRight && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.D))
        {
            movingDown = true;
        }

        Moving();
    }

    void Moving()
    {
        if(movingUp)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
            animator.SetBool("Walking", true);
            rb.velocity = Vector3.back * playerSpeed;
            //transform.Translate(Vector3.back * playerSpeed * Time.deltaTime);
        }
        else if(movingDown)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            animator.SetBool("Walking", true);
            rb.velocity = Vector3.forward * playerSpeed;
            //transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
        }
        else if(movingLeft)
        {
            transform.rotation = Quaternion.Euler(0,90,0);
            animator.SetBool("Walking", true);
            rb.velocity = Vector3.right * playerSpeed;
            //transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        }
        else if(movingRight)
        {
            transform.rotation = Quaternion.Euler(0,-90,0);
            animator.SetBool("Walking", true);
            rb.velocity = Vector3.left * playerSpeed;
            //transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }
    }

    void CheckForHit()
    {
        RaycastHit objectHit;
        Vector3 fwd = raycastObject.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(raycastObject.transform.position, fwd * 1, Color.green);
        if(Physics.Raycast(raycastObject.transform.position, fwd, out objectHit, 1))
        {
            if(objectHit.transform.CompareTag("Wall"))
            {
                Debug.Log("WALL");
                movingUp = false;
                movingDown = false;
                movingLeft = false;
                movingRight = false;
                animator.SetBool("Walking", false);
            }
        }
    }
}
