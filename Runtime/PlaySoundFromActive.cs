using UnityEngine;

namespace PixelWizards.GameSystem.Audio
{

    /// <summary>
    /// Plays sound when target GameObject becomes active.
    /// </summary>
    public class PlaySoundFromActive : MonoBehaviour
    {
        public GameObject active;
        public BaseSound sound;

        private bool lastState;

        void OnEnable()
        {
            SetState(CalculateNewState());
        }

        void Update()
        {
            bool newState = CalculateNewState();
            if (lastState != newState)
            {
                SetState(newState);
            }
        }

        private bool CalculateNewState()
        {
            return active.activeInHierarchy;
        }

        private void SetState(bool newState)
        {
            lastState = newState;

            if (newState && sound != null)
            {
                sound.Play();
            }
        }
    }
}