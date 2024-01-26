using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float accelerationCoef = 1f;
    [SerializeField] float frictionCoef = 0.95f;
    private Rigidbody2D rb;

    WeaponManager weaponManager;
    private Dash dash;
    private Vector2 mousePointIngame;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dash = GetComponent<Dash>();
        weaponManager = GetComponent<WeaponManager>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        mousePointIngame = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Move();
        ManageDash();
    }

    private void Move()
    {
        Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.velocity += inputVector * accelerationCoef;

        rb.velocity *= frictionCoef;
    }

    private void ManageDash()
    {
        Vector2 playerToMouseVector = mousePointIngame - (Vector2)transform.position;
        if (Input.GetKey(KeyCode.Space))
        {
            dash.DoDash(playerToMouseVector);
        }
    }
}
