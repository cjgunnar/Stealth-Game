using UnityEngine;

public class CameraDetector : MonoBehaviour {

    private Transform player;
    private Manager manager;

    [Range(0, 360)]
    public float viewAngle;

    private Light FOVlight;

    private bool playerIsActive;
    private bool alarmActive;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        manager = FindObjectOfType<Manager>();
        FOVlight = GetComponentInChildren<Light>();

        playerIsActive = true;
        alarmActive = false;
	}

    //subscribe and unsubscribe from events
    void OnEnable()
    {
        FindObjectOfType<Manager>().GameOver += OnGameOver;
        FindObjectOfType<Manager>().alarmEvent += OnAlarm;
    }

    void OnDisable()
    {
        FindObjectOfType<Manager>().GameOver -= OnGameOver;
        FindObjectOfType<Manager>().alarmEvent -= OnAlarm;
    }

    void OnGameOver()
    {
        playerIsActive = false;
    }

    void OnAlarm()
    {
        alarmActive = true;
        FOVlight.color = Color.red;
    }

    //can the camera see the player?
    void OnTriggerStay (Collider other)
    {
        if (other.tag == "Player" && playerIsActive)
        {

            //set up a raycast to the player, then check if it is clear line of sight and that it is within proper range and angle restrictions
            //store and modify player position Vector3
            Vector3 playerPosition = player.position;
            playerPosition.y -= 1;

            //Debug.Log("View: " + player.position);

            Vector3 direction = (playerPosition - transform.position).normalized;
            //direction.y = 0;
            float angle = Vector3.Angle(direction, -transform.right);

            RaycastHit hit;

            //Debug.Log(angle);
            //if (angle < viewAngle / 2)
            //{
            //    Debug.DrawLine(transform.position, player.position);
                
            //}

            //returns true if anything hit, facing correctly, and object hit has a tag of "player"
            if (Physics.Raycast(transform.position, direction, out hit) && angle < (viewAngle / 2) && hit.collider.tag == "Player")
            {
                //Debug.Log("raycast from enemy hit: " + hit.transform.name);
                PlayerSpotted();
                Debug.DrawLine(transform.position, hit.point, Color.green);
            }

            else
            {
                /*
                if (hit.transform.name != null)
                {
                    Debug.Log("raycast from enemy hit: " + hit.transform.name);
                }
                */
                
                Debug.DrawLine(transform.position, hit.point, Color.red);
            }

        }
    }

    void PlayerSpotted ()
    {
        if (!alarmActive)
        {
            manager.TriggerAlarm();
        }

        //CameraTrack();

    }

    /*
    Could not work out, attempt later
    void CameraTrack()
    {
        //disable animated sweep

        //look at while constraining to certain degrees
        //Debug.LogWarning("Track: " + player.position);
        Vector3 direction = (player.position - transform.position);
        //Vector3 direction = new Vector3(0, player.position.y - transform.position.y, player.position.z - transform.position.z);
        //direction.x = 0;
        //Vector3 direction = new Vector3(player.position.x, player.position.y, transform.position.z);
        //Debug.Log(direction);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            //Debug.Log("raycast from enemy hit: " + hit.transform.name);

            Debug.DrawLine(transform.position, hit.point, Color.green);
        }

        
        Quaternion toRotation = Quaternion.FromToRotation(-Vector3.right, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, .1f);
        //transform.localRotation = Quaternion.Lerp(transform.rotation, toRotation, .1f);

        //transform.localRotation = Quaternion.Euler(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
    */

}
