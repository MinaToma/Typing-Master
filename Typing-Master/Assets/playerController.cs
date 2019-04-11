using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
//

    
    public GameObject shot;
    public Transform ship;
    public float angle;
    public Transform target;
    private int targetIdx = 1;
    void Start ()
    {
    }
	
	void Update ()
    {

        Debug.Log(EnemiesManager.enemyShips.Count);
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if(Input.GetKeyDown(vKey))
            {
                if(targetIdx == -1)
                {
                    for(int i = 0; i < EnemiesManager.enemyShips.Count; i++)
                     {
                       /* if (Input.GetKeyDown(vKey).Equals(EnemiesManager.enemyShips.shpot[0p]) )
                        {
                            Vector3 shotPos = ship.position;
                            shotPos.y += 0.5f;
                            Instantiate(shot, shotPos, ship.rotation); 
                        }*/

                    }

                }
                else
                {
                
                    target = EnemiesManager.enemyShips[0].transform;
                    Vector3 shotPos = ship.position;
                    shotPos.y += 0.5f;
                    rotate();
                    Instantiate(shot, shotPos, ship.rotation);

                }
            }
        }
    }

    void rotate()
    {
        Vector3 offset = target.position - ship.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offset);
        ship.rotation = rotation;
    }
}
