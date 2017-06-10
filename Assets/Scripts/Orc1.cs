using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Orc1 : MonoBehaviour {
	bool sound=true;
	public AudioClip attackSound = null;
	AudioSource attackSource = null;
	public float speed=2;
	Rigidbody2D myBody;
	SpriteRenderer mySprite;
	Animator animator;
	int health = 1;

	public Vector3 pointA;
	public Vector3 pointB;

	public bool isDead(){
		return this.health == 0;
	}

	public BoxCollider2D headCollider;
	public BoxCollider2D bodyCollider;

	float patrolDistance = 2;

	Mode mode=Mode.GoToB;
	// Use this for initialization
	void Start () {
		attackSource = gameObject.AddComponent<AudioSource> ();
		attackSource.clip = attackSound;
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

		//Перевірка чи кролик зайшов в зону патрулювання
		if (rabit_pos.x >= Mathf.Min (pointA.x, pointB.x)&& rabit_pos.x <= Mathf.Max (pointA.x, pointB.x)){
			mode = Mode.Attack;
		}else {
			
				if(my_pos.x >= pointB.x) {
					this.mode = Mode.GoToA;
				} 
				
				if(my_pos.x <= pointA.x) {
					this.mode = Mode.GoToB;
				 
			}
		}
		if(mode == Mode.Attack) {
			//Move towards rabit
			if(my_pos.x < rabit_pos.x) {
				return 1;
			} else {
				return -1;
			}
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

	public void attackRabit(){
		if(sound)
		attackSource.Play();
		animator.SetTrigger ("attackRabit");

	}

	public void setSoundOff(){
		sound = false;
	}
	public void setSoundOn(){
		sound = true;
	}
}
