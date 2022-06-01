using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{

    public GameObject explosion_prefab;
    private GameObject explosion;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,0,10) * Time.deltaTime;
    }

    public void OnCollisionEnter(Collision other) {

        //explosion on hit
        explosion = Instantiate(explosion_prefab, other.GetContact(0).point, Quaternion.identity);
        Destroy(explosion, 2.0f);   //zerstoeren nach 2 sec
        
        //burn something
        if(other.gameObject.tag == "Burnable"){
            foreach(Transform child in other.gameObject.transform){
                child.gameObject.AddComponent<BurnController>();   //add burnscript to childs
            }
        }

        //kindle something
        if(other.gameObject.tag == "Kindle") {
            Debug.Log("kindle stuff");
        }

        //melting wall
        if(other.gameObject.tag == "Melting") {
            other.gameObject.AddComponent<MeltingController>(); //add Melting Script
        }

        //Destroy firball, mit ruben besprechen
        //transform.gameObject.SetActive(false);
    }
}
