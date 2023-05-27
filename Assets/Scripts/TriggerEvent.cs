using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    private bool firstTrigger;
    public UnityEvent OnTrigerredFirstTime;

    [SerializeField] private Transform exitPosition;
    [SerializeField] private bool canTravel = true;
    [SerializeField] private string denyMessage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            //change scene depending on the current scene.
            if (canTravel)
            {
                if (!firstTrigger && OnTrigerredFirstTime != null)
                {
                    firstTrigger = true;
                    OnTrigerredFirstTime?.Invoke();
                }

                TeleportManager.Instance.LoadScene(exitPosition);
            }
            else
            {
                Debug.Log(denyMessage);
            }
        }
    }

    public void SetCanTravel(bool enabled)
    {
        canTravel = enabled;
    }
}