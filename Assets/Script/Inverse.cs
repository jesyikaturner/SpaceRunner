using UnityEngine;

public class Inverse : MonoBehaviour {

	public Vector3 offset, rotationVelocity;
	public float recycleOffset, spawnChance;
	public float distance;
	public PlatformManagerV2 platformManager;
	
	void Start () {
		gameObject.active = false;
	}
	
	public void SpawnIfAvailable (Vector3 position) {
		if(gameObject.active || spawnChance <= Random.Range(0f, 100f)) {
			return;
		}
		transform.localPosition = position + offset;
		gameObject.active = true;
	}
	

	void OnTriggerEnter () {
		gameObject.active = false;
	}
	
	public void Update(){
		distance = transform.localPosition.x;
		if(transform.localPosition.x + recycleOffset < platformManager.curTransform.position.x){
			gameObject.active = false;
			return;
		}
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}
}