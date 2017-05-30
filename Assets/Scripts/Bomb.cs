public class Bomb : Collectable {
	protected override void OnRabitHit (HeroRabit rabit){
		rabit.removeHealth (1);
		this.CollectedHide ();
	}
}