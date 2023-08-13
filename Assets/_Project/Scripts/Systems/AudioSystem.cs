using UnityEngine;
using Utils;

namespace dragoni7
{
    public class AudioSystem : Singleton<AudioSystem>
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource soundsSource;

        public void PlayMusic(AudioClip clip)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }

        public void PlaySound(AudioClip clip, Vector2 pos, float vol = 1)
        {
            soundsSource.transform.position = pos;
            PlaySound(clip, vol);
        }

        public void PlaySound(AudioClip clip, float vol = 1)
        {
            soundsSource.PlayOneShot(clip, vol);
        }
    }
}
