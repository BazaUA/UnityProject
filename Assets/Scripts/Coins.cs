public class Coins : Collectable {
	protected override void OnRabitHit (HeroRabit rabit){
		LevelController.current.addCoins (1);
		this.CollectedHide ();
	}
}