using TMPro;
using UnityEngine;

namespace GAME331.Lab05
{
    /// <summary>
    /// Updates the player status text on the UI to display the number of apples and 
    /// oranges collected by the player.
    /// </summary>
    public class PlayerStatusText : MonoBehaviour
    {
        /// <summary>
        /// Reference to the UI Text component.
        /// </summary>
        private TextMeshProUGUI myText;

        [Tooltip("Reference to the PlayerMovement script attached to the player.")]
        public PlayerMovement targetPlayer; 

        /// <summary>
        /// Find the target player and get the reference to the UI Text component.
        /// </summary>
        private void Start()
        {
            if (!targetPlayer)
            {
                targetPlayer = GameObject.FindFirstObjectByType<PlayerMovement>();
            }
            myText = GetComponent<TextMeshProUGUI>();
        }

        /// <summary>
        /// Update the UI Text to display the number of apples and oranges collected by the player.
        /// </summary>
        private void Update()
        {
            // Set the text to display the number of apples and oranges collected by the player.
            myText.text = "Apples: " + targetPlayer.appleCount + "\n" +
                         "Oranges: " + targetPlayer.orangeCount;
        }
    }
}
