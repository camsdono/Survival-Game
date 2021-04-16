using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class plr_Inventory : MonoBehaviour
{
    public plr_Movement Movement;
    private InputSystem controls;
    public TextMeshProUGUI health;
    private string healthA;
    private bool isInventoryActive;
    public GameObject inventoryPanel;
    
    
    void Awake()
    {
        controls = new InputSystem();
        controls.Enable();

        controls.Player.Inventory.performed += e => ToggleInventory();
    }

    
    void Update()
    {
        healthA = Movement.playerHealth.ToString();
        health.text = healthA;

        if (isInventoryActive)
        {
            inventoryPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }

        if (!isInventoryActive)
        {
            inventoryPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void ToggleInventory()
    {
        if (!isInventoryActive)
        {
            isInventoryActive = true;
            return;
        }
        
        isInventoryActive = !isInventoryActive;
    }
}
