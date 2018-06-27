using UnityEngine;

public class Coins : MonoBehaviour {

	public Vector3 offset, rotationVelocity;
	public float recycleOffset, spawnChance;
	public float distance;
	public PlatformManagerV2 platformManager;
	
	void Start () {
        gameObject.SetActive(false);
	}
	
	public void SpawnIfAvailable (Vector3 position) {
		if(gameObject.activeSelf || spawnChance <= Random.Range(0f, 100f)) {
			return;
		}
		transform.localPosition = position + offset;
        gameObject.SetActive(true);
	}
	

	void OnTriggerEnter () {
        gameObject.SetActive(false);
	}
	
	public void Update(){
		distance = transform.localPosition.x;
		if(transform.localPosition.x + recycleOffset < platformManager.curTransform.position.x){
            gameObject.SetActive(false);
			return;
		}
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}
}
