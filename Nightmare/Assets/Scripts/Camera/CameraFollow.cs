using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	//Tranforme do Player
	public Transform target;

	//Amaciar a transicao da camera
	public float smothing =5f;

	//Distancia do player para a camera
	Vector3 offset;


	// Use this for initialization
	void Start () {

		offset = transform.position - target.position;
	
	}

	void FixedUpdate(){

		//Posicao no va da camera
		Vector3 targetCampPos = target.position + offset;

		//setando a nova posicao da camera
		transform.position = Vector3.Lerp (transform.position, targetCampPos, smothing *Time.deltaTime);

	}

}
