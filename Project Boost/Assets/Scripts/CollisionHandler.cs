using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // Delay time for creash reload
    [SerializeField] float mainCreashReloadTime = 1f;

    // Delay time for loading next level
    [SerializeField] float mainNextLevelTime = 1f;

    // Audio clip for successful landing
    [SerializeField] AudioClip landedAudio;

    // Audio clip for crashing
    [SerializeField] AudioClip crashedAudio;

    // Particle effect for successful landing
    [SerializeField] ParticleSystem landedParticle;

    // Particle effect for crashing
    [SerializeField] ParticleSystem crashedParticle;

    // Audio source for the rocket
    AudioSource rocketCollisionAudioSource;

    // Are we transitioning to the next scene?
    public bool isTransitioning = false;

    // Is the rocket still alive?
    public bool isAlive = true;

    // Is collision on?
    bool collisionDisable = false;

    void Start()
    {
        // Getting rocket audio source from audio source class
        rocketCollisionAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Responding to debug input
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Go to the next level
            LoadNextLevel();
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            // Toggling collision
            collisionDisable = !collisionDisable;
        }
    }

    // Collision Handler Function
    void OnCollisionEnter(Collision collision)
    {
        // If collision is disabled, return
        if (collisionDisable) { return; }

        // Tag switch statement for item collision
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // Debug logging
                Debug.Log("This is a friendly object.");
                break;

            case "Enemy":
                // Debug logging
                Debug.Log("This is an enemy object.");

                // Start the crash sequence
                StartCrashSequence();

                break;

            case "Finish":
                // Debug logging
                Debug.Log("This is the finish object.");

                // Load the next scene
                StartNextLevelSequence();

                break;
        }
    }

    // Disables controls and restarts level
    void StartCrashSequence()
    {
        if (!isTransitioning && isAlive)
        {
            // The ship isn't alive anymore
            isAlive = false;

            // Activate the particle system for crashing
            crashedParticle.Play();

            // Stop thruster audio
            GetComponent<Movement>().rocketMovementAudioSource.Stop();

            // Stop thruster particles
            GetComponent<Movement>().StopThrusterParticles();

            // Play crashing sound
            rocketCollisionAudioSource.PlayOneShot(crashedAudio);

            // Get movement call
            GetComponent<Movement>().enabled = false;

            // Reset the current scene
            Invoke("ReloadLevel", mainCreashReloadTime);
        }
    }

    // Disable controls and start the next level
    void StartNextLevelSequence()
    {
        if (!isTransitioning && isAlive)
        {
            // We are transitioning
            isTransitioning = true;

            // Activate the landing particle system for landing
            landedParticle.Play();

            // Stop thruster audio
            GetComponent<Movement>().rocketMovementAudioSource.Stop();

            // Stop thruster particles
            GetComponent<Movement>().StopThrusterParticles();

            // Play landing sound
            rocketCollisionAudioSource.PlayOneShot(landedAudio);

            // Get movement call
            GetComponent<Movement>().enabled = false;

            // Reset the scene to go to the next one
            Invoke("LoadNextLevel", mainNextLevelTime);
        }
    }

    // Reloads the current level
    void ReloadLevel()
    {
        // Get the current level from scene manager
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        // Load the recent scene / level
        SceneManager.LoadScene(currentLevel);

        // We are now alive again
        isAlive = true;
    }

    // Load the next level, start over if last level is completed
    void LoadNextLevel()
    {
        // Get the current level from scene manager
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        // Get the next level
        int nextLevel = ++currentLevel;

        // If at last level, go back to the first level
        if (nextLevel == SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0;
        }

        // Load the recent scene / level
        SceneManager.LoadScene(nextLevel);

        // We are finished transitioning
        isTransitioning = false;
    }
}
