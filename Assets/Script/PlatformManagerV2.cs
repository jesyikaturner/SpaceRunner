using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformManagerV2 : MonoBehaviour {

	public Transform prefab;
	//public Transform prefab2;
	//public Transform prefab3;
	
	public Coins coins;
	public Coins coin2;
	public Coins coin3;
	public Coins coin4;
	public Coins coin5;
	
	public SpikeTrap spikeTrap;
	public SpikeTrap spikeTrap2;
	public SpikeTrap spikeTrap3;
	
	public SpeedUp speedUp;
	public SpeedUp speedUp2;
	public SpeedUp speedUp3;
	
	public SpeedDown speedDown;
	public SpeedDown speedDown2;
	public SpeedDown speedDown3;
	
	public Inverse inverse;
	public Inverse inverse2;	
	
	public Transform curTransform;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 minSize, maxSize, minGap, maxGap;
	public float minY, maxY;
	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;
	private Queue<int> choices;
	
	public Material[] materials;
	

	void Start () {
		choices = new Queue<int>(2);
		choices.Enqueue(-6);
		choices.Enqueue(-3);
		objectQueue = new Queue<Transform>(numberOfObjects);
		for(int i = 0; i < numberOfObjects; i++){
			objectQueue.Enqueue((Transform)Instantiate(prefab));
			//objectQueue.Enqueue((Transform)Instantiate(prefab2));
			//objectQueue.Enqueue((Transform)Instantiate(prefab3));
		}
		nextPosition = transform.localPosition;
		for(int i = 0; i < numberOfObjects; i++){
			Recycle();
		}
		
	}
	void Update () {

		
		if(objectQueue.Peek().localPosition.x + recycleOffset < curTransform.position.x){
			Recycle();
		}

		
	}

	private void Recycle () {
		Vector3 scale = new Vector3(
			Random.Range(minSize.x, maxSize.x),
			Random.Range(minSize.y, maxSize.y),
			Random.Range(minSize.z, maxSize.z));
			

		Vector3 position = nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;
		
		coins.SpawnIfAvailable(position);
		coin2.SpawnIfAvailable(position);
		coin3.SpawnIfAvailable(position);
		coin4.SpawnIfAvailable(position);
		coin5.SpawnIfAvailable(position);
		
		spikeTrap.SpawnIfAvailable(position);
		spikeTrap2.SpawnIfAvailable(position);
		spikeTrap3.SpawnIfAvailable(position);
		
		speedUp.SpawnIfAvailable(position);
		speedUp2.SpawnIfAvailable(position);
		speedUp3.SpawnIfAvailable(position);
		
		speedDown.SpawnIfAvailable(position);
		speedDown2.SpawnIfAvailable(position);
		speedDown3.SpawnIfAvailable(position);
		
		inverse.SpawnIfAvailable(position);
		inverse2.SpawnIfAvailable(position);
		

		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		int materialIndex = Random.Range(0, materials.Length);
		o.GetComponent<Renderer>().material = materials[materialIndex];
		objectQueue.Enqueue(o);

		nextPosition += new Vector3(
			Random.Range(minGap.x, maxGap.x) + scale.x,
			Random.Range(minGap.y, maxGap.y),
			Random.Range(0,0));

		if(nextPosition.y < minY){
			nextPosition.y = minY + maxGap.y;
		}
		else if(nextPosition.y > maxY){
			nextPosition.y = maxY - maxGap.y;
		}
	}
}