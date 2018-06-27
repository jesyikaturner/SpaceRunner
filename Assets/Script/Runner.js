var coinCollect :AudioSource;
var jump :AudioSource;
var bgMusic :AudioSource;

var speed :float = 5.0f;
var jumpSpeed :float = 10.0f;
var gravity :float = 20.0f; 
	
private var moveDirection = Vector3.zero;
private var vertVel = 0.0;
private var left :boolean = false;
private var right :boolean = false;
private var playerPos :int = 2;

public static var isAlive :boolean = true;
public static var startGame :boolean = false;

private var distScore :float;
private var distance :int;
public static var score :int;
public static var distanceTravelled;
private var coinscore :int;


//private var isHit :boolean = false; //going to implement a two life system
//private var lives :int;
public static var isMuted :boolean = false; //mutes the sound/ bgmusic
public static var bgMusicPause = false;


public static var inverse = false;


function Start(){
    var aSources = GetComponents(AudioSource);
    coinCollect = aSources[0];
    jump = aSources[1];
    bgMusic = aSources[2];

	bgMusicPause = false;
	score = 0;
	coinscore = 0;
	inverse = false;
}

function OnTriggerEnter(trigger){
	if (trigger.gameObject.tag == "Coins"){
		coinscore += 200;
		if (!isMuted){
			coinCollect.Play();
		} else {
			return;
		}
	}
	if (trigger.gameObject.tag == "SpikeTrap"){
		//isHit = true;
		isAlive = false;
	}
	
	if (trigger.gameObject.tag == "SpeedUp"){
		coinscore += 400;
		speed = speed + 3;
		//speedUp = true;
	}
	
	if (trigger.gameObject.tag == "SpeedDown"){
		coinscore -= 200;
		speed = speed - 2;
		//speedDown = true;
	}
	
	if (trigger.gameObject.tag == "Inverse"){
		if (!inverse){
			inverse = true;
		} else {
			inverse = false;
		}
	}

}

  
function Update (){
	
	if (bgMusicPause || isMuted){
		bgMusic.mute = true;
	} else {
		bgMusic.mute = false;
	}


	distScore = parseInt(Mathf.Round(distance/16));
	distanceTravelled = transform.localPosition.x;
	
	var controller :CharacterController  = GetComponent("CharacterController");
	if (startGame){
		if(isAlive){
			distance += speed * 0.5f;
			score = distScore + coinscore;
			/*
			if (speedUp){
				speed = speed +2;
			}*/
			
/*
			moveDirection.x =speed;
			if (controller.isGrounded){
				if (Input.GetButton("Jump")) {	
					if (!isMuted){
						jump.Play();
					}		
					vertVel = jumpSpeed;
				}
				if (inverse){
					if (Input.GetKeyUp(KeyCode.A)) {
						if (playerPos == 2 || playerPos == 3)
							if (!isMuted){
								jump.Play();
							}	
						right = true;
					}
					if (Input.GetKeyUp(KeyCode.D)) {
						if (playerPos == 2 || playerPos == 1)
							if (!isMuted){
								jump.Play();
							}		
						left = true;
					}
				} else if (!inverse){
					if (Input.GetKeyUp(KeyCode.A)) {
						if (playerPos == 2 || playerPos == 3)
							if (!isMuted){
								jump.Play();
							}	
						left = true;
					}
					if (Input.GetKeyUp(KeyCode.D)) {
						if (playerPos == 2 || playerPos == 1)
							if (!isMuted){
								jump.Play();
							}		
						right = true;
					}			
				}
			}*/	

			/*if (playerPos == 2 && left)
				PlayerMove(1,0,3,15f);
			if (playerPos == 3 && left)
				PlayerMove(1,-3,0,15f);
		
			if (playerPos == 2 && right)
				PlayerMove(2,0,-3,-15f);
			if (playerPos == 1 && right)
				PlayerMove(2,3,0,-15f);
		*/
		
			//vertVel -= gravity * Time.deltaTime;
			//moveDirection.y = vertVel;
			//controller.Move(moveDirection * Time.deltaTime);
		}
		
		
			if (transform.position.y < -5) {
				isAlive = false; //stops the gameplay
			}
				
			if (!isAlive){
				bgMusicPause = true;
				Runner.startGame = false;
				GameGUI.GameOver();
			}
		}

	}
	/*
function PlayerMove(LeftRight : int, startPos : float, endPos : float, move : float){
		if (LeftRight == 1){ // Left is 1, Right is 2
			if (transform.position.z >= startPos && transform.position.z <endPos){
				moveDirection.z = move;
				moveDirection.x = 1;
				vertVel = jumpSpeed*.9f;
				//vertVel = jumpSpeed*0.7f;
			}else if (transform.position.z >= endPos){
				moveDirection.z = 0;
				transform.position.z = endPos;
				left = false;
				playerPos -= 1;
			}
		}
		
		if (LeftRight == 2){
			if (transform.position.z <= startPos && transform.position.z >endPos){
				moveDirection.z = move;
				moveDirection.x = 1;
				vertVel = jumpSpeed*.9f;
				//vertVel = jumpSpeed*0.7f;
			}else if (transform.position.z <= endPos){
				moveDirection.z = 0;
				transform.position.z = endPos;
				right = false;
				playerPos += 1;
			}
		}
	}*/
