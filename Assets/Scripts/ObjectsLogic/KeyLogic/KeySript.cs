using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySript : MonoBehaviour
{
    private Color color;
    private LevelKeyController keyController;
    private void Start()
    {
        this.keyController = GameObject.Find("KeyManager").GetComponent<LevelKeyController>();
        if (this.keyController == null)
            Debug.LogWarning("kein KeyManager in der Scene");
        this.color = this.GetComponentInChildren<Renderer>().material.GetColor("_KeyLockColor");

    }

    public void grabKey() 
    {
        this.keyController.OnLockKeySave(this.color);
    }

    // funktion das der schl�ssel los gelassen wird
}
