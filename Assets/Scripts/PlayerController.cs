using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float jumpForce = 15f;
    private bool isOnTheGround = true;
    public bool gameOver;

    private Animator _animator;
    public float gravityMultiplier = 1.5f;

    public ParticleSystem particleExplosion;
    public ParticleSystem dirtSplatter;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        Physics.gravity *= gravityMultiplier;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround && !gameOver)
        {
            Jump();
        }
    }

    private void GameOver()
    {
        gameOver = true;
        _animator.SetBool("Death_b", true);
        _animator.SetInteger("DeathType_int", Random.Range(1,3));
        particleExplosion.Play();
        dirtSplatter.Stop();

    }
    private void OnCollisionEnter(Collision otherCollider)
    {
        isOnTheGround = true;
        if (otherCollider.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
            Destroy(otherCollider.gameObject);
        }
        else if (otherCollider.gameObject.CompareTag("Ground"))
        {
            isOnTheGround = true;
            dirtSplatter.Play();
        }
    }
    private void Jump()
    {
        isOnTheGround = false;
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        _animator.SetTrigger("Jump_trig");
        dirtSplatter.Stop();
    }
}
