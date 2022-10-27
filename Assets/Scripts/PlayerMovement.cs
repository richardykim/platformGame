using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Animator animator;
    private Rigidbody rigidbodyComponent;
    private SpriteRenderer spriteRenderer;
    private bool touchingIce = false;
    [SerializeField] private float jumpPower = 7;
    [SerializeField] private float maxSpeed = 2;

    
    [SerializeField] private AudioSource jumpSoundEffect;


    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            jumpKeyWasPressed = true;
        }
        horizontalInput = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        AnimateMovement();
        if (touchingIce) {
            rigidbodyComponent.AddForce(new Vector3(horizontalInput*maxSpeed*0.8f, 0, 0));
        } else {
            rigidbodyComponent.velocity = new Vector3(horizontalInput*maxSpeed, rigidbodyComponent.velocity.y, 0);
        }

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length == 1) {
            return;
        }

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length > 1) {
            animator.SetBool("inAir", false);
        }

        if (jumpKeyWasPressed) {
            jumpSoundEffect.Play();
            animator.SetBool("inAir", true);
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
    }

    private void AnimateMovement()
    {
        if (horizontalInput > 0) {
            spriteRenderer.flipX = false;
            animator.SetBool("isMoving", true);
        } else if (horizontalInput < 0) {
            spriteRenderer.flipX = true;
            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ice") 
        {
            touchingIce = true;
        } else 
        {
            touchingIce = false;
        }
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Ice") 
        {
            touchingIce = true;
        } else 
        {
            touchingIce = false;
        }
    }

}
