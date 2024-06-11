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
    public bool jumpBtn = false;
    public bool runBtnDown = false;
    public bool runBtnUp = false;

    public Vector2 Move => move;

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
        yield return null;
        jumpBtn = false;
    }

    public void RunBtnDown()
    {
        runBtnDown = true;
        runBtnUp = false;
    }

    public void RunBtnUp()
    {
        runBtnDown = false;
        runBtnUp = true;
    }

    public bool IsMoving()
    {
        return (move != Vector2.zero);
    }

    private void Update()
    {
        move = new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical);
    }
}