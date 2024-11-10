using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBtn : MonoBehaviour
{
    void Start(){
        gameObject.GetComponent<Button>().onClick.AddListener(destroyself);
    }
    void destroyself(){
        Destroy(gameObject);
    }

    void Update(){
        if(Input.anyKeyDown)gameObject.GetComponent<Button>().onClick.Invoke();
    }
}
