using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform Player;

    void LateUpdate ()
    {
        Vector3 newPosition = Player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        // Hapus baris ini agar minimap tidak berputar mengikuti pandangan pemain
        // transform.rotation = Quaternion.Euler(90f, Player.eulerAngles.y, 0f);
    }
}
