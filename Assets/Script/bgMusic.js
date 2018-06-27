#pragma strict
@script RequireComponent(AudioSource)

var isMuted = false;

function Start () {
	
}

function Update () {
	if (Input.GetKeyUp(KeyCode.M)){
		if (!isMuted){
			isMuted = true;
		} else {
			isMuted = false;	
		}
	}

}