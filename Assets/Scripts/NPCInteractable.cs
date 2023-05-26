using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [System.Serializable]
    private struct Line
    {
        public string line;
    }

    [System.Serializable]
    private struct Conversation
    {
        public List<Line> lines;
        public bool converationRepeats;
        public bool FinishedConverstaion;
        public UnityEvent OnConversationFinishedEvent;
    }

    public event EventHandler OnInteractEvent;

    public event EventHandler OnLinesFinished;

    [SerializeField] private List<Conversation> Conversations = new List<Conversation>();

    // a field for text and text buble. that asks for player input.

    private int LineIndex = 0;
    private int conversationIndex = 0;

    [SerializeField] private bool canInteract = false;

    public void Interact()
    {
        if (Conversations.Count == 0) return;
        if (!canInteract) return;
        CharacterController.Instance.DisableMovement();
        // get current conversation
        Debug.LogWarning("conv:" + conversationIndex);
        Conversation currentConv = Conversations[conversationIndex];

        //if the currentLine is higher than the count of lines in the conversation finish it.
        print(LineIndex > currentConv.lines.Count - 1);
        if (LineIndex > currentConv.lines.Count - 1)
        {
            print("finished");
            currentConv.FinishedConverstaion = true;
            CharacterController.Instance.EnableMovement();
            LineIndex = 0;
            if (!currentConv.converationRepeats || Conversations.Count > 1)
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

    public void SetCanInteract(bool enabled)
    {
        canInteract = enabled;
    }

    public string GetCurrentLine()
    {
        // if conversation is not finished. return the line.
        if (Conversations[conversationIndex].FinishedConverstaion) return null;
        return Conversations[conversationIndex].lines[LineIndex].line;
    }
}