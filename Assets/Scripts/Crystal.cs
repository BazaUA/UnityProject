public class Crystal : Collectable {
	public CrystalColor crystalColor;
	protected override void OnRabitHit (HeroRabit rabit){
		LevelController.current.addCrystal (crystalColor);
		this.CollectedHide ();
	}
}