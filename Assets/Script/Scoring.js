#pragma strict

var scoreText :String = "SCORE: ";
var HUDSkin : GUISkin;

var gameOverText :String;

public static var gameOver :boolean = false;
var restartGame :boolean;
var quitGame :boolean;

function Start () {
	restartGame = false;
	quitGame= false;
}

function Update () {
	if (restartGame){
		RestartGame();
	}
	if (quitGame){
		Quit();
	}
}

function OnGUI (){
	GUI.skin = HUDSkin;
	var labelStyle :GUIStyle = GUI.skin.GetStyle("highScoreFont");
	GUI.Label(new Rect(50, 10, Screen.width/2, 100), scoreText +Runner.score);

	if (gameOver){
		GUI.Box(new Rect(Screen.width/2 - 200, 50, 400, Screen.height - 100),"");
		//Debug.Log(gameOver);
		
		GUI.Label(new Rect(Screen.width/2 - 50, Screen.height/2 - 100,200,50),"High Score: " +PlayerPrefs.GetInt(Application.loadedLevelName+"_highscore"), labelStyle);
		//GUI."Label2"(new Rect(625,250,200,50), "Your Score: " +Runner.score);
		/*
		if (GUI.Button(new Rect(600,400,200, 50),"Erase Highscore")){
			PlayerPrefs.SetInt(Application.loadedLevelName+"_highscore",0);
		}*/
		if (Input.GetKey(KeyCode.T)) {
			restartGame = true;
		}
		if (Input.GetKey(KeyCode.Q)){
			quitGame = true;
			//Application.Quit();

		}
		/*
		if (GUI.Button(new Rect(Screen.width/2 - 150, Screen.height - 120, 120, 50),"Try Again")){
			
			RestartGame();
		}*/
	}
}

function RestartGame(){
	gameOver = false;
	Application.LoadLevel(Application.loadedLevel);
	//yield WaitForSeconds(1f);
	Runner.isAlive = true;
}

function Quit(){
	Application.LoadLevel("menu");
}

static function Death(){
	gameOver = true;
	//Debug.Log(gameOver);
	var highestScore :int = PlayerPrefs.GetInt(Application.loadedLevelName+"_highscore");
	var currentScore = Runner.score;
	if (currentScore <= highestScore){
		return;
	}
	if (currentScore > highestScore){
		yield WaitForSeconds(2f);
		PlayerPrefs.SetInt(Application.loadedLevelName+"_highscore",currentScore);
	}
	/*
	Debug.Log(currentScore);
	Debug.Log(highestScore);
	*/
}