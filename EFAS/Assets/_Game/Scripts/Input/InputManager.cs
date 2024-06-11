using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    
    [Header("Input modules")]
    public FixedJoystick moveJoystick;
    
    [Header("Player Input values")] 
    private Vector2 move;

    [Header("Player check values")] 
    //public bool isReadyToJump = false;
    public bool jumpBtn = false;
    public bool isJumping = false;
    public bool runBtnDown = false;
    public bool runBtnHold = false;
    public bool runBtnUp = false;

    public Vector2 Move { get => move; }

    public Vector2 look;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void JumpBtnDown1()
    {
        StartCoroutine(SetJumping());
    }

    private IEnumerator SetJumping()
    {
        jumpBtn = true;
        //isJumping = true;
        yield return null; 
        //isJumping = false;
        jumpBtn = false;
    }
    
    public void RunBtnDown()
    {
        runBtnDown = true;
        runBtnHold = false;
        runBtnUp = false;
    }
    public void RunBtnUp()
    {   
        runBtnDown = false;
        runBtnHold = false;
        runBtnUp = true;
    }

    public bool IsMoving()
    {
        return (move != Vector2.zero);
    }
    
    public bool IsOnAir()
    {
        return !PlayerController.Instance.IsGround();
    }

    private void Update()
    {
        move = new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical);
    }
}
