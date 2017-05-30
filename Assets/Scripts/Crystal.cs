public class Crystal : Collectable {
	protected override void OnRabitHit (HeroRabit rabit){
		LevelController.current.addCrystal (1);
		this.CollectedHide ();
	}
}