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

    // Public boolean to control color transition (true = blue, false = green)
    private bool isBlue = true;

    // Start is called before the first frame update
    void Start()
    {
        isSteppedOn = 0;
        previousStepTime = 0.0f;
        rb = GetComponent<Rigidbody>();
        r = GetComponent<Renderer>();

        // Set initial color based on isBlue
        r.material.color = PlayerPrefs.GetInt("select") == 2 ? Color.blue : Color.green;
        isBlue = PlayerPrefs.GetInt("select") == 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSteppedOn > 0)
        {
            totalTimeStepped = (Time.time - startingStepTime) + previousStepTime;

            // Interpolate between blue/green and red based on progress
            float progress = Mathf.Clamp01(totalTimeStepped / timeUntilFall);
            if (isBlue)
            {
                // Blue to Red
                r.material.color = new Color(progress, 0, 1 - progress, 1.0f);
            }
            else
            {
                // Green to Red
                r.material.color = new Color(progress, 1 - progress, 0, 1.0f);
            }
        }

        if (totalTimeStepped >= timeUntilFall)
        {
            // Trigger fall and set final color to red
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
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isSteppedOn -= 1;
            previousStepTime += totalTimeStepped;
        }
    }
}

