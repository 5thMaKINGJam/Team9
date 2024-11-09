using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBridge : MonoBehaviour
{
    public GameObject Bridge;
    public Transform pos;
     void OnEnable()
    {
        Instantiate(Bridge, pos);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(LetsBreak());
            gameObject.SetActive(false);
        }
    }

    IEnumerator LetsBreak(){
        print(Bridge.transform.childCount);
        for(int i = 2; i < Bridge.transform.childCount; i++){
            yield return new WaitForSeconds(0.5f);
            print(i);
            Bridge.transform.GetChild(i).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
