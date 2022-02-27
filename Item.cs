using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Public")]
    public GameObject[] components;
    public string[] elements;

    public float offset;

    private void Awake()
    {
        if (gameObject.GetComponent<MeshCollider>() != null)
        {
            GetComponent<MeshCollider>().convex = true;
            GetComponent<MeshCollider>().isTrigger = true;
        }
    }

    private void Start()
    {
        gameObject.tag = "Item";
    }
}
