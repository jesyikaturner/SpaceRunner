#pragma strict
var rotationVelocity :Vector3;
function Start () {

}

function Update () {
	if (Runner.startGame && Runner.isAlive){
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}
}