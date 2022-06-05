using UnityEditor;
using UnityEngine;
using TARGET = DisplayWorldPos;

public class DisplayWorldPos : MonoBehaviour
{
}

#if UNITY_EDITOR
// Use world coordinates from transform in editor instead of local
[CustomEditor(typeof(TARGET))]
[CanEditMultipleObjects]
public class DisplayWorldPosEditor : Editor
{
    override public void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        DisplayWorldPos myTarget = (DisplayWorldPos)target;
        myTarget.gameObject.transform.position = EditorGUILayout.Vector3Field("World Pos", myTarget.transform.position);
        //this will display the target's world pos.
        EditorGUILayout.EndHorizontal();

    }
}
#endif