using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObj : MonoBehaviour
{

    public float rotationSpeed = 200f;

    bool on = true;
    public void GoRotate(bool isOn)
    { 
     on  = isOn;    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (on) transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }



}
