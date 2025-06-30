using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform doorTransform; // Transform of the door
    public float openAngle = 90f; // Angle to open the door
    public float openSpeed = 2f; // Speed to open the door
    public KeyCode openKey = KeyCode.P; // Key to open the door

    private bool isPlayerNear = false;
    private bool isDoorOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        // Initial rotation of the door
        closedRotation = doorTransform.rotation;
        // Calculate the rotation for the door when it's open (rotate around Y axis for horizontal rotation)
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    {
        // Check if the player is near and the open key is pressed
        if (isPlayerNear && Input.GetKeyDown(openKey))
        {
            // Toggle the door state
            isDoorOpen = !isDoorOpen;
        }

        // Smoothly rotate the door to the open or closed position
        if (isDoorOpen)
        {
            doorTransform.rotation = Quaternion.Slerp(doorTransform.rotation, openRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            doorTransform.rotation = Quaternion.Slerp(doorTransform.rotation, closedRotation, Time.deltaTime * openSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player exits the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
