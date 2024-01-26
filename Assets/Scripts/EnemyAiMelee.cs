using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAiMelee : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform pathfindingTarget;
    [SerializeField] float detectionRange = 50;
    [SerializeField] float attackingRange = 3;

    AIDestinationSetter aiDestinationSetter;
    AIPath aiPath;
    bool canSeeTarget;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Rat").transform;
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        aiDestinationSetter.target = pathfindingTarget;

        aiPath = GetComponent<AIPath>();

        pathfindingTarget.position = transform.position;
        pathfindingTarget.SetParent(null);


    }


    private void FixedUpdate()
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;

        RaycastHit2D[] hitList = Physics2D.RaycastAll(transform.position, targetDirection * detectionRange);

        canSeeTarget = false;
        foreach (RaycastHit2D hit in hitList)
        {
            if (hit.collider.transform == target)
            {
                canSeeTarget = true;
                break;
            }
            else if (hit.collider.gameObject.layer == 9)
            {
                break;
            }
        }

        //if (canSeeTarget && (target.position - transform.position).sqrMagnitude <= attackingRange * attackingRange)
        //{
        //    ManageWeapon();
        //}

        if (canSeeTarget)
        {
            Debug.DrawRay(transform.position, targetDirection * detectionRange, new Color(1, 0, 0));
            pathfindingTarget.position = target.position;
        }
        else
        {
            Debug.DrawRay(transform.position, targetDirection * detectionRange, new Color(0.1f, 0.1f, 0.1f));
        }

        transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = aiPath.velocity.x < 0;

    }

    //private void ManageWeapon()
    //{
    //    weapon.RotateTowardPoint(target.position);
    //    weapon.Shoot();
    //}

    private void OnDestroy()
    {
        EnemyEvents.scoreAdvancedEvent.Invoke(Random.Range(9, 13) * 10);
    }

    public void SetDamaged()
    {
        aiPath.enabled = false;
        Invoke("ChangeActive", 1);
    }

    private void ChangeActive()
    {
        aiPath.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == target)
        {
            target.GetComponent<HPController>().TakeDamage(20);
            target.GetComponent<Rigidbody2D>().AddForce((target.position - transform.position).normalized * 1000);
        }
    }
}
