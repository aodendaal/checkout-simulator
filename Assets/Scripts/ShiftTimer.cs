using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShiftTimer : MonoBehaviour
{
    [Header("Display")]
    public Text shiftTimeDisplay;

    [Header("Start Panel")]
    public GameObject startPanel;
    public Text startPanelTitle;
    public Text startPanelDescription;

    [Header("Panels")]
    public GameObject resultPanel;
    public GameObject pausePanel;

    private CheckoutManager manager;

    private bool isPlaying = false;
    private float totalShiftSeconds = 2f * 60f;
    private float shiftSeconds;
    private int day = 1;

    private void Start()
    {
        manager = GetComponent<CheckoutManager>();

        startPanelTitle.text = "Day 1 - Express Lane";
        startPanelDescription.text = "10 Items or Fewer";
        startPanel.SetActive(true);

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
        startPanel.SetActive(false);

        shiftSeconds = totalShiftSeconds;
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

        isPlaying = false;

        day++;

        manager.NextDay(day);

        if (day % 5 == 0)
        {
            startPanelTitle.text = string.Format("Day {0} - Sale", day);
            startPanelDescription.text = "Super Sale Day";
        }
        else
        {
            startPanelTitle.text = string.Format("Day {0}", day);
            startPanelDescription.text = "";
        }

        resultPanel.SetActive(false);
        startPanel.SetActive(true);
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