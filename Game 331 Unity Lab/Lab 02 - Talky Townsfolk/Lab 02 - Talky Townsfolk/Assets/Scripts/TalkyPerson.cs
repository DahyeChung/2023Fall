using UnityEngine;

namespace GAME331.Lab02
{
    /// <summary>
    /// Represents a talkable person in the game world. Used by 
    /// ConversationHandler to find all the objects that can be 
    /// talked to.
    /// </summary>
    public class TalkyPerson : MonoBehaviour
    {
        [Tooltip("The ID of the talker.")]
        public int talkerId;

        [Tooltip("The collider used for talk interaction.")]
        public Collider talkInteractionCollider;

        /// <summary>
        /// An image that will be dispalyed whenever the player is within the
        /// trigger that the TalkyPerson has
        /// </summary>
        private static GameObject keyPressImage;
        private Vector2 offset = new Vector2(0, -50);

        /// <summary>
        /// Initializes the TalkyPerson object.
        /// </summary>
        private void Start()
        {
            if (keyPressImage == null)
            {
                keyPressImage = GameObject.Find("Press Key");
                keyPressImage?.SetActive(false);
            }
        }

        /// <summary>
        /// Called when another collider enters the trigger collider attached 
        /// to this object.
        /// </summary>
        /// <param name="other">The collider that entered the trigger.</param>
        private void OnTriggerEnter(Collider other) //E 키 디스플레이 온
        {
            keyPressImage?.SetActive(true);
        }

        /// <summary>
        /// Called while another collider stays inside the trigger collider 
        /// attached to this object.
        /// </summary>
        /// <param name="other">The collider that is inside the trigger.</param>
        private void OnTriggerStay(Collider other)
        {
            if (keyPressImage != null)
            {
                Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
                pos += offset;
                keyPressImage.transform.position = pos;
            }
        }

        /// <summary>
        /// Called when another collider exits the trigger collider attached to 
        /// this object.
        /// </summary>
        /// <param name="other">The collider that exited the trigger.</param>
        private void OnTriggerExit(Collider other) //E 키 디스플레이 오프
        {
            keyPressImage?.SetActive(false);
        }
    }
}