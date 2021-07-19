using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    // Starting position for obstacle
    Vector3 obstacleStartingPosition;

    // Obstacle moving vector and factor
    [SerializeField] Vector3 obstacleMovingVector;
    float obstacleMovingFactor;
    [SerializeField] float period = 3.75f;

    // Start is called before the first frame update
    void Start()
    {
        // Get starting position
        obstacleStartingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Don't divide by 0
        if (period <= Mathf.Epsilon)
        {
            Debug.Log("Yo wtf you divided by 0.");
            return;
        }

        // How many cycles have we had?
        float cycles = Time.time / period;

        // Define Tau (the full circumference of a circle)
        const float tau = Mathf.PI * 2;

        // Our raw sine wave for our obstacle movement (-1 to 1)
        float rawSineWave = Mathf.Sin(cycles * tau);

        // We want this to be offset by 2 to go from 0 to 1
        obstacleMovingFactor = (rawSineWave + 1f) / 2f;

        // Get obstacle offset
        Vector3 obstacleOffset = obstacleMovingVector * obstacleMovingFactor;

        // Set new position
        transform.position = obstacleStartingPosition + obstacleOffset;
    }
}
