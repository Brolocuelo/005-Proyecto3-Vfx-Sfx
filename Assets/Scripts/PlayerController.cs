using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float jumpForce = 10f;
    private bool isOnTheGround = true;
    public bool gameOver;

    private Animator _animator;
    public float gravityModifier = 1.5f;
    private float randomVal;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround && !gameOver)
        {
            isOnTheGround = false;
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _animator.SetTrigger("Jump_trig");
        }
    }

    private void GameOver()
    {
        gameOver = true;
        if (gameOver = true)
        {
            _animator.SetBool("DeathType_b", true);
        }
        _animator.SetInteger("DeathType_int",1);
    }
    private void OnCollisionEnter(Collision otherCollider)
    {
        isOnTheGround = true;
        if (otherCollider.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
        else if (otherCollider.gameObject.CompareTag("Ground"))
        {
            isOnTheGround = true;
        }
    }
}
