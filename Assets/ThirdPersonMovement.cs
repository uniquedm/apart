using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator anim;
    public Transform cam;
    public ParticleSystem particleSystem;

    public float speed = 3f;
    public float runBoost = 3f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float gravity = -9.8f;
    float baseGravity;
    public float airBoost = 2f;
    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

    public float jumpHeight = 2f;

    // Start is called before the first frame update
    void Start()
    {
        baseGravity = gravity;
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        Move();
        Boost();
    }

    void IsGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        anim.SetBool("On Ground", isGrounded);
    }

    void Boost()
    {
        if (Input.GetButton("Jump"))
        {
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
                gravity = baseGravity + airBoost;
            }
        }
        else
        {
            if (particleSystem.isPlaying)
            {
                particleSystem.Stop();
                gravity = baseGravity;
            }
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveDirection = Vector3.zero;
        if (direction.magnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterController.Move(moveDirection.normalized * (speed + runBoost) * Time.deltaTime);
        }
        else
        {
            characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        anim.SetFloat("Velocity", characterController.velocity.magnitude);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Gravity - Free Fall
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
