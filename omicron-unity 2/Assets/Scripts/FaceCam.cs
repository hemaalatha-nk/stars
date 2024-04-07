using UnityEngine;

public class FaceCam : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        // Optional: Make it only rotate around the Y axis
        // transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}