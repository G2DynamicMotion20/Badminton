using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdieController : MonoBehaviour
{
	private bool isColiding = false;

	private Vector3 respawnLocation;

	public Rigidbody rb;
	// Start is called before the first frame update
	private void Awake()
	{
		rb = this.GetComponent<Rigidbody>();
		respawnLocation = transform.position;
	}

	void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (!isColiding)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
		}
    }


	private void OnCollisionEnter(Collision collision)
	{
		isColiding = true;
		rb.useGravity = true;
	}

	private void OnCollisionExit(Collision collision)
	{
		isColiding = false;
	}

    public void respawn()
    {
		transform.position = respawnLocation;
		rb.velocity = Vector3.zero;
		rb.useGravity = false;
		Debug.Log("moved!");
	}

	public void resetSpawn(Vector3 location)
	{
		respawnLocation = location;
		respawnLocation.z += 0.4f;
		respawnLocation.y += -.02f;
		respawn();
	}
}
