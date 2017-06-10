using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndOfLevel1 : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider) {
		LevelController.current.createWinPanel ();

	}

}
