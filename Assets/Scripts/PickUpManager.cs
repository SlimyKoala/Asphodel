using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    WeaponManager weaponManager;


    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pick(PickableItem item)
    {
        weaponManager.AddWeapon(item.gameObject, true);
        Debug.Log("Funkcja Pick dzia³a");
    }
}
