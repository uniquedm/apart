using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator anim;
    public Transform cam;
    public ParticleSystem boostFX;

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
    bool canBoost = false;

    [Header("SFX")]
    CharacterSound playerSound;
    public float footstepLength = 2f;
    AudioSource sfxSource;
    public AudioClip boostSFX;

    // Start is called before the first frame update
    void Start()
    {
        baseGravity = gravity;
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        playerSound = gameObject.GetComponent<CharacterSound>();
        sfxSource = GetComponent<AudioSource>();
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
        canBoost = isGrounded;
    }

    void Boost()
    {
        if (Input.GetButton("Jump"))
        {
            if (!boostFX.isPlaying && canBoost)
            {
                canBoost = false;
                boostFX.Play();
                gravity = baseGravity + airBoost;
                sfxSource.PlayOneShot(boostSFX);
                StartCoroutine("boosting");
            }
        }
        else
        {
            if (boostFX.isPlaying)
            {
                boostFX.Stop();
                gravity = baseGravity;
                sfxSource.Stop();
                StopCoroutine("boosting");
            }
        }
    }

    IEnumerator boosting()
    {
        for (float i = boostSFX.length; i >= 0; i-= 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        if (boostFX.isPlaying)
        {
            boostFX.Stop();
            gravity = baseGravity;
            sfxSource.Stop();
        }
    }

    private void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            playerSound.Footsteps(GroundType.Grass, footstepLength);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            playerSound.Footsteps(GroundType.Grass, footstepLength*0.85f);
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
            playerSound.JumpSound();
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
