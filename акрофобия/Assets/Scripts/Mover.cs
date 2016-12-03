using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
	public float speed;
	private Rigidbody2D rb2d;
	
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D>();

	}
	void Update(){
		rb2d.AddForce(transform.forward * speed);

	}
}