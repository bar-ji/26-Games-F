using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PianoTiles : MonoBehaviour
{
    [SerializeField] private Transform tile;

    [HideInInspector] public List<GameObject> aliveTiles = new List<GameObject>();

    bool active;

    float difficulty;

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        foreach(GameObject o in aliveTiles)
            if(o)
                Destroy(o);
        
        aliveTiles = new List<GameObject>();

        transform.localScale = Vector3.zero;

        active = false;
    }

    public void Play()
    {
        transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutSine);
        float speed = Random.Range(200, 800);
        difficulty = (speed + 200) / 1000;
        for(int i = 0; i < 10; i++)
        {
            int rand = Random.Range(0, 5);

            if(rand == 0)
                rand = -150;
            else if(rand == 1)
                rand = -50;
            else if(rand == 2)
                rand = 50;
            else
                rand = 150;

            Tile t = Instantiate(tile, Vector3.zero, Quaternion.identity, transform).GetComponent<Tile>();
            t.transform.localPosition = new Vector3(rand, 300 + (i * 152), 0);
            t.Init(speed, this);
            aliveTiles.Add(t.gameObject);
        }
        active = true;
    }

    void Update()
    {
        if(active && aliveTiles.Count == 0)
        {
            Reset();
            GameObject.FindObjectOfType<RodManager>().SuccessfulCast(difficulty);
        }
    }

}
