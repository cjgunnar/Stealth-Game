using UnityEngine;

public class Distraction : MonoBehaviour {

    public LayerMask enemyLayer;

    public float noise;

    private AudioSource audioSource;

	void Start () {
        //find a way to set enemyLayer layer

        audioSource = GetComponent<AudioSource>();
	}
	
    void OnCollisionEnter(Collision collision)
    {
        PlayLandingSound();

        Collider[] inRange = Physics.OverlapSphere(transform.position, noise, enemyLayer);

        for (int i = 0; i < inRange.Length; i++)
        {
            //Debug.Log("In range: " + inRange[i].transform.name);
            try
            {
                //Debug.Log(inRange[i]);
                if (inRange[i].GetComponent<EnemySenses_3>() != null)
                    inRange[i].GetComponent<EnemySenses_3>().noisePosition = transform.position;
                //else
                //    inRange[i].GetComponentInParent<EnemySenses_3>().noisePosition = transform.position;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }

        //remove this script from coin so it doesn't repeat itself
        //if it is bumped into or something
        Destroy(this, 2f);
    }

    private void PlayLandingSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.Play();
    }

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, noise);
    }

}
