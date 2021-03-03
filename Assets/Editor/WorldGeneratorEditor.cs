using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WorldGenerator))]
public class WorldGeneratorEditor : Editor
{    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
            ((WorldGenerator)target).GenerateLevel();
    }
}
