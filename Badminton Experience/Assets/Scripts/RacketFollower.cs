using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacketFollower : MonoBehaviour
{
    private RacketCollider _racketFollower;
	private Rigidbody _rigidbody;
	private Vector3 _velocity;

	public Text fakeConsole;
	public bool head;

	private float timer = -1f;
	public float cooldown = 1f;
	public float preimpactv;

	[SerializeField]
	private float _sensitivity = 200f;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		fakeConsole = GameObject.Find("FakeConsole").GetComponent<Text>();
        
		
	}

    private void Update()
    {
        if (timer > 0)
        {
			timer -= Time.deltaTime;
            if (timer <= 0)
            {
				_rigidbody.detectCollisions = true;
				//fakeConsole.text += "\nRacket collider enabled";
			}
        }
        else
        {
            preimpactv = GetComponent<Rigidbody>().velocity.magnitude;
        }
		

	}

    private void FixedUpdate()
	{
		Vector3 destination = _racketFollower.transform.position;
		_rigidbody.transform.rotation = transform.rotation;

		_velocity = (destination - _rigidbody.transform.position) * _sensitivity;

		_rigidbody.velocity = _velocity;
		transform.rotation = _racketFollower.transform.rotation;
	}

	public void SetFollowTarget(RacketCollider racketFollower, bool head)
	{
		_racketFollower = racketFollower;
        if (head)
        {
            GameObject.Find("PlayerRacket").GetComponent<RacketController>().head = this;
        }
        else
        {
			GameObject.Find("PlayerRacket").GetComponent<RacketController>().handle = this;
			_rigidbody.detectCollisions = false;
		}
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

    public void Reset()
    {
		timer = -1f;
		_rigidbody.detectCollisions = true;
		//fakeConsole.text += "\nRacket collider enabled";
	}

    public void Cooldown()
    {

		timer = cooldown;
        _rigidbody.detectCollisions = false;
		//fakeConsole.text += "\nRacket collider disabled";
	}

}

