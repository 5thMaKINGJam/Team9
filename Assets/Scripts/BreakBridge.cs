using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBridge : MonoBehaviour
{
    public GameObject Bridge;
    public GameObject BridgePrefab;
    public Transform pos;
     void OnEnable()
    {
        if(Bridge == null)
            Bridge=Instantiate(BridgePrefab, pos);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player")
        {
            GameManager.Instance.BreakPlease(Bridge);
            gameObject.SetActive(false);
        }
    }


}
