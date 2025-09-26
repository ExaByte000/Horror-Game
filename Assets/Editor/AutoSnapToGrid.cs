using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class AutoSnapToGrid : MonoBehaviour
{
    [MenuItem("Tools/Snap Selected To Grid %#h")] // Ctrl+Shift+H
    static void SnapSelected()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Undo.RecordObject(obj.transform, "Snap To Grid");

            Vector3 pos = obj.transform.position;
            pos.x = Mathf.Round(pos.x);
            pos.y = Mathf.Round(pos.y);
            pos.z = Mathf.Round(pos.z);
            obj.transform.position = pos;
        }
    }
}