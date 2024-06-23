using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class AsansorScript : MonoBehaviour
{
    public Transform upperWaypoint; // The upper point the platform will move to
    public Transform lowerWaypoint; // The lower point the platform will move to
    public float maxSpeed; // Platform's maximum speed
    public float accelerationDistance; // Distance over which the platform accelerates
    public float decelerationDistance; // Distance over which the platform decelerates

    private bool movingUp = true; // Flag to check if the platform is moving up
    private Vector2 speed = Vector2.zero; // Platform's speed
    private bool isMoving = false; // Flag to check if the platform is moving
    private List<ObjectController2D> objs = new List<ObjectController2D>(); // List of objects in contact with the platform

    private Collider2D myCollider; // Platform's collider

    void Start() {
        myCollider = GetComponent<Collider2D>(); // Get Collider2D component
    }

    void FixedUpdate() {
        if (isMoving) {
            MoveToWaypoint(movingUp ? upperWaypoint : lowerWaypoint);
        }
    }

    // Method to start moving the platform to the next waypoint
    public void MovePlatform() {
        isMoving = true;
    }

    // Method to move the platform towards the current waypoint
    private void MoveToWaypoint(Transform targetWaypoint) {
        Vector2 distance = targetWaypoint.position - transform.position;
        if (distance.magnitude <= decelerationDistance) {
            speed = Vector2.Lerp(speed, Vector2.zero, Time.fixedDeltaTime * maxSpeed / decelerationDistance);
        } else {
            speed = Vector2.Lerp(speed, distance.normalized * maxSpeed, Time.fixedDeltaTime * maxSpeed / accelerationDistance);
        }

        Vector3 newPos = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed.magnitude * Time.fixedDeltaTime);
        Vector2 velocity = newPos - transform.position;

        transform.position = newPos;
        MoveObjects(velocity);

        if (distance.magnitude < 0.01f) {
            isMoving = false;
            movingUp = !movingUp; // Toggle the direction
        }
    }

    // Method to move objects on the platform
    private void MoveObjects(Vector2 velocity) {
        foreach (ObjectController2D obj in objs) {
            obj.Move(velocity);
        }
    }

    // When another object collides with the platform
    void OnTriggerEnter2D(Collider2D other) {
        AttachObject(other);
    }

    // While another object is in contact with the platform
    void OnTriggerStay2D(Collider2D other) {
        AttachObject(other);
    }

    // Attach the object to the platform
    private void AttachObject(Collider2D other) {
        ObjectController2D obj = other.GetComponent<ObjectController2D>();
        if (obj && !objs.Contains(obj)) {
            objs.Add(obj);
        }
    }

    // When another object exits the platform
    void OnTriggerExit2D(Collider2D other) {
        ObjectController2D obj = other.GetComponent<ObjectController2D>();
        if (obj && objs.Contains(obj)) {
            objs.Remove(obj);
        }
    }
}