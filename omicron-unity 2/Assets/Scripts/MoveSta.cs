using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static omicron.EventBase;

public class MoveSta : MonoBehaviour
{

    public Vector3 velocity;
    public Vector3 initialvelocity;

    public string flag = "start";
    public Vector3 initialPos;
    public Transform cameraTransform;
    public int jump;


    // Start is called before the first frame update
    void Start()
    {
        //initialvelocity = velocity;

        //MoveObject("start");
        initialPos = transform.position;

        //Invoke("MoveObject", 2f);

        //transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);
        Vector3 targetPosition = new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z);
        transform.LookAt(targetPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == "start" || flag == "resume")
        {
            transform.position += velocity * (Time.deltaTime / 600);
        }
        else if (flag == "reset")
        {
            transform.position = initialPos;
            flag = "start";
            //Debug.Log("stop");
        }
        else if (flag == "stop")
        {

        }
        else if (flag == "jump")
        {
            transform.position += initialPos * jump;
        }
    }
  

   void MoveObject()
    {
        transform.position += velocity * (Time.deltaTime / 1000);
        
    }

   
}
