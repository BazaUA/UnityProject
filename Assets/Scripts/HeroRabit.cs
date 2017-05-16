using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroRabit : MonoBehaviour {
	public float speed=1;
	Rigidbody2D myBody;
	SpriteRenderer mySprite;

	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		mySprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		float value = Input.GetAxis ("Horizontal");

		if (Mathf.Abs (value) > 0) {
			Vector2  vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		}
		if (value < 0)
			mySprite.flipX = true;
		else if (value > 0)
			mySprite.flipX = false;
	}


}
