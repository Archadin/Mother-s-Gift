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
    }

    public event EventHandler OnInteractEvent;

    public event EventHandler OnLinesFinished;

    public UnityEvent OnConversationFinishedEvent;

    [SerializeField] private List<Conversation> Conversations = new List<Conversation>();
    [SerializeField] private List<List<Line>> Lines2 = new List<List<Line>>();

    // a field for text and text buble. that asks for player input.

    private int LineIndex = 0;
    private int conversationIndex = 0;

    [SerializeField] private bool canInteract = false;

    public void Interact()
    {
        if (Conversations.Count == 0) return;
        if (!canInteract) return;
        // get current conversation
        Debug.LogWarning("conv:" + conversationIndex);
        Conversation currentConv = Conversations[conversationIndex];

        //if the currentLine is higher than the count of lines in the conversation finish it.
        print(LineIndex > currentConv.lines.Count - 1);
        if (LineIndex > currentConv.lines.Count - 1)
        {
            print("finished");
            currentConv.FinishedConverstaion = true;
            LineIndex = 0;
            if (!currentConv.converationRepeats)
            {
                conversationIndex++;
            }
            OnLinesFinished?.Invoke(this, EventArgs.Empty);
            OnConversationFinishedEvent?.Invoke();
            return;
        }
        OnInteractEvent?.Invoke(this, EventArgs.Empty);

        Debug.LogWarning("line: " + LineIndex);
        LineIndex++;
        Debug.LogWarning("line after: " + LineIndex);
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