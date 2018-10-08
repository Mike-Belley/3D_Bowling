using UnityEngine;

public class PinReset : MonoBehaviour {

	public Transform ball;
	public Rigidbody rb;
	public GameObject pin;
	public bool rot;
	public int pindown;
	public SetupBall setupball;
	public Vector3 pos;
	public bool pinTF = true;
	
	// Update is called once per frame
	void Update () {
		//if it is rotated and the ball is between -300 and -350m will call ridPin
		if(ball.position.y < -300 && rot == true){
			ridPin ();
		}
		//if ball is below -375 and if the UIball is off or if there is a strike call resetPin
		if(ball.position.y < -375){
			if(setupball.UIballTF == false || setupball.strikeTF == true){
				resetPin();
			}
		}
		//if pin is rotated then sets rot equals to true
		if(pin.transform.rotation.x > .25 || pin.transform.rotation.x < -.25
			|| pin.transform.rotation.z > .25 || pin.transform.rotation.z < -.25
			|| transform.position.y < -5){
			rot = true;
		}
	}
	//function that teleports pin somewhere off screen and makes pindown equal to 1
	//so that ResetBall can detect if it is down
	void ridPin () {
		rb.velocity = new Vector3 (0, 0, 0);
		rb.angularVelocity = new Vector3 (0,0,0);
		transform.position = new Vector3(0,-5,-5);
		transform.rotation = new Quaternion (0,0,0,0);
		pindown = 1;
	}
	//resets pin to original position, sets pindown equal to 0, sets rot equal to false
	void resetPin () {
		rot = false;
		rb.velocity = new Vector3 (0, 0, 0);
		rb.angularVelocity = new Vector3 (0,0,0);
		transform.position = pos;
		transform.rotation = new Quaternion (0,0,0,0);
		pindown = 0;
	}
}

