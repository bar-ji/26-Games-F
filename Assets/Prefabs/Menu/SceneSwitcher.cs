using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }


    public void Switch(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
