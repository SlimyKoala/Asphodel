using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform pathfindingTarget;
    [SerializeField] float detectionRange = 50;
    [SerializeField] float firingRange = 20;

    AIDestinationSetter aiDestinationSetter;
    AIPath aiPath;

    [SerializeField] Shooting weapon;

    bool canSeeTarget;

    private void Awake()
    {
        EnemyEvents.fireEvent.AddListener(ListenToTarget);
    }

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

        if (canSeeTarget && (target.position - target.position).sqrMagnitude <= firingRange * firingRange)
        {
            ManageWeapon();
        }

        if (canSeeTarget)
        {
            Debug.DrawRay(transform.position, targetDirection * detectionRange, new Color(1, 0, 0));
            pathfindingTarget.position = target.position;
        }
        else
        {
            Debug.DrawRay(transform.position, targetDirection * detectionRange, new Color(0.1f, 0.1f, 0.1f));
        }

    }

    private void ManageWeapon()
    {
        weapon.RotateTowardPoint(target.position);
        weapon.Shoot();
    }

    public void SetDamaged()
    {
        aiPath.enabled = false;
        Invoke("UnsetDamaged", 1);
    }

    public void UnsetDamaged()
    {
        aiPath.enabled = true;
    }

    private void OnDestroy()
    {
        EnemyEvents.scoreAdvancedEvent.Invoke(Random.Range(8, 12) * 10);
    }

    private void ListenToTarget(Vector2 position)
    {
        pathfindingTarget.position = position;
    }
}
