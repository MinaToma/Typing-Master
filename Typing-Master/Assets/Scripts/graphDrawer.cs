using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class graphDrawer : MonoBehaviour
{

    //load data from file
    //fill graph

    [SerializeField] static private Sprite circleSprite;
    static private RectTransform graphContainer;
    public static List<int> value = new List<int>();


    public void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        showGraph(value, 1, 1, 1);
    }

    public static void addScoreToList(string fileName)
    {
        value.Clear();
        using (var sr = new StreamReader(fileName))
        {
            for (int i = 0; sr.Peek() != -1; i++)
            {
                string score = sr.ReadLine();
                if (i != 0)
                    value.Add(Int32.Parse(score));
            }
            sr.Close();
        }
    }
    static private GameObject createCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    static public void showGraph(List<int> valueList, float r, float g, float b)
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
                createConnection(prevDot.GetComponent<RectTransform>().anchoredPosition, curDot.GetComponent<RectTransform>().anchoredPosition, 1, 1, 1);
            }
            prevDot = curDot;
        }
    }

    static private void createConnection(Vector2 dotPositionA, Vector2 dotPositionB, float r, float g, float b)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(r, g, b, 0.5f);
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
