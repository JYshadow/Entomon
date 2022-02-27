using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingInstruction : MonoBehaviour
{
    [Header("Public")]
    public string craftableName;

    private void Awake()
    {
        GetComponent<MeshCollider>().convex = true;
        GetComponent<MeshCollider>().isTrigger = true;
    }
}
