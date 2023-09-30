using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    public Vector3 MovementVector;
    public float Speed = 10.0f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 5.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position += MovementVector * Speed * Time.deltaTime;
    }
}
