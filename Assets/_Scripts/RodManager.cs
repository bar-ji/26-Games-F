using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RodManager : MonoBehaviour
{
    public RodState state;

    [SerializeField] private GameObject bait;

    [SerializeField] private LineRenderer lr;
    [SerializeField] private Hook hook;
    [SerializeField] private Transform line;

    [SerializeField] private PianoTiles pianoTiles;

    [SerializeField] private Transform fish;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject reelCam;

    [SerializeField] private Transform reel;

    [SerializeField] private GameObject[] caughtFish;

    [SerializeField] private TMPro.TMP_Text reelTimer;
    [SerializeField] private AudioSource baitSFX;
    [SerializeField] private AudioSource splash;


    public float difficulty {get; set;}


    Quaternion targetRotation;

    float reelRot;

    float timeStartedReel;


    void Start()
    {
        Reset();
    }   

    void Update()
    {
        lr.SetPosition(0, line.position);
        lr.SetPosition(1, hook.transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);

        reelTimer.gameObject.SetActive(state == RodState.REEL);
        reelTimer.text = (Mathf.Lerp(2, 5, difficulty) - (Time.time - timeStartedReel)).ToString("F2");

        if(state == RodState.REEL)
        {
            if(Time.time - timeStartedReel > Mathf.Lerp(2, 5, difficulty))
            {
                FailedReel();
            }

            reelRot += Input.mouseScrollDelta.y * 10;
            reel.localRotation = Quaternion.Euler(reelRot, 0, 0);

            if(reelRot < -1440)
            {   
                SuccessfulReel();
            }
        }

    }

    void Reset()
    {
        bait.SetActive(false);
        reelCam.SetActive(false);
        hook.Reset();
        targetRotation = Quaternion.Euler(0, -60, 0);
        state = RodState.BAIT;
        reelRot = 0;
        FindObjectOfType<PianoTiles>().Reset();
    }

    public void ApplyBait()
    {
        bait.SetActive(true);
        StartCast();
        baitSFX.Play();
    }

    public void StartCast()
    {
        state = RodState.CAST;
        targetRotation = Quaternion.Euler(-60, 0, 0);
        hook.targetPosition = new Vector3(0, 1.5f, 0);
        pianoTiles.Play();
    }

    public void FailedCast()
    {
        Reset();
    }

    public void SuccessfulCast(float diff)
    {
        this.difficulty = diff;
        targetRotation = Quaternion.identity;
        hook.Float();
        StartCatch();
    }

    void StartCatch()
    {
        state = RodState.CATCH;
        var _fish = Instantiate(fish, spawnPoint.position, fish.rotation).GetComponent<Fish>();
        _fish.Init(difficulty);

        targetRotation = Quaternion.Euler(-30, 0, 0);
        hook.targetPosition = new Vector3(0, -1.5f, 3);

        splash.Play();
    }

    public void FailedCatch()
    {
        Reset();
    }

    public void CaughtFish()
    {
        StartReel();
    }

    void StartReel()
    {
        state = RodState.REEL;
        timeStartedReel = Time.time;
        reelCam.SetActive(true);
    }

    void SuccessfulReel()
    {
        Reset();

        foreach(GameObject fish in caughtFish)
        {
            if(!fish.activeSelf)
            {
                fish.SetActive(true);
                break;
            }
        }

    }

    void FailedReel()
    {
        Reset();
    }
}

public enum RodState
{
    BAIT,
    CAST,
    CATCH,
    REEL,
    COMPLETE
}
