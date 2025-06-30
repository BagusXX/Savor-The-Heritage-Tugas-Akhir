using UnityEngine;

public class DragAndDropController : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject selectedObject;
    private bool isDragging = false;
    private Vector3 offset;

    public float maxDistance = 5f; // Jarak maksimum objek dapat digerakkan
    public float minDistance = 1f; // Jarak minimum objek dapat digerakkan
    public LayerMask collisionMask; // LayerMask untuk objek yang dapat bertabrakan

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("Draggable"))
                {
                    selectedObject = hit.collider.gameObject;
                    offset = selectedObject.transform.position - hit.point;
                    isDragging = true;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.N) && isDragging)
        {
            if (selectedObject != null)
            {
                Rigidbody rb = selectedObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = true;
                    selectedObject = null;
                    isDragging = false;
                }
            }
        }

        if (isDragging)
        {
            // Buat ray dari kamera melalui tengah layar
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Vector3 worldPosition = ray.GetPoint(Vector3.Distance(mainCamera.transform.position, selectedObject.transform.position)) + offset;

            // Batasi jarak objek berdasarkan jarak maksimum dan minimum
            float distanceToCamera = Vector3.Distance(mainCamera.transform.position, worldPosition);
            float clampedDistance = Mathf.Clamp(distanceToCamera, minDistance, maxDistance);
            worldPosition = mainCamera.transform.position + (worldPosition - mainCamera.transform.position).normalized * clampedDistance;

            // Check if the new position would cause a collision
            Collider objCollider = selectedObject.GetComponent<Collider>();
            if (objCollider != null)
            {
                Vector3 direction = worldPosition - selectedObject.transform.position;
                float distance = direction.magnitude;

                if (!Physics.BoxCast(selectedObject.transform.position, objCollider.bounds.extents, direction, Quaternion.identity, distance, collisionMask))
                {
                    selectedObject.transform.position = worldPosition;
                }
            }
            else
            {
                selectedObject.transform.position = worldPosition;
            }
        }
    }
}
