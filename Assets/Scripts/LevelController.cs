using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LevelController : MonoBehaviour {
	public static LevelController current;
	void Awake() {
		current = this;
	}
	Vector3 startingPoint;

	public void setRebitStaringPoint(Vector3 pos){
		startingPoint = pos;
	}

	public void onRabitDeath(HeroRabit rabit){
		rabit.transform.position = this.startingPoint;
	}
}