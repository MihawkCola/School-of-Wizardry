using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private XROrigin xrRig;
    private CapsuleCollider collider;
    private Rigidbody rb;

    [SerializeField] GameObject camObj;
    
    [SerializeField] InputActionProperty moveAction;
    [SerializeField] float moveSpeed = 1;

    [SerializeField] float velocityLimit = 50;

    void Start()
    {
        xrRig = GetComponent<XROrigin>();
        collider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log(rb.velocity.magnitude);

        Vector2 moveVector = moveAction.action.ReadValue<Vector2>();

        if (moveVector != Vector2.zero && rb.velocity.magnitude < velocityLimit)
        {
            Vector3 moveForce = camObj.transform.forward * moveVector.y * moveSpeed * Time.deltaTime;

            rb.AddForce(moveForce);
        }
            

        //Vector3 center = xrRig.CameraInOriginSpacePos;
        //collider.center = new Vector3(center.x, collider.center.y, center.z);
        collider.height = xrRig.CameraInOriginSpaceHeight;
    }
}
