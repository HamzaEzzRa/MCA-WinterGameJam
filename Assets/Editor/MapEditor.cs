using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(8);
        
        if (GUILayout.Button("Generate Map"))
        {
            MapGenerator mapGen = (MapGenerator)target;
            mapGen.GenerateMap();
        }

        //if (GUI.changed)
        //{
        //    MapGenerator mapGen = (MapGenerator)target;
        //    mapGen.GenerateMap();
        //}
    }
}
