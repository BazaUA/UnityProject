using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public Vector3 MoveBy;
	Vector3 pointA;
	Vector3 pointB;
	public float time_to_wait;
	float delay;
	public Vector3 speedAndDirection;
	bool going_to_B = true;

	void Start(){
		delay = time_to_wait;
		this.pointA = this.transform.position;
		this.pointB = this.pointA + MoveBy;
	}


	void Update(){
	
	}


	void FixedUpdate(){
		time_to_wait -= Time.deltaTime;
		if (time_to_wait <= 0){
			if (going_to_B){
				if (isArrived(this.transform.position, this.pointB) == false){
					this.transform.Translate(speedAndDirection * Time.deltaTime);
				}
				else{
					going_to_B = false;
					time_to_wait = delay;
				}
			}
			else{
				if (isArrived(this.pointA, this.transform.position) == false){
					this.transform.Translate(-speedAndDirection * Time.deltaTime);
				}
				else{
					going_to_B = true;
					time_to_wait = delay;
				}
			}
		}
	}

	bool isArrived(Vector3 pos, Vector3 target){
		pos.z = 0;
		target.z = 0;
		return Vector3.Distance(pos, target)<=0.2f;
	}
}
