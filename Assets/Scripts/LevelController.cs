using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelController : MonoBehaviour {
	public bool isChoseLevel;
	public static bool isLevel1FruitCollected;
	public static bool isLevel1CrysralsCollected;
	public static bool isLevel2FruitCollected;
	public static bool isLevel2CrysralsCollected;
	public static bool isLevel1Complated;
	public static bool isLevel2Complated;
	public static int collectedCoins;
	public AudioClip music = null;
	AudioSource musicSource = null;
	public GameObject losePanelPrefab;
	public GameObject winPanelPrefab;
	public GameObject settingsPrefab;
	public MyButton pause;
	public LivesPanel livesPanel;
	public CrystalPanel crystalPanel;
	int rabitLives=3;
	public static LevelController current;
	public UILabel labelCoins;
	public UILabel labelFruits;
	public UILabel allCoins;
	public int coins = 0;
	public int fruits = 0;
	public int crystals = 0;


	void Awake() {
		current = this;
		int Level1Complated = PlayerPrefs.GetInt ("isLevel1Complated", 0);
		if (Level1Complated == 1)
			isLevel1Complated = true;
		else
			isLevel1Complated = false;
		int Level2Complated = PlayerPrefs.GetInt ("isLevel2Complated", 0);
		if (Level2Complated == 1)
			isLevel2Complated = true;
		else
			isLevel2Complated = false;
		int Level1Crystals = PlayerPrefs.GetInt ("isLevel1CrysralsCollected", 0);
		if (Level1Crystals == 1)
			isLevel1CrysralsCollected = true;
		else
			isLevel1CrysralsCollected = false;

		int Level1Fruit = PlayerPrefs.GetInt ("isLevel1FruitCollected", 0);
		if (Level1Fruit == 1)
			isLevel1FruitCollected = true;
		else
			isLevel1FruitCollected = false;

		int Level2Crystals = PlayerPrefs.GetInt ("isLevel2CrysralsCollected", 0);
		if (Level2Crystals == 1)
			isLevel2CrysralsCollected = true;
		else
			isLevel2CrysralsCollected = false;

		int Level2Fruit = PlayerPrefs.GetInt ("isLevel2FruitCollected", 0);
		if (Level2Fruit == 1)
			isLevel2FruitCollected = true;
		else
			isLevel2FruitCollected = false;

		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.clip = music;
		musicSource.loop = true;
		musicSource.Play ();
	}

	void Start(){
		
			collectedCoins = PlayerPrefs.GetInt ("collectedCoins",0);
		if (isChoseLevel) {
			string res="000";
				res+=collectedCoins.ToString ();
				res.Substring (res.Length-4,4);
			allCoins.text = res;
		}

		if(pause!=null)
		this.pause.signalOnClick.AddListener (this.showSettings);

		if (labelCoins != null) {
			labelCoins.text="0000";
		}
		if(labelFruits!=null)
		labelFruits.text = "0";
		if(livesPanel!=null)
		this.livesPanel.setLivesQuantity (this.rabitLives);
	}
	Vector3 startingPoint;

	public void setRebitStaringPoint(Vector3 pos){
		startingPoint = pos;
	}

	void showSettings() {
		//Знайти батьківський елемент
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, settingsPrefab);
		//Отримати доступ до компоненту (щоб передати параметри)
		SettingsPopUp popup = obj.GetComponent<SettingsPopUp>();
		Time.timeScale = 0;
	} 

	public void onRabitDeath(HeroRabit rabit){
		this.rabitLives -= 1;
		if(livesPanel!=null)
		this.livesPanel.setLivesQuantity (this.rabitLives);
		if (this.rabitLives == 0) {
			//Знайти батьківський елемент
			GameObject parent = UICamera.first.transform.parent.gameObject;
			//Створити Prefab
			GameObject obj = NGUITools.AddChild (parent, losePanelPrefab);
			//Отримати доступ до компоненту (щоб передати параметри)

			LosePanel lose = obj.GetComponent<LosePanel>();
			Time.timeScale = 0;
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

	public void addLife(int n){
		if (rabitLives < 3) {
			rabitLives += n;
			this.livesPanel.setLivesQuantity (this.rabitLives);
		}
	}

	public void addCrystal(CrystalColor color){
		crystalPanel.addCrystal (color);
	}

	public void createWinPanel(){
		isLevel1Complated = true;
		PlayerPrefs.SetInt ("isLevel1Complated", 1);
		PlayerPrefs.Save ();
		//Знайти батьківський елемент
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, winPanelPrefab);
		//Отримати доступ до компоненту (щоб передати параметри)
		WinPanel win = obj.GetComponent<WinPanel>();
		win.setCoins (this.coins);
		win.setFruits (this.fruits,1);
		win.setCrystal (this.crystalPanel.getObtainedCrystal(),1);
		Time.timeScale = 0;
		collectedCoins += coins;
		PlayerPrefs.SetInt ("collectedCoins",collectedCoins);

		if (isLevel1CrysralsCollected)
			PlayerPrefs.SetInt ("isLevel1CrysralsCollected", 1);
		PlayerPrefs.Save ();
	}

	public void createWinPanel2(){
		//Знайти батьківський елемент
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, winPanelPrefab);
		//Отримати доступ до компоненту (щоб передати параметри)
		WinPanel win2 = obj.GetComponent<WinPanel>();
		win2.setCoins (this.coins);
		win2.setFruits (this.fruits,2);
		win2.setCrystal (this.crystalPanel.getObtainedCrystal(),2);
		Time.timeScale = 0;
		collectedCoins += coins;
		PlayerPrefs.SetInt ("collectedCoins",collectedCoins);
		isLevel2Complated = true;
		PlayerPrefs.SetInt ("isLevel2Complated", 1);
		if (isLevel2CrysralsCollected)
			PlayerPrefs.SetInt ("isLevel2CrysralsCollected", 1);
		PlayerPrefs.Save ();
	}

	public void setMusicOff(){
		
		musicSource.Pause ();
	}

	public void setMusicOn(){
		
		musicSource.Play ();
	}

	public void saveStatLevel1(){
		collectedCoins += coins;
		PlayerPrefs.SetInt ("collectedCoins",collectedCoins);
		isLevel1Complated = true;
		PlayerPrefs.SetInt ("isLevel1Complated", 1);
		if (isLevel1CrysralsCollected)
			PlayerPrefs.SetInt ("isLevel2CrysralsCollected", 1);
		PlayerPrefs.Save ();
	}


	public void saveStatLevel2(){
		collectedCoins += coins;
		PlayerPrefs.SetInt ("collectedCoins",collectedCoins);
		isLevel2Complated = true;
		PlayerPrefs.SetInt ("isLevel2Complated", 1);
		if (isLevel2CrysralsCollected)
			PlayerPrefs.SetInt ("isLevel2CrysralsCollected", 1);
		PlayerPrefs.Save ();
	}
}