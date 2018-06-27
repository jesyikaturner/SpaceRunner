using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunnerVer3: MonoBehaviour {
	public static float distanceTraveled;
	public static float height;
	public float playerSpeed = 10.0f;
	public float jumpSpeed = 10.0f;
	public float gravity = 20.0f; 
	
	public float score = 0;
	
	private Vector3 moveDirection = Vector3.zero;
	private float vertVel = 0;
	private Transform myTransform;
	
	public int barPos = 0; //0 is default : 1 is left : 2 is right
	public int playerPos = 2; //2 is the default : 2 is the middle set of platforms
	
	void Awake (){
		myTransform = transform;
	}
	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	//GUI
	void OnGUI(){
		GUI.Label(new Rect(10, 30, 100, 60), "Score: " +(int)score);
	}
	
	// Update is called once per frame
	void Update () {
		//Score
		score = score + 0.30f;
		
		
		//Movement
		CharacterController controller = GetComponent<CharacterController>();
		transform.Translate(playerSpeed * Time.deltaTime, 0f, 0f);
		distanceTraveled = transform.localPosition.x;
		height = transform.localPosition.y;
		
		if (Input.GetKey(KeyCode.A)) {
			if (controller.isGrounded){
				barPos = 1;
				//PlayerMove();
			}
		}
		
		if (Input.GetKey (KeyCode.D)) {
			if (controller.isGrounded){
				barPos = 2;
			}
		}
		///////	
		/*
		if (Input.GetKey(KeyCode.W)) {
			transform.Translate(Vector3.right * Time.deltaTime * playerSpeed);
		}
		if (Input.GetKeyUp(KeyCode.A)) {
			transform.Rotate(0,270,0);
		}
		if (Input.GetKeyUp(KeyCode.D)) {
			transform.Rotate(0,90,0);
		}*/
		if (controller.isGrounded) {
			if (Input.GetButton("Jump")) {
				vertVel = jumpSpeed;	
			}
		}
		vertVel -= gravity * Time.deltaTime;
		moveDirection.y = vertVel;
		controller.Move(moveDirection * Time.deltaTime);
			
		
		// If the player is in the middle
		if (barPos == 1) {
			if (playerPos == 2){
				if (myTransform.position.z>=0 && myTransform.position.z <3){
					myTransform.Translate(0f,0f,0.5f);
					vertVel = jumpSpeed;
				} else if (myTransform.position.z ==3){						
					myTransform.position = new Vector3(distanceTraveled,height,3);
					barPos = 0;
					playerPos = 1;
				}
			}
		}
/*
			//If the player is on the right
			if (playerPos == 3) {
				if (myTransform.position.z <0  && myTransform.position.z >-3){
					myTransform.Translate(0f,0f,0.5f);
					vertVel = jumpSpeed;
				}else if (myTransform.position.z == 0) {
					myTransform.position = new Vector3(distanceTraveled,height,0);
					barPos = 0;
					playerPos = 2;
				}
			}
*/
		
		if (barPos == 2) {
			//If the player is on the left
			if (playerPos == 1) {
				if (myTransform.position.z <=3 && myTransform.position.z >0){
					myTransform.Translate(0f,0f,-0.5f);
					vertVel = jumpSpeed;				
				}else if (myTransform.position.z <= 0){
					myTransform.position = new Vector3(distanceTraveled,height,0);
					barPos = 0;
					playerPos = 2;
				}
			}
/*
			//if the player is in the middle
			if (playerPos == 2) {
				if (myTransform.position.z >=0 && myTransform.position.z >-3){
					myTransform.Translate(0f,0f,-0.5f);
					vertVel = jumpSpeed;
				}else if (myTransform.position.z ==-3){
					myTransform.position = new Vector3(distanceTraveled,height,-3);
					barPos = 0;
					playerPos = 3;
				}
				
			}
*/
		
		}
	
	}
			
	
}
