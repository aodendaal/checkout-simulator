using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class CheckoutManager : MonoBehaviour
{

    [Header("Panels")]
    public Scanner scanner;

    [Header("Buttons")]
    public GameObject nextButton;
    public GameObject checkoutButton;

    [Header("Items")]
    public GameObject[] itemPrefabs;

    private Transform trolleyGridTransform;

    private List<GameObject> items = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        trolleyGridTransform = GameObject.Find("Trolley").transform.Find("Grid");

        nextButton.SetActive(true);
        checkoutButton.SetActive(false);
    }

    public void Next_Click()
    {
        var count = Random.Range(1, 11);

        for (int i = 0; i < count; i++)
        {
            var prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            var go = Instantiate(prefab, trolleyGridTransform.position, Quaternion.identity);

            go.transform.SetParent(trolleyGridTransform);

            items.Add(go);
        }

        nextButton.SetActive(false);
        checkoutButton.SetActive(true);
        scanner.Clear();
    }

    public void Checkout_Click()
    {
        Checkccuracy();

        foreach (var item in items)
        {
            Destroy(item);
        }

        nextButton.SetActive(true);
        checkoutButton.SetActive(false);
    }

    private void Checkccuracy()
    {
        throw new NotImplementedException();
    }
}
