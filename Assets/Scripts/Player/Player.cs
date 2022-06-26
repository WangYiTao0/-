using System;
using UnityEngine;

namespace FarmGame
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        [SerializeField] private float _speeed;
        private float _inputX;
        private float _inputY;

        private Vector2 _movementInput;

        [Header("动画")] private Animator[] _animators;
        private bool _isMoving;
        private static readonly int InputX = Animator.StringToHash("InputX");
        private static readonly int InputY = Animator.StringToHash("InputY");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animators = GetComponentsInChildren<Animator>();
        }

        private void Update()
        {
            PlayerInput();
            SwitchAnimation();
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

            //Shift 走路
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _inputX *= 0.5f;
                _inputY *= 0.5f;
            }
            _isMoving = _movementInput != Vector2.zero;
        }

        private void Movement()
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + _movementInput * _speeed * Time.deltaTime);
        }

        void SwitchAnimation()
        {
            foreach (var animator in _animators)
            {
                animator.SetBool(IsMoving,_isMoving);
                if (_isMoving)
                {
                    animator.SetFloat(InputX,_inputX);
                    animator.SetFloat(InputY,_inputY);
                }
            }
        }
    }
}
