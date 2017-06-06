using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door1 : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider) {
		SceneManager.LoadScene ("Level1");
	}

}
