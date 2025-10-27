using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Input Actions
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;

    // Movement parameters
    [SerializeField] float thrustStrength = 1000f;
    [SerializeField] float rotationStrength = 100f;

    // Audio
    [SerializeField] AudioClip mainEngineSound;
    
    // Particle Systems 
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    // Components
    Rigidbody rb;
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
        
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            Thrusting();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void Thrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.fixedDeltaTime * thrustStrength);
        mainEngineParticles.Play();
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSound);
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
            rightThrusterParticles.Play();
        }

        if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
            leftThrusterParticles.Play();
        }

        if (rotationInput == 0)
        {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Stop();
        }

    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.fixedDeltaTime * rotationThisFrame);
        rb.freezeRotation = false;
    }
}
