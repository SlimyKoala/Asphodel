using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    [SerializeField] float maxHp = 100;
    private float hp;
    private bool isDead;

    void Start()
    {
        hp = maxHp;
        isDead = false;
    }
    public void TakeDamage(float damage)
    {

        hp -= damage;
        AudioManager.instance.Play("damage");
        if (hp <= 0)
        {
            isDead = true;
            hp = 0;
            Destroy(gameObject);
        }
    }

    public void Heal(float hpToHeal)
    {
        hp += hpToHeal;
        hp = Mathf.Min(maxHp, hp);
    }

    public float GetHP()
    {
        return hp;
    }

    public float GetMaxHP()
    {
        return maxHp;
    }

    public float GetHPFraction()
    {
        return hp / maxHp;
    }
}
