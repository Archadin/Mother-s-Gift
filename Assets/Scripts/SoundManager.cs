using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

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

    public void PlayFootsteps()
    {
        int index = Random.Range(0, AudioClipRefs.footsteps.Length);
        PlaySound(AudioClipRefs.footsteps[index], transform.position, volume);
    }

    public void PlayTeleport(Vector3 position, float volumeMultiplier)
    {
        PlaySound(AudioClipRefs.teleport, position, volumeMultiplier * volume);
    }

    public void PlayFootsteps(Vector3 position, float volumeMultiplier)
    {
        int index = Random.Range(0, AudioClipRefs.footsteps.Length);
        PlaySound(AudioClipRefs.footsteps[index], position, volumeMultiplier * volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1.0f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }
}