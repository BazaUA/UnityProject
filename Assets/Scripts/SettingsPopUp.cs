using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopUp : MonoBehaviour {
	public MyButton closeButton;
	public MyButton blackBackground;
	public MyButton soundButton;
	public MyButton musicButton;
	public Sprite buttonSoundOff;
	public Sprite buttonSoundOn;
	public Sprite buttonMusicOff;
	public Sprite buttonMusicOn;

	// Use this for initialization
	void Start () {
		if(!SoundController.current.music)
			musicButton.GetComponent<UIButton> ().normalSprite2D=buttonMusicOff;
		if(!SoundController.current.sound)
			soundButton.GetComponent<UIButton> ().normalSprite2D=buttonSoundOff;
		closeButton.signalOnClick.AddListener (this.close);
		blackBackground.signalOnClick.AddListener (this.close);
		soundButton.signalOnClick.AddListener (this.changeSound);
		musicButton.signalOnClick.AddListener (this.changeMusic);
	}
	
	// Update is called once per frame
	void close () {
		Destroy (this.gameObject);
		Time.timeScale = 1;
	}

	void changeMusic(){			
		if (musicButton.GetComponent<UIButton> ().normalSprite2D.name.Equals("music-on"))
			musicButton.GetComponent<UIButton> ().normalSprite2D=buttonMusicOff;
		else
			musicButton.GetComponent<UIButton> ().normalSprite2D=buttonMusicOn;
		SoundController.current.changeMusic ();
	}

	void changeSound(){
		if (soundButton.GetComponent<UIButton> ().normalSprite2D.name.Equals("sound-on"))
			soundButton.GetComponent<UIButton> ().normalSprite2D=buttonSoundOff;
		else
			soundButton.GetComponent<UIButton> ().normalSprite2D=buttonSoundOn;
		SoundController.current.changeSound ();
	}

}
