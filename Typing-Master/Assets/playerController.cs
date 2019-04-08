using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    
    private Rigidbody2D playerRigid2d;
    public GameObject shot;
    public Transform ship;
    
    void Start ()
    {
        playerRigid2d = GetComponent<Rigidbody2D>();
    }
	
	void Update ()
    {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(vKey))
            {
                Instantiate(shot, ship.position, ship.rotation);
            }
        }
    }

    void RotateLeft ()
    {
        transform.Rotate(Vector3.forward * -5);
    }

    void RotateRight()
    {
        transform.Rotate(Vector3.forward * 5);
    }
}
