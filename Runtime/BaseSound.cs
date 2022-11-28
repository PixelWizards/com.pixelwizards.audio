using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelWizards.GameSystem.Audio
{

    /// <summary>
    /// Abstract base class for all sounds objects.
    /// It needs to know which AudioSource to use to play sounds.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public abstract class BaseSound : MonoBehaviour
    {
        public AudioSource audioSource;

        public string key;
        [Tooltip("If TRUE, only certain amount of concurrent instances of the sound with the same key will be played at once.")]
        public bool limitInstanceCount = true;

        public static Dictionary<string, int> playingSounds = new Dictionary<string, int>();
        public const int MAX_INSTANCE_COUNT = 4;
        private bool isRegistered;

        void Awake()
        {
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }

            if (audioSource == null)
            {
                Debug.LogError("Missing AudioSource on: " + this);
            }
        }

        /// <summary>
        /// Internal function to play specific AudioClip.
        /// Called from subclasses.
        /// </summary>
        /// <param name="clip">AudioClip to play</param>
        protected void PlayClip(AudioClip clip)
        {
            if (clip == null && audioSource != null)
            {
                Debug.LogWarning("Clip passed to Play is null in: " + this);

                return;
            }

            // If we limit by instance count, check if there is not enough sounds
            // of the same type already playing.
            if (limitInstanceCount)
            {
                if (RegisterSound())
                {
                    StartCoroutine(UnregisterSoundCoroutine(clip));
                }
                else
                {
                    return;
                }
            }

            audioSource.PlayOneShot(clip);
        }

        /// <summary>
        /// Tries to incremenent number of currenly playing
        /// sound instances with the key.
        /// </summary>
        /// <returns>TRUE if the sound was registered and can be played.</returns>
        private bool RegisterSound()
        {
            int count;
            if (playingSounds.TryGetValue(key, out count))
            {
                if (count > MAX_INSTANCE_COUNT)
                {
                    return false;
                }

                playingSounds[key] = count + 1;
            }
            else
            {
                playingSounds.Add(key, 1);
            }

            isRegistered = true;

            return true;
        }

        void OnDisable()
        {
            UnregisterSound();
        }

        /// <summary>
        /// Decreases number of currenly playing sound instances.
        /// </summary>
        private void UnregisterSound()
        {
            if (!isRegistered)
            {
                return;
            }

            int count;
            if (playingSounds.TryGetValue(key, out count))
            {
                if (count > 0)
                {
                    playingSounds[key] = count - 1;
                }
            }

            isRegistered = false;
        }

        /// <summary>
        /// Unregisters the AudioClip after it's finished playing.
        /// </summary>
        /// <param name="clip">AudioClip to register</param>
        private IEnumerator UnregisterSoundCoroutine(AudioClip clip)
        {
            yield return new WaitForSeconds(clip.length);

            UnregisterSound();
        }

        /// <summary>
        /// Implement this to choose which AudioClip to play.
        /// </summary>
        public abstract void Play();
    }
}