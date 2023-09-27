using UnityEngine;
using UnityEngine.Serialization;

namespace GAME331.Lab05
{
    /// <summary>
    /// Manages the state of the game world, including spawning and saving pickups.
    /// </summary>
    public class WorldStateManager : MonoBehaviour
    {
        /// <summary>
        /// Reference to the player movement component.
        /// </summary>
        public PlayerMovement player;

        /// <summary>
        /// The desired number of pickups to be spawned in the world.
        /// </summary>
        public int desiredPickupCount = 5;

        /// <summary>
        /// Prefabs of different pickup items.
        /// </summary>
        [FormerlySerializedAs("PickupPrefabs")]
        public GameObject[] pickupPrefabs;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        void Start()
        {
            if (!player)
            {
                player = FindFirstObjectByType<PlayerMovement>();
            }

            SaveDataScript.Load();

            // Load the player's collected apples count from the saved data.
            player.appleCount = SaveDataScript.mySaveData.appleCount;
            player.orangeCount = SaveDataScript.mySaveData.orangeCount;
            player.transform.position = SaveDataScript.mySaveData.playerPosition;

            // Instantiate pickups from saved data.
            foreach (SaveDataScript.PickupSaveData pickupSaveData in SaveDataScript.mySaveData.pickups)
            {
                int pickupIndex = GetIndexFromItemType(pickupSaveData.myItemType);
                GameObject objectToSpawn = pickupPrefabs[pickupIndex];
                Instantiate(objectToSpawn, pickupSaveData.myPosition, Quaternion.identity);
            }
        }

        /// <summary>
        /// Takes care of saving if needed
        /// </summary>
        void Update()
        {
            SpawnFruit();

            if (Input.GetKeyDown(KeyCode.P))
            {
                // Save the player's collected apples count to the save data.
                SaveDataScript.mySaveData.appleCount = player.appleCount;
                SaveDataScript.mySaveData.orangeCount = player.orangeCount;
                SaveDataScript.mySaveData.playerPosition = player.transform.position;

                // Save the pickup items to the save data.
                SaveDataScript.mySaveData.pickups.Clear();
                PickupItem[] pickups = FindObjectsByType<PickupItem>(FindObjectsSortMode.None);
                for (int i = 0; i < pickups.Length; ++i)
                {
                    SaveDataScript.PickupSaveData pickupSave = new SaveDataScript.PickupSaveData();
                    pickupSave.myItemType = pickups[i].myItemType;
                    pickupSave.myPosition = pickups[i].transform.position;
                    SaveDataScript.mySaveData.pickups.Add(pickupSave);
                }

                SaveDataScript.Save();
            }
        }

        /// <summary>
        /// Spawns additional fruit pickups if needed to reach the desired count.
        /// </summary>
        void SpawnFruit()
        {
            if (pickupPrefabs.Length > 0)
            {
                PickupItem[] pickups = FindObjectsByType<PickupItem>(FindObjectsSortMode.None);
                int pickupsToSpawn = desiredPickupCount - pickups.Length;
                for (int i = 0; i < pickupsToSpawn; ++i)
                {
                    int randomIndex = Random.Range(0, pickupPrefabs.Length);
                    GameObject objectToSpawn = pickupPrefabs[randomIndex];
                    Instantiate(objectToSpawn, GetRandomFruitPos(), Quaternion.identity);
                }
            }
        }

        private Vector3 GetRandomFruitPos()
        {
            return Random.insideUnitSphere * 4.0f + new Vector3(0.0f, 6.0f, 0.0f);
        }

        /// <summary>
        /// Returns the index of a pickup prefab based on its item type.
        /// </summary>
        /// <param name="itemType">The item type of the pickup.</param>
        /// <returns>The index of the matching pickup prefab or 0 if not found.</returns>
        int GetIndexFromItemType(PickupItem.ItemType itemType)
        {
            for (int i = 0; i < pickupPrefabs.Length; ++i)
            {
                if (itemType == pickupPrefabs[i].GetComponent<PickupItem>().myItemType)
                {
                    return i;
                }
            }
            return 0;
        }

    }
}
