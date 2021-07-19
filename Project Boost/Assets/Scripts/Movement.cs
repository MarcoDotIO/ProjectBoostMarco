using UnityEngine;

public class Movement : MonoBehaviour
{
    // Main thrust value for relative up force
    [SerializeField] float mainThrustValue = 1075f;

    // Main Left value for relative Left rotation
    [SerializeField] float mainLeftValue = 200f;

    // Main Right value for relative Right rotation
    [SerializeField] float mainRightValue = 200f;

    // Engine thrust SFX
    [SerializeField] AudioClip engineThrustSFX;

    // Particle effect for left thruster
    [SerializeField] ParticleSystem leftThrusterParticle;

    // Particle effect for right thruster
    [SerializeField] ParticleSystem rightThrusterParticle;

    // Particle effect for main thruster
    [SerializeField] ParticleSystem mainThrusterParticle;

    // Rigid Body for Rocket
    Rigidbody rocketRigidBody;

    // Aduio source for Rocket
    public AudioSource rocketMovementAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Get component call
        rocketRigidBody = GetComponent<Rigidbody>();

        // Get component for audio source
        rocketMovementAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // For processing input
    void ProcessThrust()
    {
        if ( Input.GetKey( KeyCode.W ) || Input.GetKey( KeyCode.Space ) )
        {
            // Logging for debug
            Debug.Log("Pressed Space - Thrusters");

            // Adding Relative Force
            rocketRigidBody.AddRelativeForce( Vector3.up * mainThrustValue * Time.deltaTime );

            // If there is no audio playing while holding thrust, play the sound
            if (!rocketMovementAudioSource.isPlaying)
            {
                rocketMovementAudioSource.PlayOneShot(engineThrustSFX);
            }

            // Start the thruster particle systems
            StartThrusterParticles();
        }

        else
        {
            // Stop playing the sound if the thrust isn't pressed down
            rocketMovementAudioSource.Stop();

            // Stop the thruster particle systems
            StopThrusterParticles();
        }
    }

    // For processing rotation
    void ProcessRotation()
    {
        if ( Input.GetKey( KeyCode.D ) )
        {
            // Logging for debug
            Debug.Log( "Pressed A - Left" );

            // Rotate right
            ApplyRotation( false );
        }

        else if ( Input.GetKey( KeyCode.A ) )
        {
            // Logging for debug
            Debug.Log( "Pressed D - Right" );

            // Rotate left
            ApplyRotation( true );
        }
    }

    // For rotations. Input: isRotatingLeft - Bool (is the rotation going left?)
    void ApplyRotation(bool isRotatingLeft)
    {

        // Freezing rotation so manual rotation can take control.
        rocketRigidBody.freezeRotation = true;

        if (isRotatingLeft)
        {
            // Rotate left
            transform.Rotate(Vector3.forward * mainLeftValue * Time.deltaTime);
        }

        else
        {
            // Rotate right
            transform.Rotate(Vector3.back * mainRightValue * Time.deltaTime);
        }

        // Unfreezing for physics control.
        rocketRigidBody.freezeRotation = false;
    }

    void StartThrusterParticles()
    {
        // Play the three main thruster particle loops
        mainThrusterParticle.Play();
        leftThrusterParticle.Play();
        rightThrusterParticle.Play();
    }

    public void StopThrusterParticles()
    {
        // Stop the three main thruster particle loops
        mainThrusterParticle.Stop();
        leftThrusterParticle.Stop();
        rightThrusterParticle.Stop();
    }

}