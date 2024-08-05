using System.Collections;
using System.Collections.Generic;
using Tayx.Graphy.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

public class SGPlayerController : MonoBehaviour
{
 

    private Animator myanim;
    private CharacterController mychar;
    private Vector3 move = Vector3.zero;
    private bool isAttacking = false;

    public Transform cam;

    float speed;
    public float runspeed;
    public float walkspeed;
    public float gravity;
    public float jumpHeight;

    Vector3 velocity;
    bool isGrounded;
    bool isDead;
    bool recentlyJumped;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;



    Vector3 moveDirection;




    void Start()
    {
        myanim = GetComponent<Animator>();
        mychar = GetComponent<CharacterController>();
        isDead = false;
        isAttacking = false;
        speed = walkspeed;
        // Hide the cursor
        Cursor.visible = false;
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        recentlyJumped = false;

    }

    public void PlayLeftSound()
    {
        SGSound.instance.PlayleftStepAudio();
    }

    public void PlayRightSound()
    {
        SGSound.instance.PlayrightStepAudio();
    }


    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (!isDead)
        {

            

            /*
             if (Input.GetMouseButton(0)) 
             {
                 myanim.SetBool("isWeakAttack", true);
             }
             else
             {
                 myanim.SetBool("isWeakAttack", false);
             }

             */
            if (Input.GetMouseButtonDown(0))
            {
                isAttacking = true;
                myanim.SetTrigger("WeakAttack");
            }
            if (Input.GetMouseButtonDown(1))
            {
                isAttacking = true;
                myanim.SetTrigger("StrongAttack");
            }




            if (myanim.GetCurrentAnimatorStateInfo(0).IsName("Weak Attack") || myanim.GetCurrentAnimatorStateInfo(0).IsName("Strong Attack"))
            {
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
            }



            if (vertical != 0 || horizontal != 0)
            {
                if (!isAttacking && isGrounded)
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        speed = runspeed;
                        myanim.SetBool("isRunning", true);
                        myanim.SetBool("isWalking", false);

                    }
                    else
                    {
                        speed = walkspeed;
                        myanim.SetBool("isWalking", true);
                        myanim.SetBool("isRunning", false);
                    }
                }


            }
            else
            {
                myanim.SetBool("isWalking", false);
                myanim.SetBool("isRunning", false);
            }

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


           
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;

            }
           


            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                myanim.SetBool("isJumping", true);
                SGSound.instance.PlayjumpAudio();
                recentlyJumped = true;
  
            }
            else
            {
                
                if (isGrounded && recentlyJumped)
                {
                   myanim.SetBool("isJumping", false);
                   SGSound.instance.PlaylandAudio();
                   recentlyJumped = false;
                }
                
            }
 

            //gravity
            velocity.y += gravity * Time.deltaTime;
            mychar.Move(velocity * Time.deltaTime);


            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


 


            if (direction.magnitude >= 0.1f && isAttacking == false)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                mychar.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            isAttacking = false;
        }
    }
}
