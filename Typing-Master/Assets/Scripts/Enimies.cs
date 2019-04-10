using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
  
        GameObject prefab = Resources.Load("enemyBlue1") as GameObject;
        for(int i = 0; i < 100; i++)
        {
            GameObject go = Instantiate(prefab) as GameObject;
            go.transform.position = new Vector2(i, i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
