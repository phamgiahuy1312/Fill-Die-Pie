using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public RectTransform largeCircle;
    public float speed = 5f;
    public float distance = 250f;

    private float angle;
    private RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        //Increase angle with time and speed
        angle += Time.deltaTime;

        //Calculate the new position for the small circle
        float x = Mathf.Cos(angle) * distance;
        float y = Mathf.Sin(angle) * distance;

        //update the position of the small circle
        rectTransform.localPosition = largeCircle.localPosition + new Vector3(x, y, 0);
    }
}
