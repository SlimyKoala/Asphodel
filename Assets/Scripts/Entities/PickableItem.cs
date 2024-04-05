using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [SerializeField] string pickupType;
    [SerializeField] float hpToHeal = 5;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PickUpManager>())
        {
            collision.gameObject.GetComponent<PickUpManager>().Pick(this, pickupType, hpToHeal);

            if(pickupType != "weapon")
            {
                Destroy(gameObject);
            }
        }
    }

}
