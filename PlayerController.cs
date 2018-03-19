using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
[System.Serializable]

public class Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	

	public float speed;
	private Rigidbody rb;
	public Boundary boundary;
	public float tilt; //hệ số nghiêng

	public GameObject shot;
	public Transform shotSpawn; 
	public float fireRate;



	private float nextFire; 


	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		if ( Time.time > nextFire) 
		{
			//shot = Instantiate(shot) as GameObject;
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position,shotSpawn.rotation);
			//shot.transform.position = shotSpawn.transform.position;
			GetComponent<AudioSource>().Play();

		}
	}

	void FixedUpdate () 
	{
		//float moveHorizontal = Input.GetAxis("Horizontal");
		//float moveVertical = Input.GetAxis("Vertical");
		float moveHorizontal = CnInputManager.GetAxis("Horizontal");
		float moveVertical = CnInputManager.GetAxis("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal,0.0f, moveVertical);

		rb.velocity = movement*speed; //vận tốc player

		rb.position = new Vector3 
			(Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), //kẹp giá trị giữa xMax và xMin
			0.0f,
				Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax) //kẹp giá trị giữa zMax và zMin
			
		);

		rb.rotation = Quaternion.Euler (0.0f,0.0f,rb.velocity.x*-tilt);
	}
}
