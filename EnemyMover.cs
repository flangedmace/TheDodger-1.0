using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _minRotateSpeed;
    [SerializeField] private float _maxRotateSpeed;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _moveSpeed;

    private float _rotateSpeed;

    private void Start()
    {
        _rigidbody.velocity = new Vector2(0f, -_moveSpeed);

        _rotateSpeed = Random.Range(_minRotateSpeed, _maxRotateSpeed);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, _rotateSpeed * Time.deltaTime);
    }
}
