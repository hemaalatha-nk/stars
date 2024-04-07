﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DistanceFromSolHandler : MonoBehaviour
{
    public Transform origin; // Reference to the original position
    private Vector3 originalPosition; // Original position of the GameObject
    //private float distanceFromSol;
    public string displayText = "Distance from sol is xxx parsecs";
    public TMP_Text text;
    public TMP_Text distance_travel;
    public float updateInterval = 1f; // Set the delay in seconds
    float distanceInLightYears = 0;
    // Start is called before the first frame update
    void Start()
    {
        // Save the original position
        originalPosition = origin.position;
        StartCoroutine(UpdateDisplayText());
    }

    // Update is called once per frame
    void Update()
    {
        distanceInLightYears = distanceInLightYears+Time.deltaTime * 1000;
        distance_travel.text = distanceInLightYears.ToString("F2") + " Light Years";
    }

    private IEnumerator UpdateDisplayText()
    {
        // Loop infinitely
        while (true)
        {
            // Calculate the distance between current position and original position
            float distance = Vector3.Distance(origin.position, originalPosition);

            string[] parts = displayText.Split(new string[] { "xxx" }, StringSplitOptions.None);
          

            string output = parts[0] + distance.ToString("F2") + parts[1];
            //Debug.Log(output);
            text.text = output;
           

            // Wait for the specified interval before calculating distance again
            yield return new WaitForSeconds(updateInterval);
        }
    }

    //private IEnumerator UpdateDisplayText()
}
