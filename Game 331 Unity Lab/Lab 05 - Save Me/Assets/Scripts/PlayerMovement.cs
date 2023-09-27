using UnityEngine;

namespace GAME331.Lab05
{
    /// <summary>
    /// Controls the movement of the player character.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// The speed at which the player character moves per second.
        /// </summary>
        [Tooltip("The speed at which the player character moves per second.")]
        public float speedPerSecond = 16.0f;

        /// <summary>
        /// The count of apples collected by the player character.
        /// </summary>
        [Tooltip("The count of apples collected by the player character.")]
        public int appleCount;

        /// <summary>
        /// The count of oranges collected by the player character.
        /// </summary>
        [Tooltip("The count of oranges collected by the player character.")]
        public int orangeCount;

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
        /// Called every fixed framerate frame. Used for physics updates.
        /// </summary>
        private void FixedUpdate()
        {
            // Get the input for horizontal and vertical movement.
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calculate the desired movement direction.
            Vector3 desiredDirection = new Vector3(horizontalInput, 0f, verticalInput);

            // Set the velocity of the Rigidbody to move the player.
            rb.velocity = desiredDirection * speedPerSecond;
        }
    }
}
