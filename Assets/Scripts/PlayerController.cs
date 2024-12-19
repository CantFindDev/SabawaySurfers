using System;
using System.Collections.Generic;
using DG.Tweening;
using PurrNet;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField] private int CurrentPlayerLane = 1;
    [SerializeField] private float PlayerMoveDuration = .2f;
    [SerializeField] private float PlayerJumpHeight = 5;
    [SerializeField] private float PlayerFallMultiplier = 2.5f;
    
    private float Gravity;
    private bool IsGrounded;
    private float PlayerVelocityY;

    private PlayerInput playerInput;
    private InputAction TurnAction;
    private InputAction JumpAction;
    private InputAction SlideAction;

    public List<Transform> PlayerPositions = new List<Transform>();

    private Rigidbody RB;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        TurnAction = playerInput.actions["Turn"];
        JumpAction = playerInput.actions["Jump"];
        SlideAction = playerInput.actions["Slide"];
    }

    private void FixedUpdate()
    {
        if (RB.linearVelocity.y < 0)
        {
            RB.linearVelocity += Vector3.up * Physics.gravity.y * PlayerFallMultiplier * Time.deltaTime;
        }
    }
    
    public void SwichPlayerLane(int LaneDirection)
    {
        if (!IsAvaliableToMove()) return;
        CurrentPlayerLane = Mathf.Clamp(CurrentPlayerLane + LaneDirection, 0, 2);
        transform.DOMoveX(PlayerPositions[CurrentPlayerLane].position.x,PlayerMoveDuration).SetEase(Ease.InSine);
    }

    public void PlayerJump()
    {
        if (IsGrounded && IsAvaliableToMove()) RB.AddForce(Vector3.up * PlayerJumpHeight, ForceMode.VelocityChange);
    }

    public void PlayerSlide()
    {
        if (!IsGrounded && IsAvaliableToMove()) RB.AddForce(Vector3.down * PlayerJumpHeight, ForceMode.VelocityChange);
    }
    
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
    }
    
    public void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }

    private bool IsAvaliableToMove()
    {
        return GameManager.Instance.GameStarted;
    }

    private void OnEnable()
    {
      TurnAction.performed += ctx => SwichPlayerLane((int)ctx.ReadValue<float>());
      JumpAction.performed += ctx=> PlayerJump();
      SlideAction.performed += ctx => PlayerSlide();

    }
    
    private void OnDisable()
    {
        TurnAction.performed -= ctx => SwichPlayerLane((int)ctx.ReadValue<float>());
        JumpAction.performed -= ctx=> PlayerJump();
        SlideAction.performed -= ctx => PlayerSlide();
    }
}
