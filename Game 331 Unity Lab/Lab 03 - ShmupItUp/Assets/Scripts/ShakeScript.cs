using UnityEngine;
using UnityEngine.Serialization;

namespace GAME331.Lab03
{
    /// <summary>
    /// Script to shake the attached GameObject.
    /// </summary>
    public class ShakeScript : MonoBehaviour
    {
        /// <summary>
        /// The current magnitude of the shake effect.
        /// </summary>
        private float currentShakeMagnitude;

        /// <summary>
        /// The remaining duration of the shake effect.
        /// </summary>
        private float shakeSecondsRemaining;

        /// <summary>
        /// The position of the GameObject at the start of the shake effect.
        /// </summary>
        private Vector3 positionAtStartOfShake;

        /// <summary>
        /// Updates the position of the GameObject to create a shake effect if the shake is active.
        /// </summary>
        private void Update()
        {
            if (shakeSecondsRemaining > 0.0f && Time.timeScale != 0)
            {
                // Generate a random camera offset for the shake effect.
                Vector2 cameraOffset = Random.insideUnitCircle * currentShakeMagnitude;

                // Apply the shake offset to the GameObject's position along the X and Z axes.
                transform.localPosition = new Vector3
                (
                    transform.localPosition.x + cameraOffset.x,
                    transform.localPosition.y,
                    transform.localPosition.z + cameraOffset.y
                );

                // Reduce the remaining shake time by the elapsed time since the last frame.
                shakeSecondsRemaining -= Time.deltaTime;

                // If the shake duration has ended, reset the position of the GameObject to its original position.
                if (shakeSecondsRemaining <= 0.0f)
                {
                    shakeSecondsRemaining = 0.0f;
                    transform.localPosition = positionAtStartOfShake;
                }
            }
        }

        /// <summary>
        /// Initiates a shake effect.
        /// </summary>
        /// <param name="shakeSeconds">Duration of the shake effect in seconds.</param>
        /// <param name="shakeMagnitude">Magnitude (strength) of the shake effect.</param>
        public void Shake(float shakeSeconds, float shakeMagnitude)
        {
            shakeSecondsRemaining = shakeSeconds;
            currentShakeMagnitude = shakeMagnitude;
            positionAtStartOfShake = transform.localPosition;
        }

        /// <summary>
        /// Checks if the GameObject is currently shaking.
        /// </summary>
        /// <returns>True if shaking; otherwise, false.</returns>
        public bool IsShaking()
        {
            return shakeSecondsRemaining > 0.0f;
        }
    }

}