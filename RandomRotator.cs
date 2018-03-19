using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
	public float tumble;
	void Start () 
	{
		GetComponent<Rigidbody>().angularVelocity= Random.insideUnitSphere*tumble; // vận tốc góc = tọa độ random*hệ số quay lộn xộn
	}
	

}
