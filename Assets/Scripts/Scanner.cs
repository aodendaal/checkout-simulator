using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scanner : MonoBehaviour, IPointerEnterHandler
{
    public Text checkoutText;

    private Image scannerLight;
    private Image readerLight;
    private AudioSource audioSource;

    private bool isLightOn = false;
    private float lightTime;
    private float lightRate = 0.5f;

    public List<Description> Items = new List<Description>();

    void Start()
    {
        scannerLight = GameObject.Find("ScannerLight").GetComponent<Image>();
        readerLight = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isLightOn && Time.time >= lightTime)
        {
            TurnLightOff();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isLightOn)
        {
            return;
        }

        if (DragHandler.ItemBeingDragged != null)
        {
            var item = DragHandler.ItemBeingDragged.GetComponent<Description>();

            Items.Add(item);

            UpdateText();

            TurnLightOn();
            audioSource.Play();
        }
    }

    private void UpdateText()
    {
        var text = Items.GroupBy(i => i.displayName)
                          .OrderBy(i => i.Key)
                          .Select(g => g.Key.PadRight(50) + " x" + g.Count().ToString().PadRight(20) + (g.First().price * g.Count()).ToString("0.00"))
                          .Aggregate((a, n) => a + "\n" + n);

        text += "\n---------------------------------------------";
        text += "\n" + "Total".PadRight(83) + Items.Sum(i => i.price).ToString("0.00");

        checkoutText.text = text;
    }

    private void TurnLightOn()
    {
        scannerLight.color = Color.green;
        readerLight.color = Color.red;
        isLightOn = true;
        lightTime = Time.time + lightRate;
    }

    private void TurnLightOff()
    {
        scannerLight.color = Color.white;
        readerLight.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        isLightOn = false;
    }

    public void Clear()
    {
        Items.Clear();
        checkoutText.text = string.Empty;
    }
}