using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketController : MonoBehaviour
{
	public BirdieController birdie;
  public GameObject racketCenter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if (OVRInput.GetDown(OVRInput.Button.One))
		{


            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.Two))
            {
                birdie.resetSpawn(racketCenter.transform.position);
            }
            else
            {
                birdie.respawn();
            }

		}
    }


}
