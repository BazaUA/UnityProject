using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Collectable {

	protected override void OnRabitHit (HeroRabit rabit){
		LevelController.current.addLife (1);
		this.CollectedHide ();

	}
}