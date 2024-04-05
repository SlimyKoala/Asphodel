using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    List<GameObject> weaponList;
    int weaponIdx;
    [SerializeField] GameObject weaponHolder;
    Shooting currentGun;
    int weaponCount;

    void Start()
    {
        weaponCount = weaponHolder.transform.childCount;
        weaponList = new List<GameObject>();

        for (int i = 0; i < weaponCount; i++)
        {
            weaponList.Add(weaponHolder.transform.GetChild(i).gameObject);
            weaponList[i].SetActive(false);
        }

        weaponIdx = 0;
        SetWeapon(0);

    }

    private void Update()
    {
        if (PauseMenu.isGamePaused)
        {
            return;
        }
        Vector2 mousePointIngame = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        currentGun.RotateTowardPoint(mousePointIngame);
        if (Input.GetButton("Fire1"))
        {
            bool result = currentGun.Shoot();
            if (result)
            {
                EnemyEvents.fireEvent.Invoke(transform.position);
            }
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

    public void AddWeapon(GameObject gameObject, bool setAsCurrent=false)
    {
        weaponList.Add(gameObject);
        gameObject.transform.parent = weaponHolder.transform;
        weaponCount++;
        gameObject.transform.localPosition = new(0, 0, 0);

        if (setAsCurrent)
        {
            SetWeapon(weaponCount - 1);
        }
        else
        {
            weaponList[weaponCount - 1].SetActive(false);
        }
    }


}
