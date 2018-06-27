using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class backgroundGround: MonoBehaviour {

	public Transform prefab;
	/*
	public Coins coins;
	public Coins coin2;
	public Coins coin3;
	public Coins coin4;
	public Coins coin5;
	
	public SpikeTrap spikeTrap;
	public SpikeTrap spikeTrap2;
	public SpikeTrap spikeTrap3;
	*/
	
	public Transform curTransform;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 minSize, maxSize, minGap, maxGap;
	public float minY, maxY;
	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;
	private Queue<int> choices;
	

	void Start () {
		choices = new Queue<int>(2);
		choices.Enqueue(-6);
		choices.Enqueue(-3);
		objectQueue = new Queue<Transform>(numberOfObjects);
		for(int i = 0; i < numberOfObjects; i++){
			objectQueue.Enqueue((Transform)Instantiate(prefab));
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
		
		/*
		coins.SpawnIfAvailable(position);
		coin2.SpawnIfAvailable(position);
		coin3.SpawnIfAvailable(position);
		coin4.SpawnIfAvailable(position);
		coin5.SpawnIfAvailable(position);
		
		spikeTrap.SpawnIfAvailable(position);
		spikeTrap2.SpawnIfAvailable(position);
		spikeTrap3.SpawnIfAvailable(position);
		*/

		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
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