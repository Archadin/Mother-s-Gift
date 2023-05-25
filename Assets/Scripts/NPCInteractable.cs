using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [System.Serializable]
    private struct SimpleConversation
    {
        public string line;
        public bool resetConversation;
    }

    public event EventHandler OnInteractEvent;

    public event EventHandler OnLinesFinished;

    public UnityEvent OnConversationFinishedEvent;

    [SerializeField] private List<SimpleConversation> Lines = new List<SimpleConversation>();

    // a field for text and text buble. that asks for player input.

    private bool LinesFinished = false;

    private int LineIndex = 0;
    private int currentLine;

    private bool canInteract = false;

    public void Interact()
    {
        if (Lines.Count == 0) return;
        if (!canInteract) return;

        OnInteractEvent?.Invoke(this, EventArgs.Empty);
        currentLine = Lines[currentLine].resetConversation ? 0 : currentLine + 1;

        if (currentLine > Lines.Count - 1)
        {
            currentLine = Lines.Count - 1;
            LinesFinished = true;
            OnLinesFinished?.Invoke(this, EventArgs.Empty);
            OnConversationFinishedEvent?.Invoke();
        }
    }

    public void SetCanInteract(bool enabled)
    {
        canInteract = enabled;
    }

    public string GetCurrentLine()
    {
        if (LinesFinished) return null;
        return Lines[currentLine].line;
    }
}