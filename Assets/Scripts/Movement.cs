using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        Rotation();
    }

    void ApplyRotation(float rotateFrame){
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateFrame);
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }
    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        }
    }
    void Rotation(){
        if(Input.GetKey(KeyCode.A)){
            ApplyRotation(mainThrust);
        }else if(Input.GetKey(KeyCode.D)){
            ApplyRotation(- mainThrust);
        }
    }
}
