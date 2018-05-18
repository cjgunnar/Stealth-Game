using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyPatrol))]
[RequireComponent(typeof(EnemySenses_3))]
[RequireComponent(typeof(EnemyAnimation))]
[RequireComponent(typeof(NavMeshAgent))]
public class StateController : MonoBehaviour {

    //states
    public State currentState;
    public EnemyStats enemyStats;
    public State remainState;
    public State previousState;

    //components
    [HideInInspector]
    public NavMeshAgent agent;

    //script components
    [HideInInspector]
    public EnemyPatrol patrol;

    [HideInInspector]
    public EnemySenses_3 enemySenses;

    [HideInInspector]
    public EnemyAnimation anim;

    //variables
    [HideInInspector]
    public bool aiActive = true;

    [HideInInspector]
    public bool alarmActive;

    [HideInInspector]
    public bool aware;

    public bool isReinforcment = false;

    [HideInInspector]
    public bool gameOver = false;

    public float searchRadius = 50;

    public LayerMask alarmLayer;

    //references
    [HideInInspector]
    public GameObject player;

    [HideInInspector]
    public Manager manager;

    [HideInInspector]
    public Statistics stats;

    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        patrol = GetComponent<EnemyPatrol>();
        enemySenses = GetComponent<EnemySenses_3>();
        anim = GetComponent<EnemyAnimation>();

        enemyStats = new EnemyStats();

        aiActive = true;
        alarmActive = false;
        aware = false;
        gameOver = false;

        player = GameObject.FindGameObjectWithTag("Player");
        manager = FindObjectOfType<Manager>();
        stats = FindObjectOfType<Statistics>();

        if (isReinforcment)
        {
            aiActive = false;
        }
        if (currentState != null && aiActive)
        {
            aiActive = true;
            TransitionToState(currentState);
        }

        //anim.AnimationWalk();


    }

    #region Events

    void OnEnable ()
    {
        FindObjectOfType<Manager>().GameOver += OnGameOver;
        FindObjectOfType<Manager>().alarmEvent += OnAlarm;
        FindObjectOfType<BackupTimer>().BackupCalled += OnBackupCalled;
    }

    void OnDisable ()
    {
        try
        {
            FindObjectOfType<Manager>().alarmEvent -= OnAlarm;
            FindObjectOfType<BackupTimer>().BackupCalled -= OnBackupCalled;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error Unsubscribing from Events: " + e);
        }
    }

    void OnAlarm ()
    {
        alarmActive = true;
        stats.activatedAlarm = true;
        aware = true;
    }

    void OnBackupCalled ()
    {
        if (!aiActive && isReinforcment)
        {
            aiActive = true;
            //make sure currentState is HUNT in the inspector
            TransitionToState(currentState);
        }
    }

    void OnGameOver()
    {
        gameOver = true;
    }

    #endregion

    void Update ()
    {

        if (!aiActive)
        {
            return;
        }
        //update state
        currentState.UpdateState(this);

        //set animation speed
        anim.SetAnimatorSpeed(agent.velocity.magnitude);
    }

    #region Hit and Death

    //dies if player is not in sight (unaware of him)
    public void StealthHit ()
    {
        if (!enemySenses.CheckLineOfSight())
        {
            Die();
        }
        
    }

    public void Die ()
    {
        //disable animator to ragdoll
        GetComponent<Animator>().enabled = false;

        //disable sounds
        GetComponent<AudioSource>().enabled = false;

        //stop moving
        agent.enabled = false;

        //disable this StateController script
        enabled = false;
    }

    #endregion

    #region State Machine Functions

    public void TransitionToState (State nextState)
    {
        if (nextState != remainState)
        {
            currentState.ExitState(this);
            previousState = currentState;
            currentState = nextState;
            Debug.Log("changing state to " + currentState.name);

            currentState.EnterState(this);
        }
    }

    public void TransitionToPreviousState ()
    {
        if (previousState != null)
        {
            //Debug.Log("Transitioning to previous state of " + previousState);
            TransitionToState(previousState);
        }
    }

    #endregion
}
