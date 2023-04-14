using System;
using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [Header("Knockback")]
    [SerializeField] private Transform _center;
    [SerializeField] private float _knockbackVel = 8f;
    [SerializeField] private float _knockbackTime = 1f;
    [SerializeField] private bool _knockbacked;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void KnockbackHit(Transform t)
    {
        var dir = _center.position - t.position;
        _knockbacked = true;
        _rigidbody2D.velocity = dir.normalized * _knockbackVel;
        StartCoroutine(Unknockback());
    }

    private IEnumerator Unknockback()
    {
        yield return new WaitForSeconds(_knockbackTime);
        _knockbacked = false;
    }

}
