using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TMPro.TMP_Text scoreTxt;
    [SerializeField] private TMPro.TMP_Text scoreTxt2;

    public void EndGame()
    {
        endScreen.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreTxt.text = "Distance: " + score;
        scoreTxt2.text = "Distance: " + score;
    }

}
