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
    public GameObject lookPanel;
    
    [Header("Player Input values")] 
    private Vector2 move;

    //public bool isReadyToJump = false;
    public bool jumpBtn = false;
    public bool isJumping = false;

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

    public bool IsMoving()
    {
        return (move.magnitude > 0f);
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
