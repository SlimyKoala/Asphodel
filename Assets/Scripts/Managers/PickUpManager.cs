using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    WeaponManager weaponManager;
    HPController hpController;


    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        hpController = GetComponent<HPController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pick(PickableItem item, string type, float other)
    {
        if (type == "weapon")
            weaponManager.AddWeapon(item.gameObject, true);

        else
            hpController.Heal(other);


    }
}
