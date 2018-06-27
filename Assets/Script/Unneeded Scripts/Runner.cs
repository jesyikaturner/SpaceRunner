using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls{
	public KeyCode Jump = KeyCode.Space;
	public KeyCode Left = KeyCode.A;
	public KeyCode Right = KeyCode.D;	
}

public class Runner : MonoBehaviour {
	
	
	
	
	public bool onGround; //whether the player is touching a platform
	
	//private Transform myTransform;
	
	public float playerSpeed; 
	public float jumpSpeed;
	public float gravity = 20.0f;
	
	private float vertVel = 0;
	
	public static float yPos;
	public static float zPos; //store the z position of the player
	public static float distanceTraveled;
	
	//public Vector3 forwardJumpVelocity;
	public Vector3 JumpVelocity = new Vector3(0,5,zPos);
	public Vector3 leftJumpVelocity = new Vector3(0,5,zPos);
	public Vector3 rightJumpVelocity = new Vector3(0,5,zPos);
	
	public bool left = false;
	public bool right = false;
	public bool jumpAvail = false;
	public int playerPos = 2;
	
	public Rigidbody curPos;
	
	public Vector3 moveDirection;
	
	
	void Awake(){
		//curPos = GetComponent<Rigidbody>();
		//rigidbody.position = myTransform;
		//myTransform = transform;
	}
	
	void Start () {
		zPos = 0;
	}
	
	void Update () {
		//distanceTraveled = transform.localPosition.x;
		//myTransform.position = new Vector3(distanceTraveled, myTransform.position.y, zPos);
		//rigidbody.position = new Vector3(myTransform.position.x, myTransform.position.y, zPos);
		//myTransform.Translate(playerSpeed*Time.deltaTime,0f,0f);
		
		if (onGround){
			if (Input.GetKeyDown(KeyCode.W)){
				print("W");
				GetComponent<Rigidbody>().AddForce(Vector3.up*jumpSpeed);
			}
				
			if (Input.GetKeyUp(KeyCode.A))
				if (playerPos == 2|| playerPos == 3){
					left = true;
					jumpAvail = true;
				}
			
			if(Input.GetKeyUp(KeyCode.D)){
				if (zPos == 0 || zPos == 3){
					right = true;
					jumpAvail = true;
				}
			}
			if (Input.GetKey(KeyCode.Space))
				//rigidbody.AddForce(JumpVelocity, ForceMode.VelocityChange);
				vertVel = jumpSpeed;
		}
		
		
		if (left && playerPos == 2){
			PlayerMove(1,0,3,.5f);
		}
		if (left && playerPos == 3){
			PlayerMove(1,-3,0,.5f);
		}
		if (right && playerPos == 1){
			PlayerMove(2,3,0,.5f);
		}
		if (right && playerPos == 2){
			PlayerMove(2,0,-3,.5f);
		}
		
		
		if (left && playerPos == 2 && jumpAvail || left && playerPos == 3 && jumpAvail){
			GetComponent<Rigidbody>().AddForce(leftJumpVelocity, ForceMode.VelocityChange);
			jumpAvail = false;
		}
		
		if (right && playerPos == 2 && jumpAvail || right && playerPos == 1 && jumpAvail){
			GetComponent<Rigidbody>().AddForce(rightJumpVelocity, ForceMode.VelocityChange);
			jumpAvail = false;
		}
		
		vertVel -= gravity * Time.deltaTime;
		moveVec.y = vertVel;
		
		moveVec.x = playerSpeed * Time.deltaTime;
		GetComponent<Rigidbody>().MovePosition(transform.position + moveVec);
		
	}
	
	public Vector3 moveVec = Vector3.zero;
	public float speed = 5f;

	
	
	void PlayerMove(int LeftRight, float startPos, float endPos, float move){
		if (LeftRight == 1){
			if (zPos >= startPos && zPos < endPos)
				zPos +=move;
			if (zPos > endPos)
				zPos = endPos;
			if (zPos == endPos){
				left = false;
				playerPos -=1;
			}
		}
		if (LeftRight == 2){
			if (zPos <= startPos && zPos > endPos)
				zPos -=move;
			if (zPos < endPos)
				zPos = endPos;
			if (zPos == endPos){
				right = false;
				playerPos +=1;
			}
		}
		
				
	}
	
	
	void OnCollisionEnter(){
		onGround = true;
		gravity = 0;
	}
	void OnCollisionExit(){
		onGround = false;
	}
}
