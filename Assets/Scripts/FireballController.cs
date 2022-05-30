using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    //private BurnController burnController;
    public Material burnMat;

    // Start is called before the first frame update
    void Start()
    {
       //burnController.GetComponent<BurnController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,0,10) * Time.deltaTime;
    }

     private void OnTriggerEnter(Collider other) {
         
        //wenn objekt verbrennen soll
        if(other.gameObject.tag == "Burnable"){
            foreach(Transform child in other.gameObject.transform){
                child.gameObject.AddComponent<BurnController>();   //add burnscript to childs
            }
            Debug.Log("burrrrrrrrrn");
            //other.gameObject.GetComponent<BurnController>().getBurnMaterial();
        }

        //wenn objekt etwas anzünden soll
        if(other.gameObject.tag == "Kindle") {
            Debug.Log("kindle stuff");
        }


        //immer
        transform.gameObject.SetActive(false);  //feuerball löschen
        
        /*
        *   explosion an der auftreffstelle
        */
    }
}
