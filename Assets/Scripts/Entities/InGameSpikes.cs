using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSpikes : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 6)
        {
            HPController hp = collider.GetComponent<HPController>();

            if (collider.GetComponent<Dash>().GetDash() == false)
            {
                hp.TakeDamage(25);
            }

        }

    }
}
