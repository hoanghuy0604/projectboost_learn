using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    AudioClip mainEngine;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    // Start is called before the first frame update
    void Start()
    {
        mainEngine = Resources.Load("Audios/rocket") as AudioClip;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
        if (Input.GetKey(KeyCode.Space))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        }
        else
        {
            audioSource.Stop();
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
