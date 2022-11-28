using UnityEngine;

namespace PixelWizards.GameSystem.Audio
{

    /// <summary>
    /// Randomly chooses which sound to play from multiple AudioClips.
    /// </summary>
    public class SoundMulti : BaseSound
    {
        [Range(0, 100)]
        public int chanceToPlay = 100;
        public AudioClip[] audioClips;

        public override void Play()
        {
            if (audioClips.Length == 0)
            {
                return;
            }

            if (Random.Range(0, 100) <= chanceToPlay)
            {
                PlayClip(audioClips[Random.Range(0, audioClips.Length)]);
            }

        }
    }
}