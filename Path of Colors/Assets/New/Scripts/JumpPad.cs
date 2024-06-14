using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {
    public Vector2 force;

    private Animator animator;
    private AudioSource audioSource;
    private string jumpAnimation = "jump";

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Sent when another object enters a collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="collision">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D collision) {
        ObjectController2D obj = collision.collider.GetComponent<ObjectController2D>();
        if (obj) {
            obj.SetForce(force);
            obj.IgnoreFriction = true;
            CharacterController2D character = obj.GetComponent<CharacterController2D>();
            if (character) {
                character.ResetJumpsAndDashes();
            }
            animator.SetTrigger(jumpAnimation);
            if (audioSource) {
                audioSource.Play();
            }
        }
    }
}
