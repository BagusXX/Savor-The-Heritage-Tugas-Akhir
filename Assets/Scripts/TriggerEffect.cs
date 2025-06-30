using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class TriggerEffect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            StartCoroutine(StopMovement(other));
        }
    }

    private IEnumerator StopMovement(Collider entity)
    {
        NavMeshAgent agent = entity.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.isStopped = true;
            yield return new WaitForSeconds(3f);
            agent.isStopped = false;
        }
        else
        {
            // Jika tidak menggunakan NavMeshAgent (misal: untuk player dengan kontrol manual)
            MonoBehaviour[] scripts = entity.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false; // Menonaktifkan semua skrip pada objek selama 3 detik
            }
            yield return new WaitForSeconds(3f);
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = true; // Mengaktifkan kembali semua skrip setelah 3 detik
            }
        }
    }
}
