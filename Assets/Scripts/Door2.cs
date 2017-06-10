using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door2 : MonoBehaviour {
	public SpriteRenderer locked;
	public SpriteRenderer check;
	public Sprite fruitSprite;
	public Sprite crystalSprite;
	public SpriteRenderer fruitRender;
	public SpriteRenderer crystalRender;

	void Start(){
		if (LevelController.isLevel1Complated)
			locked.enabled = false;
		if (!LevelController.isLevel2Complated)
			check.enabled = false;
		if (LevelController.isLevel2CrysralsCollected)
			crystalRender.sprite = crystalSprite;
		if (LevelController.isLevel2FruitCollected) {
			fruitRender.sprite = fruitSprite;	
		}

	}
	void OnTriggerEnter2D(Collider2D collider) {
		if(LevelController.isLevel1Complated)
		SceneManager.LoadScene ("Level2");
	}

}