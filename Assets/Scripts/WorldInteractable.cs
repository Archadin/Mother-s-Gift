using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Conversation> messages = new List<Conversation>();
    private string Message;
    private UnityEvent OnInteract;
    private bool canInteract;

    public void Interact()
    {
        if (canInteract)
        {
            OnInteract?.Invoke();
        }
    }
}