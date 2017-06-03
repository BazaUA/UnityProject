using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LevelController : MonoBehaviour {
	public static LevelController current;
	public int coins = 0;
	public int fruits = 0;
	public int crystals = 0;
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

	public void addCoins(int n){
		coins += n;
	}

	public void addFruit(int n){
		fruits += n;
	}

	public void addCrystal(int n){
		crystals += n;
	}
}