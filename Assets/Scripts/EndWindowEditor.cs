using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EndWindow))]
class DecalMeshHelperEditor : Editor {
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EndWindow endWindow = (EndWindow)target;
        if (GUILayout.Button("Vizualize"))
        {
            endWindow.ActivateWindow();
        }
            
    }
}
