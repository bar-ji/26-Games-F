using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BaitManager : MonoBehaviour
{
    [SerializeField] private Transform bait;
    [SerializeField] private RodManager rodManager;

    bool isHoldingBait;
        
    public void OnMouseDown()
    {
        if(rodManager.state != RodState.BAIT) return;

        isHoldingBait = true;
        bait.localScale = Vector3.zero;
        bait.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutSine);
    }

    void Update()
    {
        if(isHoldingBait)
        {
            bait.gameObject.SetActive(true);
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.transform.position.z + 2;
            bait.position = Camera.main.ScreenToWorldPoint(mousePos);
        }
        else
        {
            bait.gameObject.SetActive(false);
        }
    }

    public void Reset() => isHoldingBait = false;
}
