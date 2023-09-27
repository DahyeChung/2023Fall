using UnityEngine;

namespace GAME331.Lab04
{
    /// <summary>
    /// Controls the movement of the player character.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [Tooltip("The speed at which the player moves.")]
        public float moveSpeed = 16.0f;

        /// <summary>
        /// A reference to the player's Rigidbody component
        /// </summary>
        private Rigidbody rb;

        /// <summary>
        /// Takes care of setting our private variables so we don't need to 
        /// get it over and over
        /// </summary>
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Called every physics step.
        /// </summary>
        private void FixedUpdate()
        {
            // Get the input for horizontal and vertical movement.
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calculate the desired movement direction.
            Vector3 desiredDirection = new Vector3(horizontalInput, 0f, verticalInput);

            // Set the velocity of the Rigidbody to move the player.
            rb.velocity = desiredDirection * moveSpeed;
        }
    }
}