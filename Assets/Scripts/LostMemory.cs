using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostMemory : MonoBehaviour
{

    public GameObject MapLimit;

    public float Bounciness = 0.008f;
    public float Frequency = 1f;

    Vector3 pos;

    void Start(){ pos=transform.position;}

    private void FixedUpdate() 
    {
        pos.y += Mathf.Sin(Time.fixedTime*Mathf.PI*Frequency)*Bounciness;
        transform.position = pos;

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            GameManager.Instance.memoryFind();
            Destroy(gameObject, 2f);
            Destroy(other.gameObject.GetComponent<Player>());
            Destroy(other.gameObject.transform.GetChild(0).GetComponent<Animator>());
            
            GameManager.Instance.PlayerDie();
            Destroy(MapLimit);
            Destroy(this);
        }
    }
}
