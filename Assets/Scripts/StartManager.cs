using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public void Start_Click()
    {
        SceneManager.LoadScene(1);
    }
}
