using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Action.PickFruit;
using _Game.Scripts.Inventory.UI_Scripts;
using Animancer;
using UnityEngine;
using UnityEngine.Serialization;

public class BlackBoard : MonoBehaviour
{
    [Header("Script")]
    public PlayerMovement playerMovement;
    public CameraControl cameraControl;
    public AnimancerComponent animancer;
    public PreviewHolder PreviewHolder;
    public InventoryManager InventoryManager;
    public InPickFruitRange PickFruitRange;
    public UIManager UIManager;
    [Header("Object")]
    public new Camera camera;
    [Header("Bool Value")]
    public bool jump;
    public bool sprint;
    public bool isFishing;
    public bool isPicking;
    public bool isGrounded;
    [Header("Component")]
    public LayerMask groundLayer;
    public AvatarMask _carryMask;
    public new Rigidbody rigidbody;
    public Vector3 moveDirection;
    public Transform fishingTarget;
    
}
