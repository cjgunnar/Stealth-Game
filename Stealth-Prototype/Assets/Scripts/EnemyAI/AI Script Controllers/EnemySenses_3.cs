using UnityEngine;

public class EnemySenses_3 : MonoBehaviour
{
    //responsible for sight and hearing

    //line of sight variables
    public float viewRange = 15;
    [Range(0, 360)]
    public float viewAngle = 90;
    RaycastHit hit;

    float sphereRadius = 0.1f;

    //public float chaseMultiplier;

    //private bool lastFramePlayerInSight = false;

    /* Whatever that is
    [SerializeField]
    private float viewTime = 0;
    [SerializeField]
    private float viewTimeLastTime = 0;
    public float viewTimeResetTime = 100;
    */

    //update contiunously, once visual lost go here
    [HideInInspector]
    public Vector3 lastSeenlocation;

    //hearing
    [HideInInspector]
    public Vector3 noisePosition;

    //to check if noise heard
    [HideInInspector]
    public Vector3 defaultNoisePosition;

    //player gameObject
    GameObject player;
    private bool playerIsActive;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerIsActive = true;
        lastSeenlocation = Vector3.zero;
        defaultNoisePosition = new Vector3(1000f, 1000f, 1000f);
        noisePosition = defaultNoisePosition;
    }

    #region Events

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

    #endregion

    public bool CheckLineOfSight ()
    {
        if (!playerIsActive)
        {
            return false;
        }

        //Debug.Log("checking enemy line of sight...");

        //set up a spherecast to the player, then check if it is clear line of sight and that it is within proper range and angle restrictions
        //store and modify player position Vector3
        Vector3 playerPosition = player.transform.position;
        playerPosition.y -= 1;

        Vector3 direction = (playerPosition - transform.position).normalized;
        //direction.y = 0;
        float angle = Vector3.Angle(direction, transform.forward);
        Vector3 origin = transform.position;
        origin.y += 1;

        //returns true if anything hit, facing correctly, and object hit has a tag of "player"
        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, viewRange) && angle < (viewAngle / 2) && hit.transform.tag == "Player")
        {
            Debug.DrawLine(origin, hit.point, Color.green);

            lastSeenlocation = player.transform.position;

            return true;
        }

        else
        {
            Debug.DrawLine(origin, hit.point, Color.red);

            return false;
        }

    }

    //for calculating angles in editor view
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}