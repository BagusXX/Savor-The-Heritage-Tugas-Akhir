using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget; // Transform dari objek yang akan diikuti kamera

    [SerializeField] float rotationSpeed = 2f; // Kecepatan rotasi kamera
    [SerializeField] float distance = 5; // Jarak antara kamera dan target

    [SerializeField] float minVerticalAngle = -45; // Sudut minimum vertikal untuk rotasi kamera
    [SerializeField] float maxVerticalAngle = 45; // Sudut maksimum vertikal untuk rotasi kamera

    [SerializeField] Vector2 framingOffset; // Offset dari posisi target untuk framing

    [SerializeField] bool invertX; // Apakah menginvert input mouse X
    [SerializeField] bool invertY; // Apakah menginvert input mouse Y

    float rotationX; // Rotasi kamera pada sumbu X
    float rotationY; // Rotasi kamera pada sumbu Y

    float invertXval; // Nilai invert untuk input mouse X
    float invertYval; // Nilai invert untuk input mouse Y

    [SerializeField] float zoomSpeed = 2f; // Kecepatan zoom in dan zoom out
    [SerializeField] float minDistance = 2f; // Jarak minimum kamera
    [SerializeField] float maxDistance = 10f; // Jarak maksimum kamera

    void Start()
    {
        // Mengatur kursor menjadi tidak terlihat dan mengunci kursor ke tengah layar
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Mengatur nilai invertXval dan invertYval berdasarkan kondisi invertX dan invertY
        invertXval = (invertX) ? -1 : 1;
        invertYval = (invertY) ? -1 : 1;

        // Mengatur rotasi awal kamera di belakang target
        rotationX = 10f; // Sudut pandang vertikal awal yang wajar
        rotationY = followTarget.eulerAngles.y; // Mengambil sudut Y dari target

        // Inisialisasi posisi dan rotasi kamera
        UpdateCameraPositionAndRotation();
    }

    void Update()
    {
        // Mengupdate rotasi kamera berdasarkan input mouse
        rotationX += Input.GetAxis("Camera Y") * invertYval * rotationSpeed * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle); // Membatasi rotasi kamera pada sumbu X

        rotationY += Input.GetAxis("Camera X") * invertXval * rotationSpeed * Time.deltaTime;

        // Zoom in dan zoom out dengan tombol K dan L
        float zoomInput = Input.GetAxis("Zoom");
        distance = Mathf.Clamp(distance - zoomInput * zoomSpeed * Time.deltaTime, minDistance, maxDistance);

        // Atur posisi dan rotasi kamera
        UpdateCameraPositionAndRotation();
    }

    void UpdateCameraPositionAndRotation()
    {
        // Menghitung rotasi target kamera
        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        // Inisialisasi focusPosition dengan posisi followTarget ditambah framingOffset
        var focusPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y, 0);

        // Atur posisi dan rotasi kamera
        transform.position = focusPosition - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;
    }

    // Properti untuk mendapatkan rotasi horizontal kamera tanpa rotasi pada sumbu Y
    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}
