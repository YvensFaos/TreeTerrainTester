using UnityEngine;
using Utils;

namespace a
{
    [UnityEditor.CustomEditor(typeof(TreeInstancerFromTerrain))]
    public class TreeInstancerFromTerrainEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var tree = (TreeInstancerFromTerrain)target; 
            
            if (GUILayout.Button("Generate Trees"))
            {
                tree.GenerateTrees();
            }

            if (GUILayout.Button("Clear"))
            {
                TransformHelper.ClearObjects(tree.transform);
            }
        }
    }
}