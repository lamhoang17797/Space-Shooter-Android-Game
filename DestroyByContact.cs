using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameController == null) 
		{
			Debug.Log ("can't find GameController");
		}

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary")
		{
			return;
		}
		Instantiate(explosion, transform.position,transform.rotation);
		if (other.tag == "Player") // neu nguoi choi va cham
		{
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}
		gameController.AddScore (scoreValue); //cong diem cho player
		Destroy(other.gameObject);
		Destroy (gameObject);

	}

}
