using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketCollider : MonoBehaviour
{
	[SerializeField]
	private RacketFollower _RacketCapsulePrefab;

	private void SpawnRacketCapsuleFollower()
	{
		var follower = Instantiate(_RacketCapsulePrefab);
		follower.transform.position = transform.position;
		follower.SetFollowTarget(this);
	}

	private void Start()
	{
		SpawnRacketCapsuleFollower();
	}
}
