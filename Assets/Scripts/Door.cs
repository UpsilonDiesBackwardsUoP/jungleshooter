using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Door : MonoBehaviour
{
    public bool hasKey;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasKey == true)
        {
            Destroy(gameObject);
        }
    }
}
