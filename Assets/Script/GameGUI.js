#pragma strict

var HUDSkin : GUISkin;
var	mainMenu = true;

var scoreText :String = "SCORE: ";

public static var gameOver = false;
public static var gameOverScreen = 0; //0 is default;

var myArray1 :Array = new Array("Good Work!", "Nice Try!", "Keep It Up!", "You'll Get There!");
var myArray2 :Array = new Array("CONGRATULATIONS!", "EXCELLENT WORK!", "AMAZING JOB!", "WOW!");
var myIndex1;
var myIndex2;


/*
	gameover screen 1, and gameover screen 2
	gs1 is for when the player's score is less than the highscore
	gs2 is for when the player's score is more than the highscore
	
	 
	 gameplayer HUD
	 
*/

function Start () {
	myIndex1 = Random.Range(0,(myArray1.length));
	myIndex2 = Random.Range(0,(myArray2.length));
	//mainMenuGUI = true;


}

function Update () {
	if (Input.GetKeyUp(KeyCode.M)){
		if (!Runner.isMuted){
			Runner.isMuted = true;
		} else {
			Runner.isMuted = false;	
		}
	}
}


function OnGUI(){

	//GUI setting the styles needed and setting what the skin is
	GUI.skin = HUDSkin;
	var mainMenuBox :GUIStyle = GUI.skin.GetStyle("mainMenuB");
	var instructHead :GUIStyle = GUI.skin.GetStyle("instructionsH");
	var instructBody :GUIStyle = GUI.skin.GetStyle("instructionsB");
	var menuButtons :GUIStyle = GUI.skin.GetStyle("menubuttons");
	var menuTitle :GUIStyle = GUI.skin.GetStyle("mainMenuHeader");
	var encourageWords :GUIStyle = GUI.skin.GetStyle("wordsofencouragement");
	var wBox :GUIStyle = GUI.skin.GetStyle("whitebox");
	
	//Mainmenu
	if (mainMenu){
		//Menu Screen Instructions and Buttons allowed only in Menu;
		GUI.Label(new Rect(60, 40, 200, Screen.height/2),"Space Runner",menuTitle);
		GUI.Label(new Rect(380, 120, 200, Screen.height/2),"Jessica Turner",instructBody);
		
		GUI.Box(new Rect(88, 198, 325, 425),"", wBox);
		GUI.Box(new Rect(90, 200, 320, 420),"", mainMenuBox);
		
		//GUI.Label(new Rect(123, 220, 200, Screen.height/2),"Instructions",instructHead);
		
		GUI.Label(new Rect(140, 230, 200, Screen.height/2),"Current High Score"+ "\n"+"\t"+"\t"+"\t"+"\t"+"\t" +PlayerPrefs.GetInt(Application.loadedLevelName+"_highscore"), instructHead);	

		
		GUI.Label(new Rect(180, 335, 200, Screen.height/2),"(P) to play.",menuButtons);
		GUI.Label(new Rect(180, 370, 200, Screen.height/2),"(Q) to quit.",menuButtons);	
		
		GUI.Label(new Rect(190, 430, 200, Screen.height/2),"(A) to jump left.",instructBody);	
		GUI.Label(new Rect(190, 450, 200, Screen.height/2),"(D) to jump right.",instructBody);
		GUI.Label(new Rect(190, 470, 200, Screen.height/2),"(Space) to jump.",instructBody);
		GUI.Label(new Rect(190, 490, 200, Screen.height/2),"(M) to mute.",instructBody);
		GUI.Label(new Rect(190, 510, 200, Screen.height/2),"(C) to clear score.",instructBody);
		
		
		if (Input.GetKeyUp(KeyCode.P)){
			Runner.startGame = true;
			mainMenu = false;
		}
		if (Input.GetKeyUp(KeyCode.Q)){
			Application.Quit();
		}
		if (Input.GetKeyUp(KeyCode.C)){
			PlayerPrefs.SetInt(Application.loadedLevelName+"_highscore",0);
		}
	}
	//Gameplay HUD
	if (Runner.startGame){
		GUI.Label(new Rect(50, 10, Screen.width/2, 100), scoreText +Runner.score);
		if (Runner.inverse){
			GUI.Label(new Rect(Screen.width/2 - 100, 10, 200, 100),"INVERSE");
		}
	}
	
	//Gameover Screens
	if (gameOver && gameOverScreen == 1){
		//Box
		GUI.Box(new Rect(197, 97, Screen.width - 394, Screen.height - 194),"", wBox);
		GUI.Box(new Rect(200, 100, Screen.width - 400, Screen.height - 200),"");
		//word of slight encouragement -- choose one of 4 phrases
    	GUI.Label(new Rect(Screen.width/2 - 100, 150, 200, 100),"" +myArray1[myIndex1], encourageWords);
    	
    	
		//display current score and highscore
		GUI.Label(new Rect(Screen.width/2 - 100, 300, 200, 100),"Current Score: " +Runner.score, encourageWords);
		GUI.Label(new Rect(Screen.width/2 - 100, 370, 200, 100),"High Score: " +PlayerPrefs.GetInt(Application.loadedLevelName+"_highscore"), encourageWords);
		
		GUI.Label(new Rect(Screen.width/2 - 50, 510, 200, 200),"(P) to continue.",instructBody);
		
		
		if (Input.GetKeyUp(KeyCode.P)){
			Restart();
		}
	}
	
	if (gameOver && gameOverScreen == 2){
		//Box
		GUI.Box(new Rect(197, 97, Screen.width - 394, Screen.height - 194),"", wBox);
		GUI.Box(new Rect(200, 100, Screen.width - 400, Screen.height - 200),"");
		//word of great encouragement -- choose one of 4 phrases
		GUI.Label(new Rect(Screen.width/2 - 100, 150, 200, 100),"" +myArray2[myIndex2], encourageWords);
		//display highscore
		GUI.Label(new Rect(Screen.width/2 - 100, 300, 200, 100),"High Score: " +PlayerPrefs.GetInt(Application.loadedLevelName+"_highscore"), encourageWords);
		
		GUI.Label(new Rect(Screen.width/2 - 50, 510, 200, 200),"(P) to continue.",instructBody);
		
		
		if (Input.GetKeyUp(KeyCode.P)){
			Restart();
		}
	}
		
}

function Restart(){
	gameOver  = false ;
	gameOverScreen = 1;
	Runner.isAlive = true;
	Application.LoadLevel(Application.loadedLevel);
	
}

public static function GameOver(){
	gameOver = true;
	//Runner.startGame = false;
	var highestScore :int = PlayerPrefs.GetInt(Application.loadedLevelName+"_highscore");
	var currentScore = Runner.score;
	
	if (currentScore <= highestScore){
		gameOverScreen = 1;
		return;
	}
	
	if (currentScore > highestScore){
		PlayerPrefs.SetInt(Application.loadedLevelName+"_highscore",currentScore);
		gameOverScreen = 2;
	}

}

