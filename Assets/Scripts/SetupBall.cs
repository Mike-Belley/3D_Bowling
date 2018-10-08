
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupBall : MonoBehaviour {

	public float bowlForce;
	public Transform Ball;
	public Vector3 offset;
	public Rigidbody rb;
	private int stage = 1;
	public int angle = 0;
	public int angleChange = 1;
	public GameObject line;
	public GameObject UIBall;
	public bool UIballTF = true;
	public Transform lineT;
	Vector3 scale;
	public int pinsDown;
	public PinReset pin;
	public PinReset pin1;
	public PinReset pin2;
	public PinReset pin3;
	public PinReset pin4;
	public PinReset pin5;
	public PinReset pin6;
	public PinReset pin7;
	public PinReset pin8;
	public PinReset pin9;
	public Vector3 changeY;
	public GameObject strike;
	public GameObject spare;
	public bool strikeTF = false;
	public bool spareTF = false;
	public int totalPins = 0;
	public int pinsDownTemp = 0;
	public bool countPinsTF = true;
	public Text balls;
	public Text pins;
	public Text strikes;
	public Text spares;
	public int ballsThrown = 0;
	public int strikesTotal = 0;
	public int sparesTotal = 0;
	public bool strikesTotalTF = true;
	public bool sparesTotalTF = true;


	// Update is called once per frame
	void Update () {
		UIBall.SetActive (UIballTF);
		strike.SetActive (strikeTF);
		spare.SetActive (spareTF);
		balls.text = ("Balls: " + ballsThrown);
		pins.text = ("Pins: " + totalPins);
		strikes.text = ("Strikes: " + strikesTotal);
		spares.text = ("Spares: " + sparesTotal);
		//sees how many pins are down
		pinsDown = pin.pindown + pin1.pindown + pin2.pindown + pin3.pindown + pin4.pindown + pin5.pindown +
			pin6.pindown + pin7.pindown + pin8.pindown + pin9.pindown;
		//if ball is below -300
		if (transform.position.y < -310) {
			//if all pins are down
			pinsDownTemp = pinsDown;
			if (countPinsTF == true) {
				if(pinsDown == 10 || UIballTF == false)
				countPins ();
			}
			if (pinsDown == 10) {
				//if the UIball is on then trike is true
				if (UIballTF == true) {
					strikeTF = true;
					if (strikesTotalTF == true) {
						strikesTotal++;
						strikesTotalTF = false;
					}
				//if not then spare is true
				} else {
					spareTF = true;
					if (sparesTotalTF == true) {
						sparesTotal++;
						sparesTotalTF = false;
					}
				}
			}
			//if ball is below -400 resets ball
			if (transform.position.y < -400) {
				//if UIball is off then turns it on
				if (UIballTF == false) {
					UIballTF = true;
				//if it is on then turn it off and strike is false
				} else {
					if (strikeTF == false) {
						UIballTF = false;
					}
				}
				resetBall ();
			}
		}
		//if stage equals 1 then you can move and change to stage 2 and 3
		if (stage == 1) {
			if(Input.GetKey(KeyCode.RightArrow)){
				transform.position = Ball.position + offset;
			}
			if(Input.GetKey(KeyCode.LeftArrow)){
				transform.position = Ball.position - offset;
			}
			if(Input.GetKey("2")){
				stage = 2;
			}
			if(Input.GetKey("3")){
				stage = 3;
			}
		}
		//if stage is 2 then you can rotate ball and go to stage 1 and 3
		if (stage == 2) {
			if(Input.GetKey(KeyCode.RightArrow)){
				angle++;
				transform.Rotate (0,angleChange,0);
			}
			if(Input.GetKey(KeyCode.LeftArrow)){
				angle--;
				transform.Rotate (0,-angleChange,0);
			}
			if(Input.GetKey("1")){
				stage = 1;
			}
			if(Input.GetKey("3")){
				stage = 3;
			}
		}
		//if stage equals 3 you can adjust power and throw the ball and change to stage 1 and 2
		//when ball is thrown sets to stage 4 and gets rid of the line
		if (stage == 3) {
			if(Input.GetKey(KeyCode.UpArrow)){
				bowlForce = bowlForce + 100;
				scale = lineT.localScale;
				scale.z = (bowlForce / 500) + 3;
				lineT.localScale = scale;
				if (bowlForce > 3000) {
					bowlForce = 3000;
				}
			}
			if(Input.GetKey(KeyCode.DownArrow)){
				bowlForce = bowlForce - 50;
				scale = lineT.localScale;
				scale.z = (bowlForce / 500) + 3;
				lineT.localScale = scale;
				if (bowlForce < 500) {
					bowlForce = 500;
				}
			}
			if(Input.GetKey("1")){
				stage = 1;
			}
			if(Input.GetKey("2")){
				stage = 2;
			}
			if(Input.GetKey(KeyCode.Space)){
				rb.AddForce (Mathf.Sin(angle*Mathf.Deg2Rad)*bowlForce,0,Mathf.Cos(angle*Mathf.Deg2Rad)*bowlForce);
				stage++;
				line.SetActive (false);
				ballsThrown++; 
			}
		}
		//if ball is moving backwards puts the ball beneath the stage for it to fall
		if (rb.velocity.z < 0 && Ball.position.z > -5 && Ball.position.z < 45 && Ball.position.y > -5) {
			transform.position = Ball.position + changeY;
			rb.AddForce (0,0,1000);
		}
	}
	//function that resets the ball to original position, sets stage equal to 1, resets the angle,
	//adds the line, sets striek and spare to false
	void resetBall () {
		rb.velocity = new Vector3 (0, 0, 0);
		rb.angularVelocity = new Vector3 (0,0,0);
		transform.position = new Vector3 (0,0,0);
		transform.rotation = new Quaternion (0,0,0,0);
		angle = 0;
		stage = 1;
		line.SetActive (true);
		strikeTF = false;
		spareTF = false;
		countPinsTF = true;
		strikesTotalTF = true;
		sparesTotalTF = true;
	}
	void countPins () {
		totalPins = totalPins + pinsDownTemp;
		pinsDownTemp = 0;
		countPinsTF = false;
	}
}
