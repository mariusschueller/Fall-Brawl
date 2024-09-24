using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFall : MonoBehaviour
{
    private int isSteppedOn;
    public float timeUntilFall = 5.0f;
    private float startingStepTime;
    private float totalTimeStepped;
    private float previousStepTime;
    private Renderer r;
    
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        isSteppedOn = 0;
        previousStepTime = 0.0f;
        rb = GetComponent<Rigidbody>();
        r = GetComponent<Renderer>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isSteppedOn > 0)
        {
            totalTimeStepped = (Time.time - startingStepTime) + previousStepTime;
            // Debug.Log(totalTimeStepped);
            if (r.material.color.r < totalTimeStepped / timeUntilFall &&
                r.material.color.g > 1 - (totalTimeStepped / timeUntilFall))
            {
                r.material.color = new Color(totalTimeStepped / timeUntilFall, 1-(totalTimeStepped/timeUntilFall), 0,1.0f);
            }
            
        }

        if (totalTimeStepped >= timeUntilFall)
        {
            Debug.Log("Fall");
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;
            r.material.color = Color.red;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isSteppedOn += 1;

            startingStepTime = Time.time;


            Debug.Log("starting step");
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isSteppedOn -= 1;
            previousStepTime += totalTimeStepped;
            Debug.Log("Ending step");
        }
    }

    
}
