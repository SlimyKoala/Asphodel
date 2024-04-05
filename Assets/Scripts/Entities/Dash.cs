using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float dashForce = 10f;
    [SerializeField] float cooldown = 0.5f;
    private float lastDashTime;
    bool isDashing;
    void Start()
    {
        isDashing = false;
        rb = GetComponent<Rigidbody2D>();
        lastDashTime = Time.time;
    }
    private void Update()
    {
        if (Time.time - lastDashTime > cooldown)
        {
            isDashing = false;
        }
    }

    public void DoDash(Vector2 direction)
    {
        if(Time.time - lastDashTime > cooldown)
        {
            isDashing = true;
            rb.velocity += direction.normalized * dashForce;
            lastDashTime = Time.time;
        }

    }

    public bool GetDash()
    {
        return isDashing;
    }
}
