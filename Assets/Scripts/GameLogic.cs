using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private void Awake()
    {
        EnemyEvents.hitEvent.AddListener(DoDamage);
    }

    void DoDamage(HitEventData data)
    {
        HPController victim = data.victim.GetComponent<HPController>();
        BulletMovement bullet = data.bullet.GetComponent<BulletMovement>();
        victim.TakeDamage(bullet.GetDamage());
    }
}
