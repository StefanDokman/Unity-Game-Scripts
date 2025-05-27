using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = new Vector3(0, -3, 0);
            Vector3 rot = new Vector3(0,0,0);
            Quaternion rota;
            rota = Quaternion.Euler(rot);
            collision.gameObject.transform.rotation = rota;
        }
    }
}
