using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hud_event : MonoBehaviour
{
    public GameObject weaponInfoPrefab;

    private void Start()
    {
        eventmanager.current.NewGunEvent.AddListener(CreateWeaponInfo);
    }

    public void CreateWeaponInfo()
    {
        Instantiate(weaponInfoPrefab, transform);
    }

}
