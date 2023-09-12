using UnityEngine;
using UnityEngine.Serialization;

namespace GAME331.Lab03
{
    /// <summary>
    /// Controls the spinning behavior of a game object.
    /// </summary>
    public class SpinScript : MonoBehaviour
    {
        /// <summary>
        /// The current rotation angle of the game object.
        /// </summary>
        private float currentRotation;

        /// <summary>
        /// The rotation speed in degrees per second.
        /// </summary>
        [Tooltip("The rotation speed in degrees per second.")]
        public float rotationSpeed = 180.0f;

        // Start is called before the first frame update
        void Start()
        {
            currentRotation = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
            currentRotation += rotationSpeed * Time.deltaTime;

            // Set the local rotation of the game object around the Y-axis
            transform.localRotation = Quaternion.Euler(0.0f, currentRotation, 0.0f);
        }
    }
}