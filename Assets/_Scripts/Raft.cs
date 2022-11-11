using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raft : MonoBehaviour
{
    [SerializeField] private float floatSpeed;
    [SerializeField] private float floatHeight;
    [SerializeField] private float rotateSpeed;

    float t;
    float rotateT;
    bool inc = true;


    float startHeight;

    void Start()
    {
        startHeight = transform.position.y;
    }

    void Update()
    {
        t += Time.deltaTime;

        float height = Mathf.Sin(Mathf.PI * 2 * t * floatSpeed) * floatHeight;

        transform.position = new Vector3(transform.position.x, height, transform.position.z);

        rotateT += (inc ? Time.deltaTime : -Time.deltaTime) * rotateSpeed;

        if(rotateT >= 1 && inc)
            inc = false;
        if(rotateT <= 0 && !inc)
            inc = true;

        Quaternion endRot = Quaternion.Euler(-2, 0, 2);
        Quaternion startRot = Quaternion.Euler(2, 0, -2);
        transform.rotation = Quaternion.Lerp(startRot, endRot, Mathf.SmoothStep(0, 1, rotateT));


    }
}
