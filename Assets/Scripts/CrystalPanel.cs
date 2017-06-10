using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPanel : MonoBehaviour {



	public List <UI2DSprite> crystalPlace;

	public Sprite crystalNotGet;
	public List<Sprite> crystalColors;

	Dictionary<CrystalColor,bool> obtainedCrystal= new Dictionary<CrystalColor, bool>();


	public Dictionary<CrystalColor,bool> getObtainedCrystal(){
		return obtainedCrystal;
	}

	// Use this for initialization
	void Start () {
		this.refreshCrystals ();
	}
	
	public void addCrystal(CrystalColor color){
		obtainedCrystal [color] = true;
		this.refreshCrystals ();
	}

	void updateCrystalColor(CrystalColor color){
		int crystal_id = (int)color;
		if (obtainedCrystal.ContainsKey (color)) {
			crystalPlace [crystal_id].sprite2D =crystalColors [crystal_id];

		} else {
			crystalPlace [crystal_id].sprite2D = crystalNotGet;
		}
	}

	void refreshCrystals(){
		updateCrystalColor (CrystalColor.Blue);
		updateCrystalColor (CrystalColor.Green);
		updateCrystalColor (CrystalColor.Red);

	}

}
