using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GAME331.Lab05
{
    /// <summary>
    /// A class responsible for saving and loading game data.
    /// </summary>
    public class SaveDataScript : MonoBehaviour
    {
        /// <summary>
        /// Represents the data for a single pickup item.
        /// </summary>
        [System.Serializable]
        public class PickupSaveData
        {
            /// <summary>
            /// The type of the pickup item.
            /// </summary>
            public PickupItem.ItemType myItemType;

            /// <summary>
            /// The position of the pickup item in the game world.
            /// </summary>
            public Vector3 myPosition;
        }

        /// <summary>
        /// Represents the complete game save data.
        /// </summary>
        [System.Serializable]
        public class SaveData
        {
            /// <summary>
            /// Initializes a new instance of the SaveData class.
            /// </summary>
            public SaveData()
            {
                appleCount = 0;
                orangeCount = 0;
                pickups = new List<PickupSaveData>();
            }

            /// <summary>
            /// The number of apples collected.
            /// </summary>
            public int appleCount;
            public int orangeCount;

            public Vector3 playerPosition;

            /// <summary>
            /// List of pickup items saved in the game.
            /// </summary>
            public List<PickupSaveData> pickups;
        }

        private const string SAVE_DATA_FILENAME = "LabSave.json";
        private const string SAVE_DATA_BACKUP_FILENAME = "LabSaveBackup.json";

        /// <summary>
        /// The current game save data.
        /// </summary>
        public static SaveData mySaveData;

        /// <summary>
        /// Load game data from the persistent data path, and create a default save 
        /// if loading fails.
        /// </summary>
        public static void Load()
        {
            LoadFromPath(Application.persistentDataPath + "/" + SAVE_DATA_FILENAME);

            if (mySaveData != null)
            {
                // Save succeeded, so save loaded file as backup
                SaveToPath(Application.persistentDataPath + "/" + SAVE_DATA_BACKUP_FILENAME);
            }
            else
            {
                // Couldn't load primary save file, try loading backup
                LoadFromPath(Application.persistentDataPath + "/" + SAVE_DATA_BACKUP_FILENAME);
            }

            if (mySaveData == null)
            {
                // Couldn't load primary or backup save data, create a default save
                mySaveData = new SaveData();
            }
        }

        /// <summary>
        /// Save the current game data to the persistent data path.
        /// </summary>
        public static void Save()
        {
            SaveToPath(Application.persistentDataPath + "/" + SAVE_DATA_FILENAME);
        }

        private static void LoadFromPath(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    string jsonString = File.ReadAllText(path);
                    mySaveData = JsonUtility.FromJson<SaveData>(jsonString);
                }
                catch (System.Exception e)
                {
                    Debug.Log("Failed to load save: " + e.ToString());
                    mySaveData = null;
                }
            }
        }

        private static void SaveToPath(string path)
        {
            try
            {
                string jsonString = JsonUtility.ToJson(mySaveData);
                File.WriteAllText(path, jsonString);
                Debug.Log("Save Complete! File: " + path);
            }
            catch (System.Exception e)
            {
                Debug.Log("Error saving file: " + e.ToString());
            }
        }
    }
}
