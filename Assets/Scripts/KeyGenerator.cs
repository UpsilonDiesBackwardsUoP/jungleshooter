using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class KeyGenerator : MonoBehaviour
{
    public GameObject[] spawns = new GameObject[0];
    [SerializeField] private GameObject choosenspawner;

    // Start is called before the first frame update
    void Start()
    {
        choosenspawner = spawns[Random.Range(0, spawns.Length)];
    }
}
