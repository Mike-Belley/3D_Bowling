using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle : MonoBehaviour {

	public int angle = 1;
	public Vector3 RotationDriveMode;
	public int stage = 1;
	public Vector3 offset;
	public Transform Direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("1")) {
			stage = 1;
		}
		if (Input.GetKey ("2")) {
			stage = 2;
		}
		if (Input.GetKey ("3")) {
			stage = 3;
		}
		if(stage == 1){
			if(Input.GetKey(KeyCode.RightArrow)){
				transform.position = Direction.position + offset;
			}
			if(Input.GetKey(KeyCode.LeftArrow)){
				transform.position = Direction.position - offset;
			}
		}
		if(stage == 2){
			if(Input.GetKey(KeyCode.RightArrow)){
				transform.Rotate (0,angle,0);
			}
			if(Input.GetKey(KeyCode.LeftArrow)){
				transform.Rotate (0,-angle,0);
			}
		}
	}
}
