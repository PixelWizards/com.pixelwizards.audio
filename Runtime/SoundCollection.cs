using System.Collections.Generic;
using UnityEngine;

namespace PixelWizards.GameSystem.Audio
{
    /// <summary>
    /// Contains all sound object which can be played in this context.
    /// It loads all sound component from provided GameObject.
    /// </summary>
    public class SoundCollection : MonoBehaviour
    {
        protected Dictionary<string, BaseSound> baseSounds = new Dictionary<string, BaseSound>();
        private bool initialized = false;

        private void Start()
        {
            GetSoundsFromObject(gameObject);
            initialized = true;
        }

        /// <summary>
        /// Gets all BaseSound components from provided GameObject
        /// </summary>
        /// <param name="target">GameObject containing all the sounds</param>
        private void GetSoundsFromObject(GameObject target)
        {
            var sounds = target.GetComponentsInChildren<BaseSound>();
            foreach (var sound in sounds)
            {
                if (!string.IsNullOrEmpty(sound.key))
                {
                    baseSounds[sound.key] = sound;
                }
            }
        }

        /// <summary>
        /// Plays sounds by their key name
        /// </summary>
        /// <param name="key">Key of the sound</param>
        public void PlaySound(string key)
        {
            if (!initialized)
                Init();

            BaseSound sound;
            if (baseSounds.TryGetValue(key, out sound))
            {
                sound.Play();
            }
        }
    }
}