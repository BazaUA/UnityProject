﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
	public static SoundController current;
	public HeroRabit rabit;
	public List<Orc1> orcs1;
	public List<Orc2> orcs2;

	public bool music=true;
	public bool sound=true;

	void Awake() {
		current = this;
	}

	void Start () {
		int music = PlayerPrefs.GetInt ("music",-1);
		int sound = PlayerPrefs.GetInt ("sound",-1);
		if (music == 1||music==-1) {
			this.music = true;
		}else if(music==0){
			this.music = false;
		}

		if (sound == 1||sound==-1) {
			this.sound = true;
		}else if(sound==0){
			this.sound = false;
		}
		changeMusic ();
		changeMusic ();
		changeSound ();
		changeSound ();
	}

	public void changeMusic(){
		if (music) {
			music = false;
			PlayerPrefs.SetInt ("music",0);
			PlayerPrefs.Save ();
			LevelController.current.setMusicOff ();
		} else {
			music = true;
			PlayerPrefs.SetInt ("music",1);
			PlayerPrefs.Save ();
			LevelController.current.setMusicOn ();
		}
		
	}

	public void changeSound(){
		if (sound) {
			sound = false;
			PlayerPrefs.SetInt ("sound",0);
			PlayerPrefs.Save ();
			HeroRabit.lastRabit.setSoundOff ();
			for (int i = 0; i < orcs1.Count; i++) {
				if (orcs1[i] != null)
					orcs1[i].setSoundOff ();
			}
			for (int i = 0; i < orcs2.Count; i++) {
				if (orcs2[i] != null)
					orcs2[i].setSoundOff ();
			}
			WinPanel.isSound = false;
			LosePanel.isSound = false;
		} else {
			sound = true;
			LosePanel.isSound = true;
			WinPanel.isSound = true;
			PlayerPrefs.SetInt ("sound",1);
			PlayerPrefs.Save ();
			HeroRabit.lastRabit.setSoundOn ();
			for (int i = 0; i < orcs1.Count; i++) {
				if (orcs1[i] != null)
					orcs1[i].setSoundOn ();
			}
			for (int i = 0; i < orcs2.Count; i++) {
				if (orcs2[i] != null)
					orcs2[i].setSoundOn ();
			}
		}
	}

}
