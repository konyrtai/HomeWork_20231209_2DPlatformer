using System;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField]  private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D _body;
    private BoxCollider2D _boxCollider;

    #region Animator

    private Animator _animator;

    private int _runHash;
    private int _groundedHash;
    private int _jumpHash;

    #endregion

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();

        _runHash = Animator.StringToHash("run");
        _groundedHash = Animator.StringToHash("grounded");
        _jumpHash = Animator.StringToHash("jump");
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");

        UpdateDirection(horizontalInput); // повернуть в направлении

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _body.transform.position += transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _body.transform.position += transform.right * -speed * Time.deltaTime;

        }


        //_body.velocity = new Vector2(horizontalInput * speed, _body.velocity.y); // движение

        if(Input.GetKey(KeyCode.Space))
            Jump();

        UpdateAnimation(horizontalInput);

        print(IsOnWall());
    }

    private void Jump()
    {
        if(IsGrounded() == false)
            return;

        _body.velocity = new Vector2(_body.velocity.x, 10);
        _animator.SetTrigger(_jumpHash);
    }


    private void UpdateAnimation(float horizontalInput)
    {
        _animator.SetBool(_runHash, horizontalInput != 0 && IsGrounded());
        _animator.SetBool(_groundedHash, IsGrounded());
    }

    private void UpdateDirection(float horizontalInput)
    {
        if (horizontalInput > 0.01)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private bool IsGrounded()
    {
        var raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0,
            Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool IsOnWall()
    {
        var raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0,
            new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
