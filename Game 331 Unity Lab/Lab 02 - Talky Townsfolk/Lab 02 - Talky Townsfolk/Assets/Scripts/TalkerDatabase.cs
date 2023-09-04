using UnityEngine;
using UnityEngine.Serialization;

namespace GAME331.Lab02
{
    [System.Serializable]
    public class TalkerDataEntry
    {
        public int talkerId;
        public string talkerName;
        public string talkerText;
    }

    [System.Serializable]
    public class TalkerDataJSON
    {
        public TalkerDataEntry[] talkers;
    }

    /// <summary>
    /// Stores and retrieves talker data from JSON
    /// </summary>
    public class TalkerDatabase : MonoBehaviour
    {
        [Tooltip("The JSON data containing talker information.")]
        public TextAsset jsonData;

        private TalkerDataJSON talkerData;

        void Start()
        {
            string json = jsonData.text;
            talkerData = JsonUtility.FromJson<TalkerDataJSON>(json);
        }

        /// <summary>
        /// Retrieves the talker name associated with the specified talker ID.
        /// </summary>
        /// <param name="talkerId">The ID of the talker.</param>
        /// <returns>The talker's name, or "[NO_NAME_FOUND]" if not found.</returns>
        public string GetTalkerName(int talkerId)
        {
            for (int i = 0; i < talkerData.talkers.Length; ++i)
            {
                if (talkerData.talkers[i].talkerId == talkerId)
                {
                    return talkerData.talkers[i].talkerName;
                }
            }
            return "[NO_NAME_FOUND]";
        }

        /// <summary>
        /// Retrieves the talker text associated with the specified talker ID.
        /// </summary>
        /// <param name="talkerId">The ID of the talker.</param>
        /// <returns>The talker's text, or "[NO_TEXT_FOUND]" if not found.</returns>
        public string GetTalkerText(int talkerId)
        {
            for (int i = 0; i < talkerData.talkers.Length; ++i)
            {
                if (talkerData.talkers[i].talkerId == talkerId)
                {
                    return talkerData.talkers[i].talkerText;
                }
            }
            return "[NO_TEXT_FOUND]";
        }
    }
}