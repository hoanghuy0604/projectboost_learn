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
    [SerializeField] ParticleSystem mainParticle;
    [SerializeField] ParticleSystem leftParticle;
    [SerializeField] ParticleSystem rightParticle;
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

    void Rotation(){
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else {
            leftParticle.Stop();
            rightParticle.Stop();
        }
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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainParticle.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainParticle.Play();
    }

    
    private void RotateRight()
    {
        ApplyRotation(-mainThrust);
        leftParticle.Play();
    }

    private void RotateLeft()
    {
        ApplyRotation(mainThrust);
        rightParticle.Play();
    }
}
