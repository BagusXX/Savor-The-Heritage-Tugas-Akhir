using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNPC : MonoBehaviour
{
    [Header("Ground Check Settings")]
    [SerializeField] float groundCheckRadius = 0.2f; // Radius untuk deteksi tanah
    [SerializeField] Vector3 groundCheckOffset; // Offset dari posisi karakter untuk deteksi tanah
    [SerializeField] LayerMask groundLayer; // Layer yang digunakan untuk menandai tanah

    bool isGrounded; // Apakah karakter berada di tanah

    private void Update()
    {
        // Melakukan deteksi tanah
        GroundCheck();
    }

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
    }
}
