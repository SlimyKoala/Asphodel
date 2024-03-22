using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float accelerationCoef = 1f;
    [SerializeField] float frictionCoef = 0.95f;
    [SerializeField] HealthBar healthBar;

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
        healthBar.SetMaxHealth((int)GetComponent<HPController>().GetMaxHP());
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (PauseMenu.isGamePaused)
        {
            return;
        }
        mousePointIngame = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Move();
        ManageDash();
        ManageHealthBar();
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

    private void ManageHealthBar()
    {
        healthBar.SetHealth((int) GetComponent<HPController>().GetHP());
    }


    private void OnDestroy()
    {
        healthBar.SetHealth(0);
        TimerManager.active.AddTimer(
            2, 
            () => { SceneManager.LoadScene(0); }, 
            false
        );
    }
}
