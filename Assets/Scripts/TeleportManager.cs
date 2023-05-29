using System;
using System.Collections;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public event EventHandler OnTeleportEvent;

    public static TeleportManager Instance;
    [SerializeField] private PlayerMovement player;

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

   

    private void Teleport(Transform transform)
    {
        if (player == null) return;

        player.transform.position = transform.position;
        SoundManager.Instance.PlayTeleport(player.transform.position, 1);

        OnTeleportEvent?.Invoke(this, EventArgs.Empty);
    }

    public void LoadScene(Transform transform)
    {
        Teleport(transform);
    }
}