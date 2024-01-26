using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    private Rigidbody2D rb;

    [SerializeField] float lifetime = 3;
    [SerializeField] float damage = 2;

    [SerializeField] bool isExplosive = false;
    [SerializeField] float explosionRange = 8f;
    [SerializeField] float explosionBaseForce = 4000;
    [SerializeField] float explosionDamageMult = 0.05f;

    private GameObject owner;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;

        Invoke("DestroyBullet", lifetime);
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HPController controller = collision.transform.GetComponent<HPController>();
        if(controller != null)
        {
            HitEventData hitEventData = new(owner, controller.gameObject, gameObject);
            EnemyEvents.hitEvent.Invoke(hitEventData);
        }

        if (isExplosive)
        {
            Vector2 explosionCenter = transform.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionCenter, explosionRange);
            foreach(Collider2D collider in colliders)
            {
                Rigidbody2D rb = collider.GetComponentInParent<Rigidbody2D>();
                if(rb != null)
                {
                    Vector2 distVector = (Vector2)collider.transform.position - explosionCenter;
                    if(distVector.magnitude > 0)
                    {
                        EnemyAI enemyAI = collider.GetComponentInParent<EnemyAI>();
                        if(enemyAI != null)
                        {
                            enemyAI.SetDamaged();
                        }
                        
                        float explosionForce = explosionBaseForce / distVector.magnitude;
                        rb.AddForce(distVector.normalized * explosionForce);
                        
                        controller = collider.transform.GetComponentInParent<HPController>();
                        if (controller != null)
                        {
                            controller.TakeDamage(explosionForce * explosionDamageMult);
                        }
                    }
                }
            }
        }
        
        Destroy(gameObject);
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }
}
