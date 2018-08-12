using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float speed;   

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            var parentTransform = transform as RectTransform;
            var width = parentTransform.rect.width;

            var children = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);

                if (child.name != "Description")
                {
                    children.Add(transform.GetChild(i));
                }
            }

            foreach (var child in children)
            {
                var rect = child as RectTransform;
                if (rect.position.x < width - 32f)
                {
                    rect.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
                }
            }
        }
    }
}