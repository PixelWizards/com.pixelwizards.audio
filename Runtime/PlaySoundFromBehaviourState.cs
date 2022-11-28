using UnityEngine;

namespace PixelWizards.GameSystem.Audio
{

    /// <summary>
    /// Plays sounds at specific lifetime of a MonoBehaviour.
    /// </summary>
    public class PlaySoundFromBehaviourState : MonoBehaviour
    {
        public BaseSound sound;

        public enum State
        {
            Awake = 0,
            Start = 1,
            OnEnable = 2,
            OnDisable = 3
        }

        public State state;


        void Awake()
        {
            OnStateChange(State.Awake);
        }

        void Start()
        {
            OnStateChange(State.Start);
        }

        void OnEnable()
        {
            OnStateChange(State.OnEnable);
        }

        void OnDisable()
        {
            OnStateChange(State.OnDisable);
        }

        private void OnStateChange(State newState)
        {
            if (newState == state && sound != null)
            {
                sound.Play();
            }
        }
    }
}