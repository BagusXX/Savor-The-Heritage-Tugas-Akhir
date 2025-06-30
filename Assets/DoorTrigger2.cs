using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger2 : MonoBehaviour
{
    Animator _doorAnim;

    private void OnTriggerEnter(Collider other)
    {
        _doorAnim.SetBool("IsOpening", true);
    }

    private void OnTriggerExit(Collider other)
    {
        _doorAnim.SetBool("IsOpening", false);
    }

    void Start()
    {
        _doorAnim = this.transform.parent.GetComponent<Animator>();
    }
}
