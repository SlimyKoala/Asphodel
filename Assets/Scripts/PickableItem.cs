using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision enabled");
        if (collision.gameObject.GetComponent<PickUpManager>())
        {
            collision.gameObject.GetComponent<PickUpManager>().Pick(this);
        }
    }

}
