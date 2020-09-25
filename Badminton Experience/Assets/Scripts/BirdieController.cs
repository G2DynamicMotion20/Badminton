using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdieController : MonoBehaviour
{
    public Text fakeConsole;

	private bool isHit = false;
    private bool isColiding = false;
	private Vector3 respawnLocation;
	private Rigidbody rb;
	private bool inBounds = false;
	private bool hitFloor = false;
    public RacketController racket;

	private float maxVelocity = 40f;
	public Color fast;
	public Color slow;


	private void Awake()
	{
		rb = this.GetComponent<Rigidbody>();
		respawnLocation = transform.position;

		fakeConsole = GameObject.Find("FakeConsole").GetComponent<Text>();
        racket = GameObject.Find("PlayerRacket").GetComponent<RacketController>();
	}

    void FixedUpdate()
    {
        if (isHit && !isColiding)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
		}
    }


	private void OnCollisionEnter(Collision collision)
	{
		
        isColiding = true;
		rb.useGravity = true;

		GameObject other = collision.gameObject;
		//fakeConsole.text = other.name;
		//if collides with a racket, apply force
        if (other.layer == 9 && !isHit) 
        {
			
            Rigidbody otherRB = other.GetComponent<Rigidbody>();
			float calc = otherRB.velocity.magnitude;
            int points = collision.contactCount;

			/* In case there is a need to manually add force based off of colision */
			
			Vector3 avg = Vector3.zero;
            for (int i = 0; i < points; i++)
            {
				avg += collision.GetContact(i).normal;
            }
			avg = avg.normalized;
            

			float rtForce = Mathf.Max(Mathf.Sqrt(calc), Mathf.Sqrt(calc) + 2.5f * Mathf.Log(calc));
			float vol = 1.25f * Mathf.Log10(calc + 10) - 1;

			// points + "\n" +
			

            SoundManager.SM.PlaySound("hit", Vector3.zero, Mathf.Min(1, vol));
			rb.velocity = Vector3.zero;
			rb.AddForce(avg * rtForce + Vector3.up * vol, ForceMode.Impulse);
			//rb.AddForce(Vector3.up * additionalForce, ForceMode.Impulse);
			GetComponent<TrailRenderer>().colorGradient = ColorVelocity(vol);
			other.GetComponent<RacketFollower>().Cooldown();
            fakeConsole.text =  calc + " - " + rtForce + " - " + vol + " - " + rb.velocity.magnitude;
			racket.LogSpeeds();
        }
		isHit = true;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!hitFloor && other.gameObject.layer == 11)
        {
			SoundManager.SM.PlaySound("score", Vector3.zero, 0.75f);
		}
		if (other.gameObject.layer == 13)
		{
			inBounds = true;
		}
		if (other.gameObject.layer == 12)
		{
            if (!hitFloor)
            {
                if (inBounds)
                {
                    SoundManager.SM.PlaySound("floor", transform.position);
                }
                else
                {
					SoundManager.SM.PlaySound("error", Vector3.zero, 0.5f);
				}
            }
			hitFloor = true;
		}
	}


    private void OnCollisionExit(Collision collision)
	{
		isColiding = false;
	}

    public void spawn(Vector3 location)
    {
		transform.position = location;
		rb.velocity = Vector3.zero;
		rb.useGravity = false;
		Debug.Log("moved!");
	}

    public Gradient ColorVelocity(float spd = 1f)
    {
		Gradient col = new Gradient();
		float velocity = rb.velocity.magnitude;
        Color c = Color.Lerp(slow, fast, Mathf.Min(1f, spd));
		GradientColorKey[] points = new GradientColorKey[2];
		GradientAlphaKey[] a = new GradientAlphaKey[2];
		points[0].color = c;
		points[0].time = 0;
		points[1].color = c;
		points[1].time = 1;
		a[0].alpha = 1;
		a[0].time = 0;
		a[1].alpha = 0;
		a[1].time = 1;

		col.SetKeys(points, a);

		return col;
	}
}
