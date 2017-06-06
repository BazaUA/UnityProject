using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelController : MonoBehaviour {
	public LivesPanel livesPanel;
	public CrystalPanel crystalPanel;
	int rabitLives=3;
	public static LevelController current;
	public UILabel labelCoins;
	public UILabel labelFruits;
	public int coins = 0;
	public int fruits = 0;
	public int crystals = 0;


	void Awake() {
		
		current = this;
	}

	void Start(){
		if (labelCoins != null) {
			labelCoins.text="0000";
		}
		labelFruits.text = "0";
		this.livesPanel.setLivesQuantity (this.rabitLives);
	}
	Vector3 startingPoint;

	public void setRebitStaringPoint(Vector3 pos){
		startingPoint = pos;
	}

	public void onRabitDeath(HeroRabit rabit){
		this.rabitLives -= 1;
		this.livesPanel.setLivesQuantity (this.rabitLives);
		if (this.rabitLives == 0) {
			SceneManager.LoadScene ("Choose level");
		} else {
			rabit.transform.position = this.startingPoint;
		}

	}

	public void addCoins(int n){
		coins += n;
		string res="000";
		if (labelCoins != null) {
			res+=coins.ToString ();
			res.Substring (res.Length-4,4);
			labelCoins.text = res;
		}
	}

	public void addFruit(int n){
		fruits += n;
		labelFruits.text = fruits.ToString();
	}

	public void addCrystal(CrystalColor color){
		crystalPanel.addCrystal (color);
	}
}