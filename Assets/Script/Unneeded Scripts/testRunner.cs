using UnityEngine;
using System.Collections;

public class testRunner : MonoBehaviour {
	
	public float playerSpeed = 10f;
	public float jumpSpeed = 15f;
	public float gravity = 20.0f;
	
	public int playerPos = 2;
	
	public bool left = false;
	public bool right = false;
	
	private float vertVel = 0;
	private Vector3 moveDirection = Vector3.zero;
	private float height;
	public float distanceTraveled;
	public float distance;
	//public static int score;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();
		//distanceTraveled = transform.localPosition.x;
		height = transform.localPosition.y;
		distance = transform.localPosition.x;
		

		if (controller.isGrounded){
			if (Input.GetButton("Jump")) {
				vertVel = jumpSpeed;
			}
			if (Input.GetKeyUp(KeyCode.A)) {
				if (playerPos == 2 || playerPos == 3)
					left = true;
			}
			if (Input.GetKeyUp(KeyCode.D)) {
				if (playerPos == 2 || playerPos == 1)
					right = true;
			}
			
		}
		
		if (playerPos == 2 && left)
			PlayerMove(1,0,3,5f);
		if (playerPos == 3 && left)
			PlayerMove(1,-3,0,5f);
		
		if (playerPos == 2 && right)
			PlayerMove(2,0,-3,-5f);
		if (playerPos == 1 && right)
			PlayerMove(2,3,0,-5f);

		
		vertVel -= gravity * Time.deltaTime;
		moveDirection.y = vertVel;
		moveDirection.x = playerSpeed;
		controller.Move(moveDirection * Time.deltaTime);
	}
	
	void PlayerMove(int LeftRight, float startPos, float endPos, float move){
		if (LeftRight == 1){ // Left is 1, Right is 2
			if (transform.position.z >= startPos && transform.position.z <endPos){
				moveDirection.z = move;
				vertVel = jumpSpeed/2;
				//print("hello");
			}else if (transform.position.z >= endPos){
				moveDirection.z = 0;
				transform.position = new Vector3(distance, height, endPos);
				if (left){
					left = false;
					playerPos -= 1;
				}
			}
		}
		
		if (LeftRight == 2){
			if (transform.position.z <= startPos && transform.position.z >endPos){
				moveDirection.z = move;
				vertVel = jumpSpeed/2;
				//print("hello");
			}else if (transform.position.z <= endPos){
				moveDirection.z = 0;
				transform.position = new Vector3(distance, height, endPos);
				if (right){
					right = false;
					playerPos += 1;
				}
			}
		}
	}
	
	
}
