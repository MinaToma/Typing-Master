using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class graphDrawer : MonoBehaviour {

    //load data from file
    //fill graph

    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    List<int> value = new List<int>() { 5, 10, 21, 34 , 100 , 200 };

    public void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        showGraph(value);
    }

    public GameObject createCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle" , typeof(Image));
        gameObject.transform.SetParent(graphContainer , false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11 , 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }


    public void showGraph(List<int> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMax = 100;
        float xSize = 50f;

        GameObject prevDot = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = valueList[i] / yMax * graphHeight;
            GameObject curDot = createCircle(new Vector2(xPosition, yPosition));
            if (prevDot != null)
            {
                createConnection(prevDot.GetComponent<RectTransform>().anchoredPosition , curDot.GetComponent<RectTransform>().anchoredPosition);
            }
            prevDot = curDot;
        }
    }

    private void createConnection(Vector2 dotPositionA , Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer , false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * 0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
