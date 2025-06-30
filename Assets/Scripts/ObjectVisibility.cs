using UnityEngine;

public class ObjectVisibilityController : MonoBehaviour
{
    public GameObject[] objectsToDisable; // Array objek yang akan dihilangkan
    public GameObject[] objectsToEnable; // Array objek yang akan muncul

    void Update()
    {
        for (int i = 0; i < objectsToDisable.Length; i++)
        {
            // Periksa jika objectToDisable[i] tidak aktif atau tidak ada
            if (objectsToDisable[i] == null || !objectsToDisable[i].activeInHierarchy)
            {
                if (i < objectsToEnable.Length && objectsToEnable[i] != null && !objectsToEnable[i].activeInHierarchy)
                {
                    objectsToEnable[i].SetActive(true); // Aktifkan objectToEnable[i]
                }
            }
        }
    }
}
