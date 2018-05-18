using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class PlayerNoiseEditor : Editor {

    //void OnSceneGUI ()
    //{
    //    PlayerController playerController = (PlayerController)target;
    //    Handles.color = Color.yellow;
    //    Vector3 playerLocation = playerController.transform.position;
    //    playerLocation.y -= 1;
    //    Handles.DrawWireDisc(playerLocation, Vector3.up, playerController.playerNoise);
    //}

}
