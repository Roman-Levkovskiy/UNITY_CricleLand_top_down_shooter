using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour
{
    public string type;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ++collision.GetComponent<PlayerController>().materials[type];
            Destroy(gameObject);
        }
    }
}
