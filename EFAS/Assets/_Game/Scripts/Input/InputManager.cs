using System;
using System.Collections;
using System.Collections.Generic;
using EasyCharacterMovement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    [SerializeField] private Character _character;

    [Header("Input modules")] 
    public FloatingJoystick moveJoystick;

    [Header("Player Input values")] 
    private Vector2 move;

    [Header("Player check values")] 
    public bool jumpBtn = false;
    public bool runBtnDown = false;
    public bool runBtnUp = false;

    public Vector2 Move => move;

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
        yield return new WaitForSeconds(0.2f);
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
    
    public void MoveHandler()
    {
        Debug.Log("Call move");
        Vector2 moveInput = move;

        // Tính toán hướng xoay dựa trên input của joystick
        float targetRotation = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg +
                               CameraController.Instance.MainCamera.transform.eulerAngles.y;
        Quaternion targetRotationQuaternion = Quaternion.Euler(0f, targetRotation, 0f);

        // Xoay đối tượng nhân vật
        CameraController.Instance.PlayerRotationObj.transform.rotation = Quaternion.Slerp(
            CameraController.Instance.PlayerRotationObj.transform.rotation,
            targetRotationQuaternion,
            Time.deltaTime * 10
        );

        // Tính toán hướng di chuyển
        //Vector3 targetDir = targetRotationQuaternion * Vector3.forward;
        Vector3 cameraForward = CameraController.Instance.MainCamera.transform.forward;
        Vector3 cameraRight = CameraController.Instance.MainCamera.transform.right;

        // Loại bỏ thành phần y để tránh di chuyển lên xuống
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Chuẩn hóa vector để đảm bảo nó không ảnh hưởng đến tốc độ di chuyển
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Tính toán hướng di chuyển mới dựa trên hướng của camera và input từ joystick
        Vector3 targetDir = cameraForward * moveInput.y + cameraRight * moveInput.x;

        _character.SetMovementDirection(targetDir);
    }
}