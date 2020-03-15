using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator Animator;

    public float walkSpeed = 20f;
    public float runSpeed = 25f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    //Change friction
    public Rigidbody2D player; 
    public PhysicsMaterial2D withFricton;
    public PhysicsMaterial2D noFriction;
    //Variables for running
    private bool runNow = false; //Main variable to be used
    public int noOfTapes = 2; //No of taps
    public float tapDelay = 0.5f; //Tap delay
    private int movePressed = 0; //No of times d has been pressed in tapDelay

    // Update is called once per frame
    void Update()
    {
        //Set walking speed
                //  Change to --> CrossPlatformInputManager.GetAxis("Horizontal")
        horizontalMove = CrossPlatformInputManager.GetAxisRaw("Horizontal") * walkSpeed;

        //Detecting double taps on Movement buttons
        if (Input.GetButtonDown("Horizontal") || CrossPlatformInputManager.GetButtonDown("Horizontal"))
        {
            movePressed += 1;
            Invoke("SetMovePressedToZero", tapDelay);
            player.sharedMaterial = noFriction;
        }

        if (Input.GetButtonUp("Horizontal") || CrossPlatformInputManager.GetButtonUp("Horizontal"))
        {
            player.sharedMaterial = withFricton;
        }

        if (movePressed > noOfTapes - 1)
        {
            runNow = true;   
        }
        if ((Input.GetButtonUp("Horizontal") || CrossPlatformInputManager.GetButtonUp("Horizontal")) && runNow)
        {
            SetMovePressedToZero();
            SetRunNowFalse();
        }
        //Running
                //Change to --> CrossPlatformInputManager.GetAxis("Horizontal")
        if (runNow)
        {
            horizontalMove = CrossPlatformInputManager.GetAxis("Horizontal") * runSpeed;
        }
        //Jumping
        if (CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetButtonDown("Jump"))
            {
                Animator.SetBool("Isjumping", true);
                jump = true;
            }
        //Crouching
        if (CrossPlatformInputManager.GetButtonDown("Crouch") || Input.GetButtonDown("Crouch"))
            {
                Animator.SetBool("IsCrouching", true);
                crouch = true;
            }
         else if (CrossPlatformInputManager.GetButtonUp("Crouch") || Input.GetButtonUp("Crouch"))
            {
                Animator.SetBool("IsCrouching", false);
                crouch = false;
            }
         Animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }
    public void onLanding()
    {
        Animator.SetBool("Isjumping", false);
    }
    private void FixedUpdate()
    {
        //Move our player
        controller.Move(horizontalMove * Time.fixedDeltaTime , crouch, jump);
        jump = false;
    }
    void SetMovePressedToZero()
    {
        movePressed = 0;
    }

    void SetRunNowFalse()
    {
        runNow = false;
    }
}
