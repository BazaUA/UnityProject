using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroRabit : MonoBehaviour {
	public float speed=1;
	public bool isMove;
	Rigidbody2D myBody;
	SpriteRenderer mySprite;
	Animator animator;
	bool isGrounded = false;
	bool JumpActive = false;
	float JumpTime = 0f;
	public float MaxJumpTime = 2f;
	public float JumpSpeed = 2f;
	Transform heroParent = null;
	public int MaxHealth = 2;
	int health = 1;


	public static HeroRabit lastRabit = null;
	// Use this for initialization
	void Awake() {
		lastRabit = this;
	}

	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		mySprite = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		LevelController.current.setRebitStaringPoint (transform.position);
		this.heroParent = this.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		if (isMove) {
			if (health > 0) {
				Vector3 from = transform.position + Vector3.up * 0.3f;
				Vector3 to = transform.position + Vector3.down * 0.1f;
				int layer_id = 1 << LayerMask.NameToLayer ("Ground");
				//Перевіряємо чи проходить лінія через Collider з шаром Ground
				RaycastHit2D hit = Physics2D.Linecast (from, to, layer_id);
				if (hit) {
					isGrounded = true;
				} else {
					isGrounded = false;
				}
				//Намалювати лінію (для розробника)
				Debug.DrawLine (from, to, Color.red);

				if (Input.GetButtonDown ("Jump") && isGrounded) {
					this.JumpActive = true;
				}
				if (this.JumpActive) {
					//Якщо кнопку ще тримають
					if (Input.GetButton ("Jump")) {
						this.JumpTime += Time.deltaTime;
						if (this.JumpTime < this.MaxJumpTime) {
							Vector2 vel = myBody.velocity;
							vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
							myBody.velocity = vel;
						}
					} else {
						this.JumpActive = false;
						this.JumpTime = 0;
					}
				}

				float value = Input.GetAxis ("Horizontal");

				if (Mathf.Abs (value) > 0) {
					Vector2 vel = myBody.velocity;
					vel.x = value * speed;
					myBody.velocity = vel;
				}
				if (value < 0)
					mySprite.flipX = true;
				else if (value > 0)
					mySprite.flipX = false;
		
				if (Mathf.Abs (value) > 0) {
					animator.SetBool ("run", true);
				} else {
					animator.SetBool ("run", false);
				}

				if (this.isGrounded) {
					animator.SetBool ("jump", false);
				} else {
					animator.SetBool ("jump", true);
				}

				if (hit) {
					if (hit.transform != null && hit.transform.GetComponent<MovingPlatform> () != null) {
						SetNewParent (this.transform, hit.transform);
					}
				} else {
					SetNewParent (this.transform, this.heroParent);
				}
			}
		}
	}

	static void SetNewParent(Transform obj, Transform new_parent){
		if (obj.transform.parent != new_parent){
			Vector3 pos = obj.transform.position;
			obj.transform.parent = new_parent;
			obj.transform.position = pos;
		}
	}

	public void addHealth(int number){
		this.health += number;
		if(this.health > MaxHealth){
			this.health = MaxHealth; 
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
		animator.SetBool ("die",true);
		yield return new WaitForSeconds(duration);
		LevelController.current.onRabitDeath(this);
		animator.SetBool ("die",false);
		addHealth (1);
	}


	public void makeBigger(){
			this.transform.localScale = Vector3.one * 2;
	}


	public void OnTriggerEnter2D(Collider2D collider){
		if (this.health > 0) {
			Orc1 orc = collider.gameObject.GetComponent<Orc1> ();
			Orc2 orc2 = collider.gameObject.GetComponent<Orc2> ();
			if (orc != null) {
				if (!orc.isDead ()) {
					if (collider == orc.bodyCollider) {
						this.removeHealth (1);
						orc.attackRabit ();
					} else if (collider == orc.headCollider) {
						orc.removeHealth (1);
					}
				}
			}
			if (orc2 != null) {
				if (!orc2.isDead ()) {
					 if (collider == orc2.headCollider) {
						orc2.removeHealth (1);
					}
				}
			}
		}
	}

}
