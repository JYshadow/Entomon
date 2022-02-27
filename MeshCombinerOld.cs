using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Text;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshCombinerOld : MonoBehaviour
{
    [HideInInspector] public string savedPath;
    [HideInInspector] public string savedMeshName;

    MeshFilter[] meshFilters;
    CombineInstance[] combine;

    public void CombineMeshAndSave()
    {
        meshFilters = GetComponentsInChildren<MeshFilter>();
        combine = new CombineInstance[meshFilters.Length];

        for (int i = 1; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = transform.worldToLocalMatrix * meshFilters[i].transform.localToWorldMatrix;
        }
        var meshFilter = transform.GetComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combine);

#if UNITY_EDITOR
        AssetDatabase.CreateAsset(meshFilter.mesh, "Assets/Prefabs/CombinedMeshes/Manual/" + meshFilter.mesh.name + " (Manual).asset");
        AssetDatabase.SaveAssets();
#endif
    }
}
