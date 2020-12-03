using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class synctojoint : MonoBehaviour
{
    public GameObject t1;
    public GameObject t2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = (t1.transform.position + t2.transform.position) / 2;
        //this.transform.rotation = t1.transform.position - t2.transform.position;
        
    }
}
