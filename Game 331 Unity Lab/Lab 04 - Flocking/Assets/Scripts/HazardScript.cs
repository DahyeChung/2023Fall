using UnityEngine;

namespace GAME331.Lab04
{
    /// <summary>
    /// Class representing a hazard in the game world.
    /// </summary>
    public class HazardScript : MonoBehaviour
    {
        /// <summary>
        /// Called when the collider of this hazard makes contact with another collider.
        /// </summary>
        /// <param name="collision">The collision data of the contact.</param>
        private void OnCollisionEnter(Collision collision)
        {
            // Check if the collided object has a FlockerScript attached to it.
            FlockerScript flocker = collision.gameObject.GetComponent<FlockerScript>();

            // If the collided object has a FlockerScript, destroy it.
            if (flocker != null)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}