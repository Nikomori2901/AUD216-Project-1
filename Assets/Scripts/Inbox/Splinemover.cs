using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splinemover : MonoBehaviour
{
    public Spline spline; // The spline to follow
    public Transform followObj;
    public AudioSource audioSource; // The AudioSource to move along the spline
    public float speed = 5f; // Speed at which the object moves along the spline
    public bool smoothMovement = true; // Enable or disable smooth movement
    public float smoothTime = 0.3f; // Smoothing time for the movement
    public bool offsetStartPoint = false; // Toggle to offset the start point of the audio clip

    private Transform thisTransform;
    private Vector3 velocity = Vector3.zero; // Velocity used for smooth damping

    private void Awake()
    {
        thisTransform = transform;
    }

    private void Start()
    {
        // Offset the start point of the audio clip if the toggle is enabled
        if (offsetStartPoint && audioSource != null && audioSource.clip != null)
        {
            audioSource.time = audioSource.clip.length * 0.5f; // Offset by 50% of the clip's length
        }

        // Ensure the audio source is playing
        if (audioSource != null) audioSource.Play();
    }

    private void Update()
    {
        if (spline == null || followObj == null)
        {
            return; // Exit if the spline or followObj is not set
        }

        Vector3 targetPosition = spline.WhereOnSpline(followObj.position);

        if (smoothMovement)
        {
            // Smoothly move towards the target position
            thisTransform.position = Vector3.SmoothDamp(thisTransform.position, targetPosition, ref velocity, smoothTime);
        }
        else
        {
            // Move directly towards the target position at the specified speed
            thisTransform.position = Vector3.MoveTowards(thisTransform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (spline == null || followObj == null)
        {
            return; // Exit if the spline or followObj is not set
        }

        if (thisTransform == null)
        {
            thisTransform = transform;
        }

        if (spline.IsSplinePointInitialized())
        {
            Vector3 targetPosition = spline.WhereOnSpline(followObj.position);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(thisTransform.position, targetPosition);
        }
    }
}
