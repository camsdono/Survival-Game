using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plr_Weapons : MonoBehaviour
{
    private InputSystem controls;
    public Camera playerCam;
    public float damage;
    public float range;
    public plr_Resources resources;
    public plr_WeaponTypes weaponType;
    public GameObject glock19;
    public GameObject axe;
    public GameObject hands;
    

    void Awake()
    {
        controls = new InputSystem();
        controls.Player.Shoot.performed += e => Shoot();
        
        controls.Enable();
        
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            plr_Target target = hit.transform.GetComponent<plr_Target>();
            if (target != null)
            {
                if (target.TargetType == plr_TargetType.HARVASTABLE && weaponType == plr_WeaponTypes.AXE && glock19.activeSelf == false)
                {
                    resources.GiveWood(1);
                    target.TakeDamage(damage);
                }

                if (target.TargetType == plr_TargetType.ENEMY && weaponType == plr_WeaponTypes.GUN && axe.activeSelf == false)
                {
                    target.TakeDamage(damage);
                }
                else if (target.TargetType == plr_TargetType.ENEMY && weaponType == plr_WeaponTypes.MELEE && axe.activeSelf == false )
                {
                    target.TakeDamage(damage);
                }
            }
        }
    }
    
}
