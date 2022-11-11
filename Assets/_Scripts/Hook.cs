using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private RodManager rodManager;
    [SerializeField] private BaitManager baitManager;

    public Vector3 targetPosition {get; set;}

    bool isFloating;

    void Start() => Reset();

    public void Reset()
    {
        targetPosition = new Vector3(0, 1, 3);
        isFloating = false;
    }

    void OnMouseDown()
    {
        if(rodManager.state != RodState.BAIT) return;
        rodManager.ApplyBait();
        baitManager.Reset();
    }

    void Update()
    {
        float offset = 0;

        if(isFloating)
        {
            offset = Mathf.Sin(Time.time * Mathf.PI * 2) * 0.1f;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition + Vector3.up * offset, Time.deltaTime * 10);
        transform.up = Vector3.up;
    }

    public void Float()
    {
        targetPosition = new Vector3(0, 0, 7);
        isFloating = true;
    }



}
