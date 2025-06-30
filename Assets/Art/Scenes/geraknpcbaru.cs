using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class geraknpcbaru : MonoBehaviour
{
    public Transform target;
    public Transform[] patrolPoints;
    public float detectionRadius = 10f;
    public float obstacleAvoidanceRadius = 2f;
    public float searchDelay = 5f; // Waktu delay untuk mencari target baru
    public float randomMoveRadius = 5f; // Radius pergerakan acak

    private NavMeshAgent agent;
    private int currentPatrolIndex;
    private bool isPatrolling;
    private Animator animator;
    private float walkSpeed = 2f; // Kecepatan berjalan

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = walkSpeed; // Set kecepatan berjalan

        if (patrolPoints.Length > 0)
        {
            isPatrolling = true;
            currentPatrolIndex = 0;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }

        StartCoroutine(SearchForTarget());
    }

    void Update()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) <= detectionRadius)
        {
            agent.SetDestination(target.position);
            isPatrolling = false;
        }
        else if (isPatrolling)
        {
            Patrol();
        }

        animator.SetBool("isWalking", agent.velocity.magnitude > 0);
    }

    void Patrol()
    {
        // Tentukan radius jarak minimum untuk menentukan NPC hanya berjalan di tempat
        float minimumDistance = 0.5f;

        if (agent.remainingDistance < minimumDistance)
        {
            // Tentukan lokasi acak dalam radius tertentu
            Vector3 randomDirection = Random.insideUnitSphere * randomMoveRadius;
            randomDirection += transform.position;
            NavMeshHit hit;

            // Cari posisi yang valid di NavMesh
            if (NavMesh.SamplePosition(randomDirection, out hit, randomMoveRadius, 1))
            {
                Vector3 finalPosition = hit.position;
                agent.SetDestination(finalPosition);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, obstacleAvoidanceRadius);
    }

    void FixedUpdate()
    {
        AvoidObstacles();
    }

    void AvoidObstacles()
    {
        Collider[] obstacles = Physics.OverlapSphere(transform.position, obstacleAvoidanceRadius);
        foreach (Collider obstacle in obstacles)
        {
            if (obstacle.CompareTag("Obstacle"))
            {
                Vector3 avoidDirection = transform.position - obstacle.transform.position;
                Vector3 newTargetPosition = transform.position + avoidDirection.normalized * obstacleAvoidanceRadius;
                agent.SetDestination(newTargetPosition);
                break;
            }
        }
    }

    IEnumerator SearchForTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(searchDelay);

            if (target == null) // Jika tidak ada target
            {
                // Pergi ke lokasi acak dalam radius tertentu
                Vector3 randomDirection = Random.insideUnitSphere * randomMoveRadius;
                randomDirection += transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, randomMoveRadius, 1);
                Vector3 finalPosition = hit.position;
                agent.SetDestination(finalPosition);

                isPatrolling = false; // Hentikan patroli sementara
                yield return new WaitForSeconds(5f); // Tunggu beberapa saat sebelum melanjutkan patroli
                isPatrolling = true; // Lanjutkan patroli setelah pencarian selesai
            }
        }
    }
}
