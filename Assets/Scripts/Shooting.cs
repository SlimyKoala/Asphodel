using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float firerate = 3;

    [Range(0, 90)]
    [SerializeField] float bulletSpread = 0f;

    [Range(1,100)]
    [SerializeField] int bulletAmount = 1; 
    private Transform bulletSpawner;

    private bool canFire;
    private float lastTimeFired;

    private void Start()
    {
        canFire = true;
        lastTimeFired = Time.time;

        bulletSpawner = transform.GetChild(0);
    }
    
    public bool Shoot()
    {
        if(Time.time - lastTimeFired > 1 / firerate)
        {
            canFire = true;
        }
        
        if (canFire)
        {
            for (int i = 0; i<bulletAmount; i++)
            {
                GameObject tempBullet = Instantiate(bullet, bulletSpawner.position, transform.rotation);
                tempBullet.GetComponent<BulletMovement>().SetOwner(gameObject);
                ApplyBulletSpread(tempBullet);
            }

            AudioManager.instance.Play("shot");
            canFire = false;
            lastTimeFired = Time.time;
            return true;
        }
        return false;

    }

    public void RotateTowardPoint(Vector2 point)
    {
        Vector2 directionVector = point - (Vector2)transform.position;
        float angle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void ApplyBulletSpread(GameObject bullet)
    {
        float z = Random.Range(-bulletSpread / 2, bulletSpread / 2);
        bullet.transform.Rotate(new Vector3(0, 0, z));
    }
}
