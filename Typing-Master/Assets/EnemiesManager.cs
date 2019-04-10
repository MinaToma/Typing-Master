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
        int arraIdx = UnityEngine.Random.Range(0, enemySprites.Length);
        Sprite shipSprite = enemySprites[arraIdx];
        string shipName = shipSprite.name;

        GameObject go = Instantiate(enemyPrefab);
        go.name = shipName;
        go.GetComponent<EnemyClass>().shipName = shipName;

        go.GetComponent<SpriteRenderer>().transform.position = new Vector2(UnityEngine.Random.Range(-4, 4), UnityEngine.Random.Range(4.5f, 10f));

        go.GetComponent<SpriteRenderer>().sprite = shipSprite;
        Console.WriteLine(go.GetComponent<Collider2D>().isTrigger);
    }

    void Start()
    {
       for(int i = 0; i < 15; i++)
        {
            MakeRandomShip();
        }
    }

}
