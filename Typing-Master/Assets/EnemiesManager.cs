using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemiesManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Sprite[] enemySprites;

    public void MakeRandomShip()
    {
        Console.WriteLine("DDD");
        int arraIdx = UnityEngine.Random.Range(0, enemySprites.Length);
        Sprite shipSprite = enemySprites[arraIdx];
        string shipName = shipSprite.name;

        GameObject go = Instantiate(enemyPrefab);
        go.name = shipName;
        go.GetComponent<EnemyClass>().shipName = shipName;

        go.GetComponent<SpriteRenderer>().transform.position = new Vector2(UnityEngine.Random.Range(-3, 3), 6);

        go.GetComponent<SpriteRenderer>().sprite = shipSprite;

    }

    void Start()
    {
       for(int i = 0; i < 10; i++)
        {
            MakeRandomShip();
        }
    }

}
