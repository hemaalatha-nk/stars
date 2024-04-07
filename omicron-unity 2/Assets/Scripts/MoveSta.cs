using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSta : MonoBehaviour
{

    public Vector3 velocity;
    public Vector3 initialvelocity;

  
    // Start is called before the first frame update
    void Start()
    {
        //initialvelocity = velocity;

        //MoveObject("start");
    }

    // Update is called once per frame
    void Update()
    {

        Invoke("MoveObject", 2f);
    }
  

   void MoveObject()
    {
        transform.position += velocity * (Time.deltaTime / 1000);
        
    }

   
}
