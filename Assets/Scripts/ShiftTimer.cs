using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShiftTimer : MonoBehaviour
{
    [Header("Display")]
    public Text shiftTimeDisplay;

    [Header("Panels")]
    public GameObject infoPanel;
    public GameObject resultPanel;
    public GameObject pausePanel;

    private bool isPlaying = false;
    private float shiftSeconds = 2f * 60f;

    private void Start()
    {
        infoPanel.SetActive(true);
        resultPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isPlaying)
        {
            shiftSeconds -= Time.deltaTime;

            var minutes = Mathf.FloorToInt(shiftSeconds / 60f);
            var remaining = shiftSeconds - minutes * 60f;
            var seconds = Mathf.FloorToInt(remaining);

            shiftTimeDisplay.text = string.Format("{0}:{1:00} Remaining", minutes, seconds);

            if (shiftSeconds < 1f)
            {
                isPlaying = false;
                resultPanel.SetActive(true);
            }
        }
    }

    public void StartGame_Click()
    {
        infoPanel.SetActive(false);
        
        isPlaying = true;
    }

    public void PauseGame_Click()
    {
        pausePanel.SetActive(true);

        isPlaying = false;
    }

    public void ContinueGame_Click()
    {
        pausePanel.SetActive(false);

        isPlaying = true;
    }

    public void NextGame_Click()
    {
        SceneManager.LoadScene(1);
    }

    public void RestartGame_Click()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame_Click()
    {
        Application.Quit();
    }
}