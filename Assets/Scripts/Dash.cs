using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float dashForce = 10f;
    [SerializeField] float cooldown = 0.5f;
    private float lastDashTime;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastDashTime = Time.time;
    }

    public void DoDash(Vector2 direction)
    {
        if(Time.time - lastDashTime > cooldown)
        {
            rb.velocity += direction.normalized * dashForce;
            lastDashTime = Time.time;
        }

    }
}
