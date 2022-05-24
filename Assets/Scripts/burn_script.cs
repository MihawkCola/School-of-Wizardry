using System.Collections.Generic;
using UnityEngine;

public class burn_script : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1f;
    public float dissolveSpeed = 1f;
    public float dissolve_threshold = 0.75f;
    
    private float burn_amount;
    private float burn_default;
    private float dissolve_amount;
    private float dissolve_default;

    private bool x = true;

    [SerializeField] Material burningMat;
    private Material lastDefaultMat;

    //Texture Components
    private Color object_color;
    private Texture object_tex;
    private Texture normal_tex;
    private float metallic;
    private float smoothness;


    //more than one object
    private List<Transform> parts = new List<Transform>();
    private Renderer[] renderer;
    

    private bool go = false;
    void Start()
    {
        //renderer = GetComponent<Renderer> ();   //renderer
        burn_default = burningMat.GetFloat("prop_burn");    //anfangswert burn    0.55
        dissolve_default = burningMat.GetFloat("prop_dissolve");    //anfangswert dissolve  0
        burn_amount = burn_default;     //aktiver burn wert
        dissolve_amount = dissolve_default;     //aktiver dissolve wert

        foreach(Transform child in transform){
            parts.Add(child);
        }
        renderer = new Renderer[parts.Count];   //as many renderers as parts
        for(int i = 0; i < parts.Count; i++){
            renderer[i] = parts[i].GetComponent<Renderer>();
        }

    /*-----------------------------

        object_color = renderer.material.color;
        //Debug.Log("color: " + object_color);
        burningMat.SetColor("_object_color", object_color);

    */
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(normal_tex);
        if(Input.GetKey("b")){

            foreach(Transform Part in parts){
                

                object_tex = Part.GetComponent<Renderer>().material.GetTexture("_MainTex");
                normal_tex = Part.GetComponent<Renderer>().material.GetTexture("_BumpMap");
                metallic = Part.GetComponent<Renderer>().material.GetFloat("_Metallic");
                smoothness = Part.GetComponent<Renderer>().material.GetFloat("_Smoothness");
                //burningMat.mainTexture = object_tex;
                burningMat.SetTexture("_object_tex", object_tex);
                burningMat.SetTexture("_normal_tex", normal_tex);
                burningMat.SetFloat("_metallic", metallic);
                burningMat.SetFloat("_smoothness", smoothness);

                Material[] mats = {Part.GetComponent<Renderer>().material, burningMat};      //erstelle neues mat array mit old mat und burn mat
                lastDefaultMat = Part.GetComponent<Renderer>().material; //save old mat

                Part.GetComponent<Renderer>().materials = mats;  //set new mats

                burningMat.SetFloat("prop_dissolve", 0);
                burn_amount = 0;
                dissolve_amount = 0;
                go = true;

            }    
        }

        if(Input.GetKey("r")){
            go = false;
            dissolve_amount = dissolve_default;

            Material[] mats = {lastDefaultMat};
            //renderer.materials = mats;

            burningMat.SetFloat("prop_dissolve", dissolve_default);
            burn_amount = burn_default;
            burningMat.SetFloat("prop_burn", burn_default);
        }
    
        if(go){
                if(burn_amount < dissolve_threshold){   //burn bis threshold (0.75)
                    Material[] mats = {burningMat};
                    foreach(Transform Part in parts){
                        Part.GetComponent<Renderer>().materials = mats;
                    }
                    //renderer.materials = mats;
                    burn_amount += speed * Time.deltaTime;
                    burningMat.SetFloat("prop_burn", burn_amount);
            } 
            else{
                if(x){
                    Material[] mats = {burningMat};
                    foreach(Transform Part in parts){
                        Part.GetComponent<Renderer>().materials = mats;
                    }
                    //renderer.materials = mats;
                    x = false;
                }
                dissolve_amount += dissolveSpeed * Time.deltaTime;
                burningMat.SetFloat("prop_dissolve", dissolve_amount);

                if(dissolve_amount > 1.1f){
                    Material[] mats = {};
                    foreach(Transform Part in parts){
                        Part.GetComponent<Renderer>().materials = mats;
                    }
                    //renderer.materials = mats;
                }
            }
        }
        
    }
}
