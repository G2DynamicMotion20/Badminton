using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacketController : MonoBehaviour
{
    public GameObject birdiePrefab;

    private GameObject birdie;
    private Vector3 spawnLocation;
    private BirdieController birdControl;
    public GameObject racketCenter;

    public Rigidbody spedometer;

    public RacketFollower follow;

    public RacketFollower head;
    public RacketFollower handle;
    public Text fakeConsole;

    public GameObject[] children;

    // Start is called before the first frame update
    void Start()
    {
        birdie = null;
        birdControl = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        try
        {
            if (head != null && spedometer == null)
            {
                spedometer = head.gameObject.GetComponent<Rigidbody>();
            }
        }
        catch
        {
            fakeConsole.text = "failed to set spedometer";
        }

        if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetButton("Submit"))
		{
            //fakeConsole.text = "trying to respawn";
            head.Reset();
            if (birdie != null)
            {
                GameObject.Destroy(birdie);
                birdie = null;
                birdControl = null;
            }

            birdie = GameObject.Instantiate(birdiePrefab);
            birdControl = birdie.GetComponent<BirdieController>();
            

            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.Two) || Input.GetButtonDown("Jump"))
            {
                spawnLocation = racketCenter.transform.position + (new Vector3(0,-0.05f,0.3f));
            }

            birdControl.spawn(spawnLocation);

		}
        if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickHorizontal") != 0)
        {
            foreach (Transform child in transform)
            {
                child.transform.Rotate(new Vector3(-Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickHorizontal"), 0, 0));
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick) )
        {
            foreach (Transform child in transform)
            {
                child.transform.rotation.Set(0,0,0,0);
            }
        }
    }

    public void LogSpeeds()
    {
        fakeConsole.text += "\nhead :" + head.preimpactv
                           + "\nbody :" + handle.preimpactv;
        if (head.preimpactv > 2 && head.preimpactv * .4f < handle.preimpactv)
        {
            fakeConsole.text += "\nTry using your wrist more and swinging less!";
        }
    }

}
