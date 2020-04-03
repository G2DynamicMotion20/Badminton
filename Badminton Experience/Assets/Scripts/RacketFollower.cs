using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketFollower : MonoBehaviour
{
    private RacketCollider _racketFollower;
	private Rigidbody _rigidbody;
	private Vector3 _velocity;

	[SerializeField]
	private float _sensitivity = 100f;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		Vector3 destination = _racketFollower.transform.position;
		_rigidbody.transform.rotation = transform.rotation;

		_velocity = (destination - _rigidbody.transform.position) * _sensitivity;

		_rigidbody.velocity = _velocity;
		transform.rotation = _racketFollower.transform.rotation;
	}

	public void SetFollowTarget(RacketCollider racketFollower)
	{
		_racketFollower = racketFollower;
	}

    public void OnCollisionEnter(Collision collision)
    {
		GameObject obj = collision.gameObject;
        if (obj.layer == 8)
        {
			Rigidbody bird = obj.GetComponent<Rigidbody>();
            //bird.AddForce(transform.rotation * Vector3.forward * _rigidbody.velocity.magnitude, ForceMode.Impulse);

        }
    }
}

