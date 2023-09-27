using System;
using TMPro;
using UnityEngine;

namespace GAME331.Lab04
{
    /// <summary>
    /// Controls the flocking behavior mode and hazard avoidance for the flockers.
    /// </summary>
    public class FlockingLogicSwitcher : MonoBehaviour
    {
        /// <summary>
        /// The current flocking mode for the flockers.
        /// </summary>
        public FlockerScript.FlockingMode currentFlockingMode = FlockerScript.FlockingMode.ChaseTarget;

        /// <summary>
        /// Determines whether flockers should avoid hazards.
        /// </summary>
        public bool flockersAvoidHazards = true;

        /// <summary>
        /// The UI text element to display the current status.
        /// </summary>
        public TextMeshProUGUI statusText;

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
            HandleInput();

            // Update status text if available
            if (statusText != null)
            {
                statusText.text = "Flocking Mode: <b>" + currentFlockingMode.ToString() + "</b>\n" +
                    "Avoid Hazards: <b>" + flockersAvoidHazards.ToString() + "</b>";
            }
        }

        /// <summary>
        /// Handles keyboard input for changing flocking mode and toggling hazard avoidance.
        /// </summary>
        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetFlockingMode(FlockerScript.FlockingMode.ChaseTarget);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetFlockingMode(FlockerScript.FlockingMode.FleeTarget);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetFlockingMode(FlockerScript.FlockingMode.MaintainDistance);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SetFlockingMode(FlockerScript.FlockingMode.DoNothing);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                ToggleHazardAvoidance();
            }
        }

        /// <summary>
        /// Sets the flocking mode for all flockers.
        /// </summary>
        /// <param name="desiredFlockingMode">The desired flocking mode.</param>
        private void SetFlockingMode(FlockerScript.FlockingMode desiredFlockingMode)
        {
            currentFlockingMode = desiredFlockingMode;
            FlockerScript[] AllFlockers = FlockerScript.GetAllFlockers();
            for (int i = 0; i < AllFlockers.Length; ++i)
            {
                AllFlockers[i].currentFlockingMode = currentFlockingMode;
            }
        }

        /// <summary>
        /// Toggles hazard avoidance for all flockers.
        /// </summary>
        private void ToggleHazardAvoidance()
        {
            flockersAvoidHazards = !flockersAvoidHazards;
            FlockerScript[] AllFlockers = FlockerScript.GetAllFlockers();
            for (int i = 0; i < AllFlockers.Length; ++i)
            {
                AllFlockers[i].avoidHazards = flockersAvoidHazards;
            }
        }


    }
}
