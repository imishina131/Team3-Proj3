using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

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

    public GameObject boneMessage;

    public GameObject endPopUp;
    public Image starPopUp;
    public Sprite zeroStar;
    public Sprite oneStar;
    public Sprite twoStar;
    public Sprite threeStar;


    public Slider timerSlider;
    public float time = 0f;
    public float maxTime = 4f;
    public Slider timerSliderDeath;
    private bool startTimer;

    private bool canWalk = true;

    private AudioSource audio;
    public AudioClip collectSound;
    public AudioClip bombSound;
    public AudioClip movingBlockSound;
    public AudioClip saltSound;
    public AudioClip spikeSound;
    public AudioClip starSound;

    public AudioSource walkAudio;
    private bool walkPlaying;

    public GameObject explosion;

    public Transform teleport;

    public GameObject camera;
    private Animator camAnim;
    
    private bool openGate;
    public Animator gateAnim;

    private int requiredSlime;
    private int numberOfSlimes;
    public TMP_Text slimeCounter;
    public TMP_Text requiredSlimeCounter;
    private GameObject[] slimes;

    private static bool spikeHazardShown;
    private static bool bombHazardShown;
    private static bool saltHazardShown;
    public GameObject spikeMessage;
    public GameObject bombMessage;
    public GameObject saltMessage;

    public GameObject deathPopUp;

    public Animator boneAnim;

    private static bool afterLevel;
    public GameObject bone;

    private bool level5BoneFirstDone;
    private bool level6BoneLastOne;

    public Animator fade;

    private bool firstBoneCollected;
    public Animator gate01Anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(afterLevel)
        {
            bone.SetActive(false);
            afterLevel = false;
        }
        bones = GameObject.FindGameObjectsWithTag("Bone");
        slimes = GameObject.FindGameObjectsWithTag("Slime");
        healthBar.maxValue = health;

        rb = GetComponent<Rigidbody>();

        startTimer = false;
        timerSlider.maxValue = maxTime;
        timerSliderDeath.maxValue = maxTime;

        audio = GetComponent<AudioSource>();
        camAnim = camera.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Level05" || SceneManager.GetActiveScene().name == "Level06")
        {
            if(numberOfBones >= 1)
            {
                firstBoneCollected = true;
                gate01Anim.SetTrigger("Open");
            }
        }
        if(canWalk)
        {
            ChoosingDirection();
        }

        healthBar.value = health;

        if(health <= 0)
        {
            deathPopUp.SetActive(true);
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
        }

        if(SceneManager.GetActiveScene().name != "Tutorial")
        {
            boneCounter.text = "Bones Collected: " + numberOfBones + "/" + bones.Length;
        }

        if(SceneManager.GetActiveScene().name == "Level03" || SceneManager.GetActiveScene().name == "Level04" || SceneManager.GetActiveScene().name == "Level05" || SceneManager.GetActiveScene().name == "Level06")
        {
            requiredSlimeCounter.text = "Slime Required: " + requiredSlime + "/1";
            slimeCounter.text = "Slimes Collected: " + numberOfSlimes + "/" + slimes.Length;
        }

        if(movingUp || movingDown || movingLeft || movingRight)
        {
            CheckForHit();
        }

        if(GameObject.FindGameObjectWithTag("Bone") == null)
        {
            if(SceneManager.GetActiveScene().name == "Level03" || SceneManager.GetActiveScene().name == "Level04" || SceneManager.GetActiveScene().name == "Level05" || SceneManager.GetActiveScene().name == "Level06")
            {
                if(requiredSlime >= 1)
                {
                    openGate = true;
                    gateAnim.SetTrigger("Open");
                }
            }
            else if(SceneManager.GetActiveScene().name != "Level03" || SceneManager.GetActiveScene().name != "Level04" || SceneManager.GetActiveScene().name != "Level05" || SceneManager.GetActiveScene().name != "Level06")
            {
                openGate = true;
                gateAnim.SetTrigger("Open");
            }

            if(SceneManager.GetActiveScene().name == "Level06")
            {
                if(!level6BoneLastOne)
                {
                    boneMessage.SetActive(true);
                    level6BoneLastOne = true;
                    Invoke("HideMessage", 5.0f);
                }
            }

        }

        if(startTimer)
        {
            float timeActual = time += Time.deltaTime;
            timerSlider.value = timeActual;
            timerSliderDeath.value = timeActual;
            if(time >= 4)
            {
                startTimer = false;
            }

        }


        Debug.Log("UP:" + movingUp + "DOWN:" + movingDown + "RIGHT:" + movingRight + "LEFT:" + movingLeft);
        Debug.Log("Bone:" + GameObject.FindGameObjectWithTag("Bone"));

    }

    void OnCollisionEnter(Collision other)
    {
        

        if(other.gameObject.tag == "Bone")
        {
            numberOfBones += 1;
            Destroy(other.gameObject);
            audio.clip = collectSound;
            audio.Play();
            if(SceneManager.GetActiveScene().name == "Level01" || SceneManager.GetActiveScene().name == "Level02" || SceneManager.GetActiveScene().name == "Level03")
            {
                boneMessage.SetActive(true);
                Invoke("HideMessage", 5.0f);
            }
            if(SceneManager.GetActiveScene().name == "Level05")
            {
                if(!level5BoneFirstDone)
                {
                    boneMessage.SetActive(true);
                    level5BoneFirstDone = true;
                    Invoke("HideMessage", 5.0f);
                }
            }
        }

        if(other.gameObject.tag == "Slime")
        {
            audio.clip = collectSound;
            audio.Play();
            if(requiredSlime == 0)
            {
                requiredSlime += 1;
            }
            numberOfSlimes += 1;
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Bomb")
        {
            if(!bombHazardShown)
            {
                bombMessage.SetActive(true);
                bombHazardShown = true;
                Invoke("HideBombMessage", 4.0f);
            }
            explosion.SetActive(true);
            Invoke("HideExplosion", 3.0f);
            audio.clip = bombSound;
            audio.Play();
            health = health - 50;
            Destroy(other.gameObject);
        }

    }

    void HideExplosion()
    {
        explosion.SetActive(false);
    }

    void HideMessage()
    {
        boneMessage.SetActive(false);
    }

    void HideBombMessage()
    {
        bombMessage.SetActive(false);
    }

    void HideSpikeMessage()
    {
        spikeMessage.SetActive(false);
    }

    void HideSaltMessage()
    {
        saltMessage.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Teleport")
        {
            if(firstBoneCollected)
            {
                movingDown = false;
                movingLeft = false;
                movingRight = false;
                movingUp = false;
                animator.SetBool("Walking", false);
                walkAudio.Stop();
                walkPlaying = false;
                transform.position = teleport.position;
                camAnim.SetTrigger("Change");
            }
        }

        if(other.gameObject.tag == "Finish")
        {       
            if(openGate == true)
            {
                movingDown = false;
                movingLeft = false;
                movingRight = false;
                movingUp = false;
                walkAudio.Stop();
                walkPlaying = false;
                animator.SetBool("Walking", false);
                if(SceneManager.GetActiveScene().name != "Tutorial")
                {
                    endPopUp.SetActive(true);
                    audio.clip = starSound;
                    audio.Play();
                    if(GameObject.FindGameObjectWithTag("Bone") == null && health == 100 && GameObject.FindGameObjectWithTag("Slime") == null)
                    {
                        starPopUp.sprite = threeStar;
                    }
                    else if(GameObject.FindGameObjectWithTag("Bone") == null && GameObject.FindGameObjectWithTag("Slime") == null)
                    {
                        starPopUp.sprite = twoStar;
                    }
                    else if(GameObject.FindGameObjectWithTag("Bone") == null)
                    {
                        starPopUp.sprite = oneStar;
                    }
                }

                if(SceneManager.GetActiveScene().name == "Tutorial" && GameObject.FindGameObjectWithTag("Bone") == null)
                {
                    SceneChange.tutorialComplete = true;
                    boneAnim.SetTrigger("Shrink");
                    StartCoroutine(LoadLevel("LevelChoiceMenu"));
                }     
                else if(SceneManager.GetActiveScene().name == "Level01" && GameObject.FindGameObjectWithTag("Bone") == null)
                {
                    SceneChange.level01Complete = true;
                    if(starPopUp.sprite == threeStar)
                    {
                        SceneChange.level01Stars = 3;
                    }
                    else if(starPopUp.sprite == twoStar)
                    {
                        SceneChange.level01Stars = 2;
                    }
                    else if(starPopUp.sprite == oneStar)
                    {
                        SceneChange.level01Stars = 1;
                    }
                    StartCoroutine(LoadLevel("Level02"));
                }
                else if(SceneManager.GetActiveScene().name == "Level02" && GameObject.FindGameObjectWithTag("Bone") == null)
                {
                    SceneChange.level02Complete = true;
                    if(starPopUp.sprite == threeStar)
                    {
                        SceneChange.level02Stars = 3;
                    }
                    else if(starPopUp.sprite == twoStar)
                    {
                        SceneChange.level02Stars = 2;
                    }
                    else if(starPopUp.sprite == oneStar)
                    {
                        SceneChange.level02Stars = 1;
                    }
                    StartCoroutine(LoadLevel("Level03"));
                }
                else if(SceneManager.GetActiveScene().name == "Level03" && GameObject.FindGameObjectWithTag("Bone") == null)
                {
                    SceneChange.level03Complete = true;
                    if(starPopUp.sprite == threeStar)
                    {
                        SceneChange.level03Stars = 3;
                    }
                    else if(starPopUp.sprite == twoStar)
                    {
                        SceneChange.level03Stars = 2;
                    }
                    else if(starPopUp.sprite == oneStar)
                    {
                        SceneChange.level03Stars = 1;
                    }
                    StartCoroutine(LoadLevel("Level04"));
                }
                else if(SceneManager.GetActiveScene().name == "Level04" && GameObject.FindGameObjectWithTag("Bone") == null)
                {
                    SceneChange.level04Complete = true;
                    if(starPopUp.sprite == threeStar)
                    {
                        SceneChange.level04Stars = 3;
                    }
                    else if(starPopUp.sprite == twoStar)
                    {
                        SceneChange.level04Stars = 2;
                    }
                    else if(starPopUp.sprite == oneStar)
                    {
                        SceneChange.level04Stars = 1;
                    }
                    StartCoroutine(LoadLevel("Level05"));
                }
                else if(SceneManager.GetActiveScene().name == "Level05" && GameObject.FindGameObjectWithTag("Bone") == null)
                {
                    SceneChange.level05Complete = true;
                    if(starPopUp.sprite == threeStar)
                    {
                        SceneChange.level05Stars = 3;
                    }
                    else if(starPopUp.sprite == twoStar)
                    {
                        SceneChange.level05Stars = 2;
                    }
                    else if(starPopUp.sprite == oneStar)
                    {
                        SceneChange.level05Stars = 1;
                    }
                    StartCoroutine(LoadLevel("Level06"));
                }
                else if(SceneManager.GetActiveScene().name == "Level06" && GameObject.FindGameObjectWithTag("Bone") == null)
                {
                    SceneChange.level06Complete = true;
                    if(starPopUp.sprite == threeStar)
                    {
                        SceneChange.level06Stars = 3;
                    }
                    else if(starPopUp.sprite == twoStar)
                    {
                        SceneChange.level06Stars = 2;
                    }
                    else if(starPopUp.sprite == oneStar)
                    {
                        SceneChange.level06Stars = 1;
                    }
                    StartCoroutine(LoadLevel("LevelChoiceMenu"));
                }
            }

        }



        if(other.gameObject.tag == "Spike")
        {
            if(!spikeHazardShown)
            {
                spikeMessage.SetActive(true);
                spikeHazardShown = true;
                Invoke("HideSpikeMessage", 4.0f);
            }
            audio.clip = spikeSound;
            audio.Play();
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

        if(other.gameObject.tag == "Salt")
        {
            if(!saltHazardShown)
            {
                saltMessage.SetActive(true);
                saltHazardShown = true;
                Invoke("HideSaltMessage", 4.0f);
            }
            audio.clip = saltSound;
            audio.Play();
            health = health - 75;
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

    IEnumerator LoadLevel(string name)
    {
        if(SceneManager.GetActiveScene().name != "Tutorial")
        {
            afterLevel = true;
        }
        startTimer = true;
        yield return new WaitForSeconds(1);
        fade.SetTrigger("Leave");
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(name);
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Move")
        {
            Transform child = other.transform.Find("RaisingStoneWall");
            GameObject childObject = child.gameObject;
            blockToMove = childObject.GetComponent<Animator>();
            blockToMove.SetTrigger("Show");
            audio.clip = movingBlockSound;
            audio.Play();
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
            if(!walkPlaying)
            {
                walkAudio.Play();
            }
            walkPlaying = true;
            transform.rotation = Quaternion.Euler(0,180,0);
            animator.SetBool("Walking", true);
            rb.linearVelocity = Vector3.back * playerSpeed;
            //transform.Translate(Vector3.back * playerSpeed * Time.deltaTime);
        }
        else if(movingDown)
        {
            if(!walkPlaying)
            {
                walkAudio.Play();
            }
            walkPlaying = true;
            transform.rotation = Quaternion.Euler(0,0,0);
            animator.SetBool("Walking", true);
            rb.linearVelocity = Vector3.forward * playerSpeed;
            //transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
        }
        else if(movingLeft)
        {
            if(!walkPlaying)
            {
                walkAudio.Play();
            }
            walkPlaying = true;
            transform.rotation = Quaternion.Euler(0,90,0);
            animator.SetBool("Walking", true);
            rb.linearVelocity = Vector3.right * playerSpeed;
            //transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        }
        else if(movingRight)
        {
            if(!walkPlaying)
            {
                walkAudio.Play();
            }
            walkPlaying = true;
            transform.rotation = Quaternion.Euler(0,-90,0);
            animator.SetBool("Walking", true);
            rb.linearVelocity = Vector3.left * playerSpeed;
            //transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }
        else
        {
            walkAudio.Stop();
            walkPlaying = false;
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
