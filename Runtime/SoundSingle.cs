using UnityEngine;

namespace PixelWizards.GameSystem.Audio
{
    /// <summary>
    /// Play specific AudioClip.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SoundSingle : BaseSound
    {
        [Range(0, 100)]
        public int chanceToPlay = 100;
        public AudioClip audioClip;
        public bool playOnEnable = false;
        public bool useRandomChance = false;

        private void OnEnable()
        {
            if (playOnEnable)
                Play();
        }

        public override void Play()
        {
            if (useRandomChance)
            {
                if (Random.Range(0, 100) <= chanceToPlay)
                {
                    PlayClip(audioClip);
                }
            }
            else
            {
                PlayClip(audioClip);
            }

        }
    }
}