using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable {
	public float timeToDestroy;
	public float speed;

	protected override void OnRabitHit (HeroRabit rabit){
		rabit.removeHealth (1);
		this.CollectedHide ();
	}

	 float my_direction = 0;

	public void launch (float direction){
		this.my_direction = direction;
		if (direction < 0) {
			this.GetComponent<SpriteRenderer> ().flipX = true;
		}
		StartCoroutine (destroyLater());
	}

	IEnumerator destroyLater(){
		yield return new WaitForSeconds (timeToDestroy);

		Destroy (this.gameObject);
	}

	void Update(){
		Vector3 pos = this.transform.position;
		pos.x += Time.deltaTime * my_direction * speed;
		this.transform.position = pos;
	}
}
