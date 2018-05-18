using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    //controls player movement, gets input, sets noise levels

    //allow it to be disabled at the end of the game
    [HideInInspector]
    public bool playerIsActive;

    //speeds
    public float walkSpeed;
    public float crouchSpeed;
    public float sprintSpeed;

    //speed that is applied
    float speed;

    //sprinting
    public float sprintLength = 10f;
    public float sprintRecharge = .1f;
    public float sprintMin = 2f;

    public Text sprintPercentText;

    private float sprintCharge = 9;

    //crouching
    private bool isCrouching;
    CharacterController controller;
    private float height;
    private CapsuleCollider col;

    public float crouchHeight;

    //calculating movement
    private Vector3 moveDirection = Vector3.zero;

    //throwing distractions
    public int numberOfDistractions;
    public float distractionReloadTime;
    public GameObject distraction;

    private float timeSinceLastDistraction;

    //misc
    public float gravity = 20.0F;
    public float playerReach;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        col = GetComponent<CapsuleCollider>();
        Cursor.lockState = CursorLockMode.Locked;
        speed = walkSpeed;
        isCrouching = false;
        playerIsActive = true;

        timeSinceLastDistraction = Time.time;

        sprintCharge = 9;
    }

    //subscribe and unsubscribe from events
    void OnEnable()
    {
        FindObjectOfType<Manager>().GameOver += OnGameOver;
    }

    void OnDisable()
    {
        FindObjectOfType<Manager>().GameOver -= OnGameOver;
    }

    void OnGameOver()
    {
        playerIsActive = false;
    }

    void Update()
    {
        //disable if canMove is false;
        if (!playerIsActive)
        {
            if (!controller.isGrounded)
            {
                moveDirection = Vector3.zero;
                moveDirection.y -= gravity * Time.deltaTime; //gravity is constant
                controller.Move(moveDirection * Time.deltaTime); //execute move command
            }

            //return to exit update function
            return;
        }

        if (Input.GetKeyDown("escape")) //press escape key to exit cursor lock mode
        {
            FindObjectOfType<Manager>().PauseGame();
        }
        //press l to relock mouse
        if (Input.GetKeyDown("l"))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        //Debug.Log("before: " + speed);

        speed = walkSpeed;
        //playerNoise = walkNoise;

        if (Input.GetKeyDown(KeyCode.C)) //crouching
        {
            //Debug.Log("pressed C");
            if (!isCrouching)
            {
                isCrouching = true;
            }

            else
            {
                isCrouching = false;
            }
        }

        if (isCrouching)
        {
            //Debug.Log("crouching");
            controller.height = crouchHeight;
            col.height = crouchHeight;
            speed = crouchSpeed;
        }
        else
        {
            controller.height = 2f;
            col.height = 2f;
            speed = walkSpeed;
        }


        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching && sprintCharge > sprintMin)
        {
            //Debug.Log("sprinting");
            speed = sprintSpeed;
            sprintCharge -= 1f * Time.deltaTime;
        }
        else
        {
            if (controller.isGrounded)
            {
                //recharge sprint
                if (sprintCharge < sprintLength)
                    sprintCharge += sprintRecharge * Time.deltaTime;
            }
        }

        sprintPercentText.text = Mathf.Round((sprintCharge / sprintLength) * 100).ToString();
        sprintPercentText.text = "Sprint: " + sprintPercentText.text;

        if (Mathf.Round(sprintCharge / sprintLength * 100) <= 30)
        {
            sprintPercentText.color = Color.red;
        }
        else
        {
            sprintPercentText.color = Color.green;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (Time.time - timeSinceLastDistraction >= distractionReloadTime)
            {
                if (numberOfDistractions > 0)
                {
                    GameObject projectile = Instantiate(distraction) as GameObject;
                    projectile.transform.position = Camera.main.transform.position + transform.forward * 1.5f;
                    Rigidbody rb = projectile.GetComponent<Rigidbody>();
                    rb.velocity = Camera.main.transform.forward * 10f;

                    Vector3 spin = new Vector3(Random.Range(2, 3), 0, Random.Range(1, 3));
                    rb.angularVelocity = spin;

                    numberOfDistractions -= 1;
                }
                else
                {
                    Debug.Log("out of distractions");
                }

                timeSinceLastDistraction = Time.time;
            }
        }

        //movement controls
        if (controller.isGrounded) //if the controller is on ground, calculate input movement
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        moveDirection.y -= gravity * Time.deltaTime; //gravity is constant
        controller.Move(moveDirection * Time.deltaTime); //execute move command
    }

    void FixedUpdate ()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(Camera.transform.position, Vector3.forward);

        if (Physics.Raycast(ray, out hit, 5f))
        {
            if (hit.transform.tag == "Enemy")
            {
                //Debug.Log(hit.transform.name);

                if(Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponentInParent<StateController>().StealthHit();
                }
            }
        }

    }
}
