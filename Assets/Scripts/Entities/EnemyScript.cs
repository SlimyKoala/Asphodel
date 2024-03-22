using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float accelerationCoef = 1f;
    [SerializeField] float frictionCoef = 0.95f;
    private Rigidbody2D rb;

    private Transform target;
    private string state;

    [SerializeField] float minFireDistance = 3;
    [SerializeField] float maxFireDistance = 10;

    [SerializeField] Shooting weapon;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Rat").transform;
        state = "CHASE";
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float distance = (target.position - transform.position).sqrMagnitude;

        if (distance > maxFireDistance * maxFireDistance) state = "CHASE";
        else if (distance > minFireDistance * minFireDistance) state = "FIRE";
        else state = "RUN";

        if (state == "CHASE") Move();
        if (state == "FIRE") Fire();
        if (state == "RUN") Run();
    }

    private void Move()
    {
        Vector2 inputVector = (target.position - transform.position).normalized;
        rb.velocity += inputVector * accelerationCoef;
        rb.velocity *= frictionCoef;
    }

    private void Fire()
    {
        rb.velocity *= frictionCoef;
        ManageWeapon();
    }

    private void Run()
    {
        Vector2 inputVector = -(target.position - transform.position).normalized;
        rb.velocity += inputVector * accelerationCoef;
        rb.velocity *= frictionCoef;
    }

    private void ManageWeapon()
    {
        Vector2 point = ((Vector2)target.position);
        weapon.RotateTowardPoint(point);
        weapon.Shoot();
    }
}
