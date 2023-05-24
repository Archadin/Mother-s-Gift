using System;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private List<SimpleConversation> Lines = new List<SimpleConversation>();

    // a field for text and text buble. that asks for player input.
    private bool hasSpoken = false;

    private bool LinesFinished = false;

    private int currentLine;

    public void Interact()
    {
        if (Lines.Count == 0) return;

        if (LinesFinished)
        {
        }

        hasSpoken = currentLine > 0;
        OnInteractEvent?.Invoke(this, EventArgs.Empty);
        currentLine = Lines[currentLine].resetConversation ? 0 : currentLine + 1;

        if (currentLine > Lines.Count - 1)
        {
            currentLine = Lines.Count - 1;
            LinesFinished = true;
            OnLinesFinished?.Invoke(this, EventArgs.Empty);
        }
    }

    public string GetCurrentLine()
    {
        if (LinesFinished) return null;
        return Lines[currentLine].line;
    }
}