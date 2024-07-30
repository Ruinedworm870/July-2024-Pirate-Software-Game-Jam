using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldPowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController p = other.gameObject.GetComponent<PlayerController>();
        
        if(p != null)
        {
            UISoundController.Instance.PlayPickupSound();
            p.ApplyShieldPowerup();
            ShieldPowerupPool.Instance.ReturnShield(gameObject);
        }
    }
}
