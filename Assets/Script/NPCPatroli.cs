using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints; // Array untuk waypoints
    public float moveSpeed = 3f; // Kecepatan pergerakan NPC

    private int currentWaypointIndex = 0;
    private Vector3 targetPosition;

    void Start()
    {
        if (waypoints.Length > 0)
        {
            SetTargetPosition();
        }
        else
        {
            Debug.LogError("No waypoints assigned to EnemyPatrol script.");
        }
    }

    void Update()
    {
        // Bergerak menuju target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Cek jika NPC mencapai target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            IterateWaypointIndex();
            SetTargetPosition();
        }
    }

    void SetTargetPosition()
    {
        targetPosition = waypoints[currentWaypointIndex].position;
    }

    void IterateWaypointIndex()
    {
        currentWaypointIndex++;
        if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = 0;
        }
    }
}
