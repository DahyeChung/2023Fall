using UnityEngine;
using UnityEngine.Serialization;

namespace GAME331.Lab04
{
    /// <summary>
    /// /// <summary>
    /// Controls the flocking behavior of attached game objects.
    /// </summary>
    /// </summary>
    public class FlockerScript : MonoBehaviour
    {
        /// <summary>
        /// Flocking mode options.
        /// </summary>
        public enum FlockingMode
        {
            ChaseTarget,
            FleeTarget,
            MaintainDistance,
            DoNothing
        }

        [Tooltip("The speed per second at which the flocker moves.")]
        public float speedPerSecond = 4.0f;

        /// <summary>
        /// The target game object the flocker chases or flees from
        /// </summary>
        [HideInInspector] // Set in code, so shouldn't be seen in the inspector
        public GameObject flockingTarget;

        [Tooltip("The current flocking behavior mode.")]
        public FlockingMode currentFlockingMode = FlockingMode.ChaseTarget;

        [Tooltip("The minimum desired distance from the target.")]
        public float minDistanceToTarget = 3.5f;

        [Tooltip("The maximum desired distance from the target.")]
        public float maxDistanceToTarget = 4.5f;

        [Tooltip("Determines if the flocker should avoid hazards.")]
        public bool avoidHazards = true;

        /// <summary>
        /// Performs the flocking behavior update.
        /// </summary>
        /// <remarks>
        /// The flocking behavior includes chasing the target, fleeing from the target, maintaining a distance from the target,
        /// and avoiding hazards if enabled.
        /// </remarks>
        void Update()
        {
            Vector3 desiredDirection = new Vector3();

            Vector3 vectorToTarget = flockingTarget.transform.position - transform.position;
            float distanceToTarget = vectorToTarget.magnitude;

            switch (currentFlockingMode)
            {
                case FlockingMode.ChaseTarget:
                    desiredDirection = vectorToTarget;          //Move towards target
                    break;

                case FlockingMode.FleeTarget:
                    //LAB TASK #1: Implement FleeTarget
                    //TODO: Move away from target
                    //HINT: Set desiredDirection = to a mathmatical operation on vectorToTarget
                    break;

                case FlockingMode.MaintainDistance:
                    {
                        //LAB TASK #2: Implement MaintainDistance
                        //TODO: If distance to target is less than minDistanceToTarget, move away from target
                        //TODO: Otherwise, if distance to target is more than maxDistanceToTarget, move towards target
                        //HINT: Start with an if/else statment with a branch for the move away condition and another for move away
                        //HINT: A typical if/else statement looks like
                        // if (your_condition_goes_here)
                        // {
                        //    code_for_when_codition_is_true
                        // }
                        // else
                        // {
                        //    code_for_when_codition_is_false
                        // }
                    }
                    break;

                case FlockingMode.DoNothing:
                    desiredDirection = Vector3.zero;
                    break;
            }

            if (avoidHazards)
            {
                HazardScript[] hazards = FindObjectsByType<HazardScript>(FindObjectsSortMode.None);

                Vector3 avoidanceVector = Vector3.zero;
                for (int i = 0; i < hazards.Length; ++i)
                {
                    Vector3 vectorToHazard = hazards[i].transform.position - transform.position;
                    if (vectorToHazard.magnitude < 4.0f)
                    {
                        Vector3 vectorAwayFromHazard = -vectorToHazard;
                        //LAB TASK #3: Implement hazard avoidance, part 1
                        //TODO: Accumulate vectors away from hazards in avoidance vector
                        //HINT: This loop runs once for every hazard in the level
                        //HINT: Try setting avoidanceVector to itself plus a vector pointing away from a hazard
                    }
                }

                if (avoidanceVector != Vector3.zero)
                {
                    desiredDirection.Normalize();
                    avoidanceVector.Normalize();

                    //LAB TASK #4: Implement hazard avoidance, part 2
                    //TODO: Set the value of desiredDirection to 50% desiredDirection and 50% avoidanceVector
                    //HINT: Set desiredDirection = a mathmatical formula sums half of desiredDirection and half of avoidanceVector
                }
            }

            desiredDirection.Normalize();
            transform.position += desiredDirection * speedPerSecond * Time.deltaTime;
        }

        /// <summary>
        /// Helper script to allow user to get all of flockers in the scene
        /// </summary>
        /// <returns>An array of all objects with the FlockerScript attached</returns>
        public static FlockerScript[] GetAllFlockers()
        {
            return GameObject.FindObjectsByType<FlockerScript>(FindObjectsSortMode.None);
        }
    }
}

