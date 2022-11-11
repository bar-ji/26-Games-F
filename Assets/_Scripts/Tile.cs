using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    float speed;
    PianoTiles p;

    public void Init(float speed, PianoTiles p)
    {
        this.speed = speed;
        this.p = p;
    }

    void Update()
    {
        transform.localPosition -= Vector3.up * speed * Time.deltaTime;
        if(transform.localPosition.y < -275)
        {
            GameObject.FindObjectOfType<RodManager>().FailedCast();
            p.Reset();
            Destroy(gameObject);
        }

        if(transform.localScale.magnitude == 0)
            Destroy(gameObject);
    }

    public void Clicked()
    {
        p.aliveTiles.Remove(gameObject);
        transform.DOScale(Vector3.zero, 0.1f);
    }
}
