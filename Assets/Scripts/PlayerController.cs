using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f; // Kecepatan gerak karakter
    [SerializeField] float sprintSpeed = 10f; // Kecepatan berlari
    [SerializeField] float rotationSpeed = 500f; // Kecepatan rotasi karakter
    [SerializeField] float interactionDistance = 2f; // Jarak interaksi dengan objek
    [SerializeField] Transform holdPoint; // Titik di depan karakter untuk memegang objek
    [SerializeField] GameObject arrowPrefab; // Prefab panah

    [Header("Ground Check Settings")]
    [SerializeField] float groundCheckRadius = 0.2f; // Radius untuk deteksi tanah
    [SerializeField] Vector3 groundCheckOffset; // Offset dari posisi karakter untuk deteksi tanah
    [SerializeField] LayerMask groundLayer; // Layer yang digunakan untuk menandai tanah

    bool isGrounded; // Apakah karakter berada di tanah
    bool isSprinting = false; // Apakah karakter sedang berlari
    bool isHoldingObject = false; // Apakah karakter sedang memegang objek
    GameObject heldObject = null; // Objek yang sedang dipegang
    GameObject arrowInstance = null; // Instance panah

    float ySpeed; // Kecepatan vertikal karakter
    Quaternion targetRotation; // Rotasi target untuk karakter

    CameraController cameraController; // Referensi ke script CameraController
    Animator animator; // Referensi ke komponen Animator
    CharacterController characterController; // Referensi ke komponen CharacterController

    private void Awake()
    {
        // Mengambil referensi komponen CameraController dari kamera utama
        cameraController = Camera.main.GetComponent<CameraController>();

        // Mengambil referensi komponen Animator dan CharacterController dari GameObject ini
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Mendapatkan input horizontal dan vertikal
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Mendeteksi input dari input system untuk berlari
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isSprinting = true;
        }
        else if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            isSprinting = false;
        }

        // Menghitung jumlah input untuk menentukan apakah karakter sedang bergerak
        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
        animator.SetFloat("moveAmount", moveAmount);

        // Normalisasi input gerak
        var moveInput = new Vector3(h, 0, v).normalized;

        // Menghitung kecepatan berjalan atau berlari berdasarkan input
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;
        animator.SetBool("isRunning", isSprinting);

        // Mendapatkan arah gerak dalam bidang datar kamera
        var moveDir = cameraController.PlanarRotation * moveInput;

        // Melakukan deteksi tanah
        GroundCheck();

        // Mengatur kecepatan vertikal karakter berdasarkan kondisi tanah
        if (isGrounded)
        {
            ySpeed = -0.5f; // Jika di tanah, beri sedikit kecepatan ke bawah
        }
        else
        {
            ySpeed += Physics.gravity.y * Time.deltaTime; // Jika di udara, terapkan gravitasi
        }

        // Menghitung kecepatan total karakter
        var velocity = moveDir * currentSpeed;
        velocity.y = ySpeed; // Menambahkan kecepatan vertikal

        // Memindahkan karakter menggunakan CharacterController
        characterController.Move(velocity * Time.deltaTime);

        // Mengatur rotasi karakter sesuai arah gerak
        if (moveAmount > 0)
        {
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        // Mengubah rotasi karakter secara perlahan
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Mengatur parameter animasi untuk kecepatan gerak karakter
        animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);

        // Mendeteksi input untuk mengambil atau melepaskan objek
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame || Keyboard.current.rightShiftKey.wasPressedThisFrame)
        {
            if (isHoldingObject)
            {
                DropObject();
            }
            else
            {
                TryPickupObject();
            }
        }

        // Memperbarui posisi panah jika sedang memegang objek
        if (isHoldingObject && arrowInstance != null)
        {
            arrowInstance.transform.position = heldObject.transform.position + Vector3.up * 2f; // Sesuaikan posisi panah di atas objek
        }
    }

    private void TryPickupObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionDistance);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Draggable"))
            {
                PickupObject(collider.gameObject);
                break;
            }
        }
    }

    private void PickupObject(GameObject obj)
    {
        heldObject = obj;
        heldObject.GetComponent<Rigidbody>().isKinematic = true;
        heldObject.transform.SetParent(holdPoint);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;
        isHoldingObject = true;

        // Instantiate panah di atas objek yang dipegang
        arrowInstance = Instantiate(arrowPrefab, heldObject.transform.position + Vector3.up * 2f, Quaternion.identity);
    }

    private void DropObject()
    {
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject.transform.SetParent(null);
        heldObject = null;
        isHoldingObject = false;

        // Menghapus panah jika ada
        if (arrowInstance != null)
        {
            Destroy(arrowInstance);
        }
    }

    // Fungsi untuk melakukan deteksi tanah menggunakan sphere cast
    void GroundCheck()
    {
        // Menjalankan sphere cast untuk mendeteksi tanah di bawah karakter
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
    }

    // Fungsi ini digunakan untuk menggambar gizmos untuk deteksi tanah di Unity Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}
