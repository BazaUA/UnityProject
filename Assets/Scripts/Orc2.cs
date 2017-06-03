using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Orc2 : MonoBehaviour {
	public float speed=2;
	Rigidbody2D myBody;
	SpriteRenderer mySprite;
	Animator animator;
	int health = 1;
	public GameObject carrot;

	public float radius;
	public float time;
	public Vector3 pointA;
	public Vector3 pointB;

	public bool isDead(){
		return this.health == 0;
	}

	public BoxCollider2D headCollider;
	public BoxCollider2D bodyCollider;
	float lastCarrot=0;
	float patrolDistance = 2;

	Mode mode=Mode.GoToB;
	// Use this for initialization
	void Start () {
		pointA = this.transform.position;
		pointB = pointA;
		if (patrolDistance < 0) {
			pointA.x += patrolDistance;
		} else {
			pointB.x += patrolDistance;
		}

		myBody = this.GetComponent<Rigidbody2D> ();
		mySprite = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

	}

	float getDirection(){
		Vector3 rabit_pos = HeroRabit.lastRabit.transform.position;
		Vector3 my_pos = this.transform.position;
		if(Mathf.Abs(rabit_pos.x - my_pos.x) < radius) {
			StartCoroutine (launchCarrot ());
			if(!isDead())
				this.launchCarrot ();
			return 0;
		}

		if(this.mode == Mode.GoToB) {
			if(my_pos.x >= pointB.x) {
				this.mode = Mode.GoToA;
			} 
		}
		else if(this.mode == Mode.GoToA) {
			if(my_pos.x <= pointA.x) {
				this.mode = Mode.GoToB;
			} 
		}
		if(mode == Mode.GoToB) {
			if(my_pos.x <= pointB.x) {
				return 1;
			} else {
				return -1;
			}
		}else if(mode == Mode.GoToA) {
			if(my_pos.x >= pointA.x) {
				return -1;
			} else {
				return 1;
			}
		}



		return 0;
	}


	void FixedUpdate(){
		float value = this.getDirection ();

		if (!isDead ()) {
			if (Mathf.Abs (value) > 0) {
				Vector2 vel = myBody.velocity;
				vel.x = value * speed;
				myBody.velocity = vel;
			}
		}
		if (value > 0)
			mySprite.flipX = true;
		else if (value < 0)
			mySprite.flipX = false;

		if (Mathf.Abs (value) > 0) {
			animator.SetBool ("walk", true);
		} else {
			animator.SetBool ("walk", false);
		}
	}

	public void removeHealth(int number){
		this.health -= number;
		if(this.health < 0){
			this.health = 0;
		}
		this.onHealthChange();
	}

	void onHealthChange(){
		if(this.health == 1){
			this.transform.localScale = Vector3.one;
		}else if(this.health == 0){
			StartCoroutine (die (2.0f));
		}
	}
	IEnumerator die (float duration){
		mode = Mode.Dead;
		foreach(BoxCollider2D collider in this.GetComponents<BoxCollider2D> () ){
			collider.enabled = false;
		}
		Destroy (this.myBody);
		animator.SetBool ("die",true);

		yield return new WaitForSeconds(duration);

		Destroy (this.gameObject);
		//LevelController.current.onRabitDeath(this);

	}
		

	IEnumerator launchCarrot() {
		Vector3 my_pos = this.transform.position;
		Vector3 rabit_pos = HeroRabit.lastRabit.transform.position;

		if (Mathf.Abs (rabit_pos.x - my_pos.x) < radius) {
			if (Time.time - this.lastCarrot > time) {
				this.lastCarrot = Time.time;
				animator.SetTrigger ("attackRabit");
				yield return new WaitForSeconds (0.2f);
				GameObject obj = GameObject.Instantiate (this.carrot);
				obj.transform.position = my_pos + Vector3.up * 0.5f;
				//Запускаємо в рух


				Carrot carrot = obj.GetComponent<Carrot> ();
				if (rabit_pos.x < my_pos.x) {
					carrot.launch (-1);
				} else {
					carrot.launch (1);
				}

			}
		}
	}
}