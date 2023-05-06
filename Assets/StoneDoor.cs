using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDoor : MonoBehaviour
{
    public void ColliderOff()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.enabled = false;
    }
}
