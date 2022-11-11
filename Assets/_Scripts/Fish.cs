using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float speed {get; private set;}

    float rotT;
    bool inc = true;

    bool isAlive = true;

    void Start()
    {
        transform.localScale = Vector3.one * Random.Range(0.1f, 1f);
    }

    public void Init(float diff)
    {
        speed = diff * 30;
    }

    void Update()
    {
        transform.position -= Vector3.right * speed * Time.deltaTime;

        Quaternion startRot = Quaternion.Euler(0, 10, 0);
        Quaternion endRot = Quaternion.Euler(0, -10, 0);

        rotT += (inc ? Time.deltaTime : -Time.deltaTime) * 5;

        if(inc && rotT >= 1)
            inc = false;
        else if(!inc && rotT <= 0)
            inc = true;

        transform.rotation = Quaternion.Slerp(startRot, endRot, Mathf.SmoothStep(0, 1, rotT));

        if(transform.localScale.magnitude == 0)
            Destroy(gameObject);

        if(transform.position.x < -35)
        {
            KillFish();
            GameObject.FindObjectOfType<RodManager>().FailedCatch();
        }
    }

    void OnMouseDown()
    {
        if(!isAlive) return;
        KillFish();
        GameObject.FindObjectOfType<RodManager>().CaughtFish();
    }

    void KillFish()
    {
        isAlive = false;
        transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutSine);
    }
}
