using UnityEngine;

[CreateAssetMenu(fileName = "SoundsSO", menuName = "ScriptableObjects/Sounds")]
public class SoundsSO : ScriptableObject
{
    public AudioClip[] footsteps;
    public AudioClip instrument;
    public AudioClip pickup;
    public AudioClip menu;
    public AudioClip teleport;
    public AudioClip gold;
}