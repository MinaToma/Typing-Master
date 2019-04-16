using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    
    public GameObject shot;
    public Transform ship;
    public float angle;
    private GameObject target = null;
    void Start ()
    {
    }
	
	void Update ()
    {

       foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if(Input.GetKeyDown(vKey))
            {
                string key = vKey.ToString();
                key = key.ToLower();
                Debug.Log(key);

                if (target == null)
                {
                    foreach(GameObject go in EnemiesManager.enemyShips)
                     {
                        Debug.Log((go.GetComponentInChildren<TextMesh>().text[0]));
                        if(key[0].Equals(go.GetComponentInChildren<TextMesh>().text[0]) && go.transform.position.y > 0)
                        {
                            
                            Vector3 shotPos = ship.position;
                            shotPos.y += 0.5f;
                            target = go;
                            rotate();
                            Instantiate(shot, shotPos, ship.rotation);


                            if (EnemiesManager.hit(go))
                                target = null;

                            break;
                        }
                    }
                }

                else if(key[0].Equals(target.GetComponentInChildren<TextMesh>().text[0]))
                {
                    target = EnemiesManager.enemyShips[0];
                    Vector3 shotPos = ship.position;
                    shotPos.y += 0.5f;
                    rotate();

                    if (EnemiesManager.hit(target))
                        target = null;

                    Instantiate(shot, shotPos, ship.rotation);
                }
            }
        }
    }

    void rotate()
    {
        Vector3 offset = target.transform.position - ship.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offset);
        ship.rotation = rotation;
    }
}
