using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GAME331.Lab02
{
    /// <summary>
    /// Handles conversations and interactions with NPCs in the game.
    /// </summary>
    public class ConversationHandler : MonoBehaviour
    {

        /// <summary>
        /// Input buttons for triggering interactions.
        /// </summary>
        private string[] interactions = { "Fire1", "Submit", "Jump" };

        private enum InteractionState
        {
            None,
            Idle,
            Talking
        }

        [Tooltip("Panel for displaying conversation UI.")]
        [SerializeField] private Image conversationPanel;

        [Tooltip("Text component for displaying the name of the speaker.")]
        [SerializeField] private TextMeshProUGUI conversationSpeaker;

        [Tooltip("Text component for displaying the conversation text.")]
        [SerializeField] private TextMeshProUGUI conversationText;

        private InteractionState currentInteractionState;
        private int currentTalkerId;

        [Tooltip("Database containing talker information.")]
        [SerializeField] private TalkerDatabase talkerDatabase;

        private void Start()
        {
            currentInteractionState = InteractionState.None;
            currentTalkerId = 0;
            SetInteractionState(InteractionState.Idle);
        }

        private void Update()
        {
            // Check each of the possible interactions
            foreach (var interaction in interactions)
            {
                // If the player activates them
                if (Input.GetButtonDown(interaction))
                {
                    // Depending on the current interaction state
                    if (currentInteractionState == InteractionState.Idle)
                    {
                        TalkyPerson[] talkyPeople = FindObjectsByType<TalkyPerson>(FindObjectsSortMode.None);
                        foreach (TalkyPerson talkyPerson in talkyPeople)
                        {
                            if (talkyPerson.talkInteractionCollider.bounds.Contains(transform.position))
                            {
                                currentTalkerId = talkyPerson.talkerId;
                                SetInteractionState(InteractionState.Talking);

                                // Stop the player
                                GetComponent<Rigidbody>().velocity = Vector3.zero;
                                return;
                            }
                        }
                    }
                    else if (currentInteractionState == InteractionState.Talking)
                    {
                        SetInteractionState(InteractionState.Idle);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Helper function to set the visibility of the conversation panel and text objects
        /// </summary>
        /// <param name="visible">if should be visible</param>
        private void SetConversionVisibility(bool visible)
        {
            conversationPanel.enabled = visible;
            conversationSpeaker.enabled = visible;
            conversationText.enabled = visible;
        }

        /// <summary>
        /// Updates the Interaction State to its new value
        /// </summary>
        /// <param name="newState">The state to change to</param>
        private void SetInteractionState(InteractionState newState)
        {
            if (newState != currentInteractionState)
            {
                currentInteractionState = newState;
                switch (currentInteractionState)
                {
                    case InteractionState.Idle:
                        SetConversionVisibility(false);
                        break;
                    case InteractionState.Talking:
                        SetConversionVisibility(true);

                        conversationSpeaker.text = talkerDatabase.GetTalkerName(currentTalkerId);
                        conversationText.text = talkerDatabase.GetTalkerText(currentTalkerId);
                        break;
                }
            }
        }

        /// <summary>
        /// Checks if the player is currently in an interaction.
        /// </summary>
        /// <returns>True if the player is in an interaction, false otherwise.</returns>
        public bool IsInteracting()
        {
            return currentInteractionState == InteractionState.Talking;
        }
    }
}