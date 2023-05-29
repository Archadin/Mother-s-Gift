using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource Music;
    [SerializeField] private SoundsSO AudioClipRefs;
    private float volume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMusicVolume()
    {
        volume = (volume + .1f) % 1.1f;
        Music.volume = volume;
    }

    public void ToggleMusic()
    {
        Music.mute = !Music.mute;
    }

    /// <summary>
    /// Sending true will mute the music.
    /// </summary>
    /// <param name="toggle"></param>
    public void ToggleMusic(bool toggle)
    {
        Music.mute = toggle;
    }

    public void PlayFootstepsEmpty()
    {
        int index = Random.Range(0, AudioClipRefs.footsteps.Length);
        PlaySound(AudioClipRefs.footsteps[index], transform.position, volume);
    }

    public void PlayInstrument(Vector3 position, float volumeMultiplier)
    {
        PlaySound(AudioClipRefs.instrument, position, volumeMultiplier * volume);
    }

    public void PlayGold(Vector3 position, float volumeMultiplier)
    {
        PlaySound(AudioClipRefs.gold, position, volumeMultiplier * volume);
    }

    public void PlayPickup(Vector3 position, float volumeMultiplier)
    {
        PlaySound(AudioClipRefs.pickup, position, volumeMultiplier * volume);
    }

    public void PlayTeleport(Vector3 position, float volumeMultiplier)
    {
        PlaySound(AudioClipRefs.teleport, position, volumeMultiplier * volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1.0f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }
}