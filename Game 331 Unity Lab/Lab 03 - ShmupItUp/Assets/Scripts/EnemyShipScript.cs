using UnityEngine;
using UnityEngine.Serialization;

namespace GAME331.Lab03
{
    /// <summary>
    /// Script that manages the enemy ship's shot particle systems.
    /// </summary>
    public class EnemyShipScript : MonoBehaviour
    {
        /// <summary>
        /// The particle systems used for shots.
        /// </summary>
        public ParticleSystem[] shotParticleSystems;

        /// <summary>
        /// Time interval between shot system changes.
        /// </summary>
        public float secondsBetweenShotSystemChanges = 5.0f;

        private int currentShotParticleSystemIndex;

        private float secondsSinceLastSystemChange;

        /// <summary>
        /// Called before Start, any setup can be done here
        /// </summary>
        private void Awake()
        {
            currentShotParticleSystemIndex = 0;
            secondsSinceLastSystemChange = secondsBetweenShotSystemChanges;
        }

        /// <summary>
        /// Start the enemy ship's first attack
        /// </summary>
        private void Start()
        {
            PlayCurrentParticleSystem();
        }

        /// <summary>
        /// Updated the enemy's current attack and changes if needed
        /// </summary>
        private void Update()
        {
            secondsSinceLastSystemChange -= Time.deltaTime;

            if (secondsSinceLastSystemChange <= 0.0f)
            {
                // Reset timer
                secondsSinceLastSystemChange = secondsBetweenShotSystemChanges;

                StopCurrentParticleSystem();

                currentShotParticleSystemIndex++;

                // If we've gone past the end of the array restart
                if (currentShotParticleSystemIndex >= shotParticleSystems.Length)
                {
                    currentShotParticleSystemIndex = 0;
                }

                PlayCurrentParticleSystem();
            }
        }

        /// <summary>
        /// Gets the current shot particle system.
        /// </summary>
        /// <returns>The current particle system or null if not found.</returns>
        private ParticleSystem GetCurrentParticleSystem()
        {
            if (currentShotParticleSystemIndex < shotParticleSystems.Length)
            {
                return shotParticleSystems[currentShotParticleSystemIndex];
            }

            return null;
        }

        /// <summary>
        /// Stops the current particle system.
        /// </summary>
        private void StopCurrentParticleSystem()
        {
            ParticleSystem previousSystem = GetCurrentParticleSystem();

            if (previousSystem != null)
            {
                previousSystem.Stop();
            }
        }

        /// <summary>
        /// Plays the current particle system.
        /// </summary>
        private void PlayCurrentParticleSystem()
        {
            ParticleSystem nextSystem = GetCurrentParticleSystem();

            if (nextSystem != null)
            {
                nextSystem.Play();
            }
        }
    }
}