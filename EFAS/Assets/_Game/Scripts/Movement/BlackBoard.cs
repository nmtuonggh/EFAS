using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class BlackBoard : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public CameraControl cameraControl;
    public AnimancerComponent animancer;
    public new Camera camera;
    public new Rigidbody rigidbody;
    public Vector3 moveDirection;
    public bool jump;
    public bool sprint;
    public bool isGrounded;
    public LayerMask groundLayer;
    public AvatarMask _carryMask;
    public PreviewHolder PreviewHolder;
    public InventoryManager InventoryManager;
}
