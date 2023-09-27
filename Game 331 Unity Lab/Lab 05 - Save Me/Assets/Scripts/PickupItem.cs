using UnityEngine;

namespace GAME331.Lab05
{
    /// <summary>
    /// Represents a pickup item that can be collected by the player.
    /// </summary>
    public class PickupItem : MonoBehaviour
    {
        /// <summary>
        /// Enumeration of available item types.
        /// </summary>
        public enum ItemType
        {
            Apple,
            Orange
        }

        public ItemType myItemType; // The type of this pickup item.

        /// <summary>
        /// Called when this object collides with another object.
        /// </summary>
        /// <param name="collision">The collision data.</param>
        void OnCollisionEnter(Collision collision)
        {
            // Check if the colliding object has a PlayerMovement component.
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                // If the pickup item is an Apple, increment the AppleCount of the player.
                if (myItemType == ItemType.Apple)
                {
                    playerMovement.appleCount++;
                }
                // If the pickup item is an Orange, increment the OrangeCount of the player.
                else if (myItemType == ItemType.Orange)
                {
                    playerMovement.orangeCount++;
                }

                // Destroy this pickup item after it's collected by the player.
                Destroy(gameObject);
            }
        }
    }
}