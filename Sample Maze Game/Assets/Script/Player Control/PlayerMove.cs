using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public FixedJoystick joystick;
    public float SpeedMove = 5f;
    private CharacterController controller;

    //footstep
    private Vector3 lastPosition; // To store the last position
    public float stepDistance = 2f; // Distance required to trigger footstep sound
    private float distanceMoved = 0f; // Accumulated distance

    // Footstep audio
    public AudioSource footstepAudioSource; // Assign in inspector
    public AudioClip footstepClip; // Assign the footstep sound clip in inspector
    public float footstepVolume = 0.5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        lastPosition = transform.position; // Initialize last position
    }


    void Update()
    {
        Vector3 Move = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;
        controller.Move(Move * SpeedMove * Time.deltaTime);

        // Calculate the distance moved
        float distanceThisFrame = Vector3.Distance(transform.position, lastPosition);
        distanceMoved += distanceThisFrame;

        // If the player moved enough distance, play footstep sound
        if (distanceMoved >= stepDistance && footstepAudioSource != null && !footstepAudioSource.isPlaying)
        {
            footstepAudioSource.PlayOneShot(footstepClip, footstepVolume);
            distanceMoved = 0f; // Reset the accumulated distance
        }

        // Update last position for the next frame
        lastPosition = transform.position;
    }
    public void SetMoveSpeed(float newSpeed)
    {
        SpeedMove = newSpeed;
    }
}
