using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private float _speeed;
    private float _inputX;
    private float _inputY;

    private Vector2 _movementInput;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Movement();
    }


    private void PlayerInput()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        _inputY = Input.GetAxisRaw("Vertical");


        if (_inputX != 0 && _inputY != 0)
        {
            _inputX *= 0.71f;
            _inputY *= 0.71f;
        }
        
        _movementInput = new Vector2(_inputX, _inputY);
    }
    
    private void Movement()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _movementInput * _speeed * Time.deltaTime);
    }
}
