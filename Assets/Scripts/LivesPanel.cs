using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesPanel: MonoBehaviour {
	public List <UI2DSprite> hearts;

	public Sprite spriteLiveFull;
	public Sprite spriteLiveEmpty;

	void Start () {
		
	}

	public void setLivesQuantity(int lives){
		for (int i = 0; i < 3; i++) {
			if (i < lives) {
				hearts [i].sprite2D = this.spriteLiveFull;
			} else {
				hearts [i].sprite2D = this.spriteLiveEmpty;
			}
		}
	}
	

}
