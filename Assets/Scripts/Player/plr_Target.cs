using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plr_Target : MonoBehaviour
{
    public float health = 50f;
    public plr_TargetType TargetType;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}