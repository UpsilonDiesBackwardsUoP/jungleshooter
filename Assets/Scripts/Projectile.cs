using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    Camera cam = Camera.main;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos += Camera.main.transform.forward * 10f; 
        Vector3 aim = Camera.main.ScreenToWorldPoint(mousePos);

        rb.AddRelativeForce(aim * 2);

        /*Vector3 mousePos = Input.mousePosition;

        Vector3 aim = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));


        rb.AddRelativeForce(aim * 2)*/
    }
}
