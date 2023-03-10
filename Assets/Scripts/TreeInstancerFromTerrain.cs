using UnityEngine;

public class TreeInstancerFromTerrain : MonoBehaviour
{
    [SerializeField]
    private Terrain terrain;

    public void GenerateTrees()
    {
        if (terrain == null) return;
        
        var terrainTerrainData = terrain.terrainData;
        var trees = terrainTerrainData.treeInstances;
        var prototypes = terrainTerrainData.treePrototypes;

        foreach(var tree in trees)
        {
            var treePosition = Vector3.Scale(tree.position, terrainTerrainData.size) + terrain.transform.position;
            var prototypeIndex = tree.prototypeIndex;
            var treePrefab = prototypes[prototypeIndex].prefab;
            var newTree = Instantiate(treePrefab, treePosition, treePrefab.transform.rotation, treePrefab.transform);
            newTree.transform.parent = transform;
        }
    }
}
