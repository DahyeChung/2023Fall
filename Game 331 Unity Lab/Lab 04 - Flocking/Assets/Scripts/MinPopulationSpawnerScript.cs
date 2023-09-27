using UnityEngine;

namespace GAME331.Lab04
{
    /// <summary>
    /// Spawns objects with flocking behavior based on a minimum population count.
    /// </summary>
    public class MinPopulationSpawnerScript : MonoBehaviour
    {
        [Header("Spawner Settings")]
        [Tooltip("The number of objects to spawn when the population is below the minimum.")]
        public int spawnCount = 10;

        [Header("Spawn Ranges")]
        [Tooltip("The X-axis range for spawning objects.")]
        public Vector2 spawnRangeX = new Vector2(-4.0f, 4.0f);

        [Tooltip("The Y-axis range for spawning objects.")]
        public Vector2 spawnRangeY = new Vector2(0.0f, 0.0f);

        [Tooltip("The Z-axis range for spawning objects.")]
        public Vector2 spawnRangeZ = new Vector2(-4.0f, 4.0f);

        [Header("Prefab Settings")]
        [Tooltip("The prefab object to spawn.")]
        public GameObject objectToSpawn;

        [Header("Flocking Settings")]
        [Tooltip("The target game object for flocking behavior.")]
        public GameObject flockingTarget;

        [Tooltip("The flocking logic switcher.")]
        public FlockingLogicSwitcher flockingLogic;

        private float spawnCooldownSeconds;

        // Start is called before the first frame update
        void Start()
        {
            spawnCooldownSeconds = 0.0f;
        }

        // FixedUpdate is called at a fixed time interval
        void FixedUpdate()
        {
            spawnCooldownSeconds -= Time.deltaTime;

            if (spawnCooldownSeconds <= 0.0f)
            {
                DoSpawn();
                spawnCooldownSeconds = 0.5f;
            }
        }

        /// <summary>
        /// Spawns objects if the population count is below the minimum.
        /// </summary>
        void DoSpawn()
        {
            int alreadySpawned = FlockerScript.GetAllFlockers().Length;
            int countToSpawn = spawnCount - alreadySpawned;

            if (countToSpawn > 0)
            {
                Vector3 position = transform.position +
                    new Vector3(
                        Random.Range(spawnRangeX.x, spawnRangeX.y),
                        Random.Range(spawnRangeY.x, spawnRangeY.y),
                        Random.Range(spawnRangeZ.x, spawnRangeZ.y)
                    );

                GameObject spawnedObject = Instantiate(objectToSpawn, position, Quaternion.identity);
                if (spawnedObject != null)
                {
                    FlockerScript flocking = spawnedObject.GetComponent<FlockerScript>();
                    if (flocking != null)
                    {
                        flocking.flockingTarget = flockingTarget;
                        flocking.currentFlockingMode = flockingLogic.currentFlockingMode;
                        flocking.avoidHazards = flockingLogic.flockersAvoidHazards;
                    }
                }
            }
        }
    }
}
