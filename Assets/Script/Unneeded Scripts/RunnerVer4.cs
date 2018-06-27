using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunnerVer4 : MonoBehaviour {
	
	public float pSpeed; 
	public float playerSpeed = 6;
	public float jumpSpeed = 10.0f;
	public float gravity = 20.0f; 
	
	
	public float playerPos = 2;
	public float barPos = 0;
	public Transform myTransform;
	
	public static float distanceTraveled;
	private float height;
	private bool left = false;
	private bool right = false;
	
	
	private Vector3 moveDirection = Vector3.zero;
	private float vertVel = 0;
	
	public static int score;
	private int distance;
	
	void Awake() {
		myTransform = transform;
	}
	
	// Use this for initialization
	void Start () {
		pSpeed = playerSpeed;
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();
		myTransform.Translate(playerSpeed * Time.deltaTime, 0f, 0f);
		distanceTraveled = transform.localPosition.x;
		height = transform.localPosition.y;
		distance += (int)(playerSpeed * 0.10f);
		score = distance/4;
		
		if (Input.GetKeyUp(KeyCode.A)){
			if (playerPos == 2 || playerPos ==3){
				if (controller.isGrounded)
					left = true;
			}
		}
		if (Input.GetKeyUp(KeyCode.D)) {
			if (playerPos == 2 || playerPos == 1){
				if (controller.isGrounded)
					right = true;
			}
		}
		
		if (controller.isGrounded) {
			if (Input.GetButton("Jump")) {
				vertVel = jumpSpeed;
				pSpeed = pSpeed/3;
			}
			pSpeed = playerSpeed;
		}
		vertVel -= gravity * Time.deltaTime;
		moveDirection.y = vertVel;
		controller.Move(moveDirection * Time.deltaTime);
		
		
		if (playerPos == 2 && left)
			PlayerMove(1,0,3,.5f);
		if (playerPos == 3 && left)
			PlayerMove(1,-3,0,.5f);
		
		if (playerPos == 2 && right)
			PlayerMove(2,0,-3,-.5f);
		if (playerPos == 1 && right)
			PlayerMove(2,3,0,-.5f);
	}
	
	void PlayerMove(int LeftRight, float startPos, float endPos, float move){
		if (LeftRight == 1){ // Left is 1, Right is 2
			if (myTransform.position.z >= startPos && myTransform.position.z <endPos){
				myTransform.Translate(0f,0f,move);
				vertVel = jumpSpeed;
				//print("hello");
			}else if (myTransform.position.z >= endPos){
				myTransform.position = new Vector3(distanceTraveled, height, endPos);
				if (left){
					left = false;
					playerPos -= 1;
				}
			}
		}
		if (LeftRight == 2){
			if (myTransform.position.z <= startPos && myTransform.position.z >endPos){
				myTransform.Translate(0f,0f,move);
				vertVel = jumpSpeed;
				//print("hello");
			}else if (myTransform.position.z <= endPos){
				myTransform.position = new Vector3(distanceTraveled, height, endPos);
				if (right){
					right = false;
					playerPos += 1;
				}
			}
		}
	}
	
	
			
}
