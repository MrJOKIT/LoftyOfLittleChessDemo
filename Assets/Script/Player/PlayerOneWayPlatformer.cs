using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatformer : MonoBehaviour
{
    private GameObject currentOneWayPlatformer;
    [SerializeField] private BoxCollider2D playerCollider;

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentOneWayPlatformer != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatformer = col.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatformer = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatformer.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider,platformCollider);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(playerCollider,platformCollider,false);
    }
}
