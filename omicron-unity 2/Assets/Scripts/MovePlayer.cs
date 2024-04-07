using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MovePlayer : MonoBehaviour
{


    public GameObject player; // Reference to the GameObject whose position will be the initial position
    Vector3 targetPosition=new Vector3(16.29f, -0.3f, 8.9f); // The target position to move towards
     //float duration = 5f; // The duration of the movement

    private bool isMoving = false; // Flag to check if movement is in progress

    private Vector3 initialPosition; // Initial position of the object
    private Quaternion initialRotation; // Initial rotation of the object

     bool activate = false;

   

    private void Start()
    {
        initialPosition = player.transform.position;
        initialRotation = player.transform.rotation;
        if (activate)
        {
            Invoke("StartMovementToTargetConstellation", 10f);
            Invoke("ResetPosition", 25f);
            Invoke("ResetRotation", 35f);
        }
    }

    public void StartMovementToTargetConstellation()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveObject(player.transform.position, player.transform.rotation, targetPosition, Quaternion.Euler(new Vector3(0, 59.33f, 0))));
        }
    }

    public void ResetPosition()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveObject(player.transform.position, player.transform.rotation, initialPosition, player.transform.rotation));
        }
    }

    public void ResetRotation()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveObject(player.transform.position, player.transform.rotation, player.transform.position, initialRotation));
        }
    }

    IEnumerator MoveObject(Vector3 currentPosition, Quaternion currentRotation, Vector3 targetPosition, Quaternion targetRotation)
    {
        isMoving = true; // Set flag to indicate movement is in progress

        float elapsedTime = 0f;

        while (elapsedTime < 5f)
        {
            // Calculate the interpolation ratio based on elapsed time and duration
            float t = elapsedTime / 5f;

            // Use Vector3.Lerp to smoothly interpolate between initial and target positions
            player.transform.position = Vector3.Lerp(currentPosition, targetPosition, t);

            // Use Quaternion.Slerp to smoothly interpolate between initial and target rotations
            player.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, t);

            // Increment elapsed time by the time passed since the last frame
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the object reaches the target position and rotation precisely
        player.transform.position = targetPosition;
        player.transform.rotation = targetRotation;

        isMoving = false; // Reset the flag after movement is complete
    }

}
