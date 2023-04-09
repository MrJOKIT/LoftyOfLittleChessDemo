using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropDown : MonoBehaviour
{
    private PlayerJump _playerJump;
    private PlayerDash _playerDash;
    private Rigidbody2D rb;
    private PlayerController _playerController;
    private LayerMask _layerMask;

    private void Start()
    {
        _playerDash = GetComponent<PlayerDash>();
        _playerJump = GetComponent<PlayerJump>();
        rb = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        DropDownSkill();
    }

    private void DropDownSkill()
    {
        if (!_playerJump.IsGround && _playerJump.IsJump && Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(DropDown());
        }
    }
    
    private IEnumerator DropDown()
    {
        
        _playerDash.CanDash = false;
        rb.gravityScale = 15f;
        yield return new WaitForSeconds(0.1f);
        /*if (_layerMask == LayerMask.GetMask("Ground"))
        {
            _playerController._cameraShake.ShakeCamera();
        }*/
        _playerDash.CanDash = true;
        rb.gravityScale = 2f;
        yield return new WaitForSeconds(0.05f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (((1 << col.gameObject.layer) & LayerMask.GetMask("Ground")) != 0)
        {
            _layerMask = LayerMask.GetMask("Ground");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (((1 << other.gameObject.layer) & LayerMask.GetMask("Ground")) != 0)
        {
            _layerMask = LayerMask.GetMask("default");
        }
    }
}
