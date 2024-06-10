using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    
    [Header("Input modules")]
    public FixedJoystick moveJoystick;
    public GameObject lookPanel;
    
    [Header("Player Input values")] 
    private Vector2 move;

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

    private void Update()
    {
        move = new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical);
    }
}
