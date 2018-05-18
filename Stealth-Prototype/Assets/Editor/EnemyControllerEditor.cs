using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(EnemySenses_3))]
public class EnemyControllerEditor : Editor
{

    void OnSceneGUI()
    {
        EnemySenses_3 fov = (EnemySenses_3)target;
        Handles.color = Color.white;
        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);
        Handles.DrawWireArc(fov.transform.position, Vector3.up, viewAngleA, fov.viewAngle, fov.viewRange);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRange);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRange);
    }
}