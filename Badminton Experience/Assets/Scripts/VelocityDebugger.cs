using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocityDebugger : MonoBehaviour
{
    [SerializeField]
    private float maxVelocity = 20f;

    //private Text ffconsole;
        
    // Start is called before the first frame update
    void Start()
    {
        //ffconsole = GameObject.Find("FakeFakeConsole").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.color = ColorForVelocity();
    }

    private Color ColorForVelocity()
    {
        float velocity = GetComponent<Rigidbody>().velocity.magnitude;
        //ffconsole.text = this.gameObject.name + " " + velocity.ToString();
        return Color.Lerp(Color.green, Color.red, velocity / maxVelocity);
    }
}
