public class Mushroom : Collectable {
	protected override void OnRabitHit (HeroRabit rabit){
		rabit.addHealth (1);
		rabit.makeBigger ();
		this.CollectedHide ();
	}
}