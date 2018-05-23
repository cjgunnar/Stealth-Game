using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensitivityAdjuster : MonoBehaviour {
	
    void Start()
    {
        
    }

	public void UpdateSensitivity(float sensitivity)
    {
        Camera.main.GetComponent<CameraController>().sensitivity = sensitivity;
        Debug.Log("Changed sensitivity to " + sensitivity);
    }

}
