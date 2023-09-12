using UnityEngine;

namespace GAME331.Lab03
{
    /// <summary>
    /// Makes the GameObject this script is attached to face towards a target Transform.
    /// </summary>
    public class FaceTowardTarget : MonoBehaviour
    {

        [Tooltip("The target Transform to face towards.")]
        public Transform targetToFace;

        /// <summary>
        /// Face the target
        /// </summary>
        private void Update()
        {
            if (targetToFace != null)
            {
                transform.LookAt(targetToFace);
            }
        }
    }
}