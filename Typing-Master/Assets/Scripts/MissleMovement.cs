using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleMovement : MonoBehaviour {

    public float speed;

    private Rigidbody2D missle;
	void Start ()
    {
        missle = GetComponent<Rigidbody2D>();
        missle.velocity = transform.up * speed;
    }	
}
