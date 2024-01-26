using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    GameObject[] weaponList;
    int weaponIdx;
    [SerializeField] GameObject weaponHolder;
    Shooting currentGun;
    int weaponCount;

    void Start()
    {
        weaponCount = weaponHolder.transform.childCount;
        weaponList = new GameObject[weaponCount];

        for (int i = 0; i < weaponCount; i++)
        {
            weaponList[i] = weaponHolder.transform.GetChild(i).gameObject;
            weaponList[i].SetActive(false);
        }

        weaponIdx = 0;
        SetWeapon(0);

    }

    private void Update()
    {
        Vector2 mousePointIngame = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        currentGun.RotateTowardPoint(mousePointIngame);
        if (Input.GetButton("Fire1"))
        {
            currentGun.Shoot();
        }

        
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetWeapon((weaponIdx + 1) % weaponCount);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetWeapon((weaponIdx - 1 + weaponCount) % weaponCount);
        }
    }

    private void SetWeapon(int newWeaponIdx)
    {
        weaponList[weaponIdx].SetActive(false);
        weaponIdx = newWeaponIdx;
        weaponList[weaponIdx].SetActive(true);
        currentGun = weaponList[weaponIdx].GetComponent<Shooting>();
    }


}
