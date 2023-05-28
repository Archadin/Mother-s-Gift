using System;
using System.Collections.Generic;
using UnityEngine.Events;

public interface IInteractable
{
    public void Interact();
}

[Serializable]
public struct Conversation
{
    public string title;
    public bool isLocked;
    public List<string> lines;
    public bool converationRepeats;
    public bool conversationEnded;
    public UnityEvent OnConversationFinishedEvent;
}
