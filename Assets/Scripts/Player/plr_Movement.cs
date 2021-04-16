using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static scr_Models;
using UnityEngine.UI;


public class plr_Movement : MonoBehaviour
{
    private CharacterController characterController;
    private InputSystem controls;
    public Vector2 input_Movement;
    public Vector2 input_View;

    private Vector3 newCameraRotation;
    private Vector3 newCharacterRotation;

    [Header("Refrences")]
    public Transform cameraHolder;

    [Header("Settings")]
    public PlayerSettingsModel playerSettings;
    public float viewClampYMin = -70;
    public float viewClampYMax = 80;
    private float currentSensitivityX;
    private float currentSensitivityY;

    [Header("Gravity")]
    public float gravityAmount;
    private float playerGravity;
    public float gravityMin;

    public Vector3 jumpingForce;
    private Vector3 jumpingForceVelocity;

    private bool isSprinting;

    [Header("Stamina")]
    public float stamina = 100f;
    public float maxStamina = 100f;
    public Slider staminaBar;
    public float staminaReganTime;
    public float staminaTakeAway;
    [Header("Health")]
    public float playerHealth = 100f;
    public float maxPlayerHealth = 100f;
    public Slider healthBar;
    public GameObject healthBarSlider;
    
    
    
    private void Awake()
    {
        controls = new InputSystem();

        controls.Player.Movement.performed += e => input_Movement = e.ReadValue<Vector2>();
        controls.Player.View.performed += e => input_View = e.ReadValue<Vector2>();
        controls.Player.Jump.performed += e => Jump();
        controls.Player.Sprint.performed += e => ToggleSprint();
        
        
        controls.Enable();

        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;

        Cursor.lockState = CursorLockMode.Locked;

        characterController = GetComponent<CharacterController>();
        
    }

    private void Update()
    {
        CalculateView();
        CalculateMovement();
        CalculateJump();
        CurrentGamepd();
    }

    private void CalculateView()
    {
        newCharacterRotation.y += currentSensitivityX * (playerSettings.ViewXInverted ? -input_View.x : input_View.x) * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(newCharacterRotation);
        
        newCameraRotation.x += currentSensitivityY * (playerSettings.ViewYInverted ? input_View.y : -input_View.y) * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClampYMin, viewClampYMax);
        
        cameraHolder.localRotation = Quaternion.Euler(newCameraRotation);
    }

    private void CalculateMovement()
    {
        if (input_Movement.y <= 0.2f)
        {
            isSprinting = false;
        }

        if (stamina <= 0)
        {
            isSprinting = false;
        }

        var verticalSpeed = playerSettings.WalkingForwardSpeed;
        var horizontalSpeed = playerSettings.WalkingStrafeSpeed;

        if (isSprinting && stamina > 0)
        {
            verticalSpeed = playerSettings.RunningForwardSpeed;
            horizontalSpeed = playerSettings.RunningStrafeSpeed;
            stamina -= staminaTakeAway * Time.deltaTime;
        }

        if (!isSprinting && stamina <= maxStamina)
        {
            stamina += staminaReganTime * Time.deltaTime;
        }

        staminaBar.value = stamina;

        healthBar.value = playerHealth;

        if (playerHealth == 0)
        {
            healthBarSlider.SetActive(false);
        }
        else
        {
            healthBarSlider.SetActive(true);
        }

        var newMovementSpeed = new Vector3(horizontalSpeed * input_Movement.x * Time.deltaTime, 0, verticalSpeed * input_Movement.y * Time.deltaTime);

        newMovementSpeed = transform.TransformDirection(newMovementSpeed);

        if (playerGravity > gravityMin && jumpingForce.y < 0.1f)
        {
            playerGravity -= gravityAmount * Time.deltaTime;
        }

        if (playerGravity < -1 && characterController.isGrounded)
        {
            playerGravity = -1;
        }

        if (jumpingForce.y > 0.1f)
        {
            playerGravity = 0;
        }

        newMovementSpeed.y += playerGravity;

        newMovementSpeed += jumpingForce;
        
        characterController.Move(newMovementSpeed);
    }

    private void CalculateJump()
    {
        jumpingForce = Vector3.SmoothDamp(jumpingForce, Vector3.zero, ref jumpingForceVelocity, playerSettings.JumpingFallOff);
    }

    private void Jump()
    {
        if (!characterController.isGrounded)
        {
            return;
        }

        jumpingForce = Vector3.up * playerSettings.JumpingHeight;
    }

    private void ToggleSprint()
    {
        if (input_Movement.y <= 0.2f)
        {
            isSprinting = false;
            return;
        }
        
        isSprinting = !isSprinting;
    }
    
    
    private void CurrentGamepd()
    {
        var gamePad = Gamepad.current;

        if (gamePad == null)
        {
            currentSensitivityY = playerSettings.ViewYSensitivityM;
            currentSensitivityX = playerSettings.ViewXSensitivityM;
        }
        else
        {
            currentSensitivityY = playerSettings.ViewYSensitivityC;
            currentSensitivityX = playerSettings.ViewXSensitivityC;
        }
    }

}
