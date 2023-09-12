using UnityEngine;
using UnityEngine.Serialization;

namespace GAME331.Lab03
{
    /// <summary>
    /// Controls the movement of the hero ship.
    /// </summary>
    public class HeroShipMovement : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The movement speed of the hero ship.")]
        private float movementSpeed = 4.0f;


        [SerializeField]
        [Tooltip("Reference to the ShakeScript for hit effects.")]
        private ShakeScript hitShake;

        /// <summary>
        /// Update's the hero ship's position based on player's input
        /// </summary>
        private void Update()
        {
            // Getting raw input bypasses Unity's input filtering which can mean quicker response
            // to controls at the cost of having to ignore spurious (bogus) input yourself
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            Vector3 desiredDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
            desiredDirection.Normalize();

            transform.position += desiredDirection * Time.deltaTime * movementSpeed;
        }

        /// <summary>
        /// Called when the hero ship collides with a particle system.
        /// </summary>
        /// <param name="other">The GameObject the particle collided with.</param>
        private void OnParticleCollision(GameObject other)
        {
            if (!hitShake.IsShaking())
            {
                hitShake.Shake(0.25f, 0.125f);
            }
        }
    }
}