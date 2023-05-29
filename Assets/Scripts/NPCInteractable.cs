using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    public event EventHandler OnInteractEvent;

    public event EventHandler OnLinesFinished;

    [SerializeField] private List<Conversation> Conversations = new List<Conversation>();
    private Conversation currentConv;
    // a field for text and text buble. that asks for player input.

    private int LineIndex = 0;
    private int conversationIndex = 0;

    [SerializeField] private bool canInteract = false;

    public UnityEvent NPCQuestFinishedEvent;

    public void Interact()
    {
        if (Conversations.Count == 0) return;
        if (!canInteract) return;
        // get current conversation
        currentConv = Conversations[conversationIndex];

        if (!currentConv.isLocked)
        {
            PlayerMovement.Instance.DisableMovement();

            //if the currentLine is higher than the count of lines in the conversation finish it.
            if (LineIndex > currentConv.lines.Count - 1)
            {
                currentConv.conversationEnded = true;
                Conversations[conversationIndex] = currentConv;
                PlayerMovement.Instance.EnableMovement();
                LineIndex = 0;
                if (!currentConv.converationRepeats || conversationIndex < Conversations.Count - 1)
                {
                    conversationIndex++;
                }
                OnLinesFinished?.Invoke(this, EventArgs.Empty);
                currentConv.OnConversationFinishedEvent?.Invoke();
                return;
            }
            OnInteractEvent?.Invoke(this, EventArgs.Empty);

            LineIndex++;
        }
    }

    public void SetAndUnlockConversation(int index)
    {
        conversationIndex = index;
        currentConv = Conversations[conversationIndex];
        currentConv.isLocked = false;
        Conversations[conversationIndex] = currentConv;
    }

    public void CheckQuestComplete(QuestSO questSO)
    {
        if (questSO.isComplete)
        {
            NPCQuestFinishedEvent?.Invoke();
        }
    }

    public void SetCanInteract(bool enabled)
    {
        canInteract = enabled;
    }

    public string GetCurrentLine()
    {
        if (currentConv.conversationEnded) return null;
        if (currentConv.isLocked) return null;
        // if conversation is not finished. return the line.
        if (Conversations[conversationIndex].conversationEnded) return null;
        return Conversations[conversationIndex].lines[LineIndex];
    }
}