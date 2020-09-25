using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketCollider : MonoBehaviour
{
	[SerializeField]
	private RacketFollower _RacketCapsulePrefab;

	private RacketFollower instance;
	public bool head;

	private void SpawnRacketCapsuleFollower()
	{
		instance = Instantiate(_RacketCapsulePrefab);
		instance.transform.position = transform.position;
		instance.SetFollowTarget(this, head);
	}

	private void Start()
	{
		SpawnRacketCapsuleFollower();
	}

    public float GetSpeed()
    {
		return instance.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
	}
}
