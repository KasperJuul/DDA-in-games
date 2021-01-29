using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private float damage = 50f;

    private void Awake()
    {
        GetComponent<PlayerInput>().Onfire += HandleWeapon;
    }

    private void HandleWeapon()
    {
        
    }
}
