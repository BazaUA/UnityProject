using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndOfLevel2 : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider) {
		LevelController.current.createWinPanel2 ();
		LevelController.current.saveStatLevel2 ();
	}

}
