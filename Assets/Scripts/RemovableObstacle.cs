using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovableObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,5f);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject, 2f);
        }
    }
}
