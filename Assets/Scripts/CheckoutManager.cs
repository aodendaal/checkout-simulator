using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CheckoutManager : MonoBehaviour
{
    [Header("Panels")]
    public Transform conveyerTransform;
    public Scanner scanner;
    public Text resultText;

    [Header("Buttons")]
    public GameObject nextButton;
    public GameObject checkoutButton;

    [Header("Items")]
    public GameObject[] itemPrefabs;
    
    private List<GameObject> items = new List<GameObject>();

    private AudioSource nextSource;
    private AudioSource successSource;
    private AudioSource failSource;

    private int day = 1;

    private int score = 0;
    private int total = 0;

    // Use this for initialization
    void Start()
    {
        AssignAudioSources();

        nextButton.SetActive(true);
        checkoutButton.SetActive(false);
    }

    private void AssignAudioSources()
    {
        var sounds = GetComponents<AudioSource>();
        nextSource = sounds[0];
        successSource = sounds[1];
        failSource = sounds[2];
    }

    public void Next_Click()
    {
        nextSource.Play();

        var parentTransform = conveyerTransform as RectTransform;
        var height = parentTransform.rect.height;

        var count = 0;
        if (day == 1)
        {
            count = Random.Range(1, 11);
        }
        else if (day % 5 == 0)
        {
            count = Random.Range(5, 31);
        }
        else
        {
            count = Random.Range(3, 17);
        }

        for (int i = 0; i < count; i++)
        {
            var prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

            var go = Instantiate(prefab, parentTransform, false);

            go.transform.position -= new Vector3(Random.Range(-50f, 0f), Random.Range(0f, height - 50f), 0f);

            items.Add(go);
        }

        total++;
        UpdateResult();

        nextButton.SetActive(false);
        checkoutButton.SetActive(true);
        scanner.Clear();
    }

    public void Checkout_Click()
    {
        CheckAccuracy();

        ClearItems();
    }

    private void ClearItems()
    {
        foreach (var item in items)
        {
            Destroy(item);
        }

        items.Clear();

        nextButton.SetActive(true);
        checkoutButton.SetActive(false);
    }

    private void CheckAccuracy()
    {
        if (scanner.Items.Count != items.Count)
        {
            failSource.Play();
            return;
        }

        score += 1;
        UpdateResult();

        successSource.Play();
    }

    private void UpdateResult()
    {
        resultText.text = string.Format("You scored {0}% out of {1}", Mathf.FloorToInt((float)score / (float)total * 100f), total);
    }

    public void NextDay(int day)
    {
        this.day = day;

        score = 0;
        total = 0;

        ClearItems();
    }
}
