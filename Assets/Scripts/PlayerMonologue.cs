using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonologue : MonoBehaviour
{
    public event EventHandler OnMonologue;

    public event EventHandler OnMonologueEnded;

    [SerializeField] private bool canMonologue;
    [SerializeField] private List<Conversation> monologue = new List<Conversation>();
    private Conversation currentConv;
    private int lineIndex;
    private int monologueIndex;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Monologue();
        }
    }

    private void Monologue()
    {
        if (monologue.Count == 0) return;
        if (!canMonologue) return;
        // get current conversation
        currentConv = monologue[monologueIndex];

        if (!currentConv.isLocked)
        {
            PlayerMovement.Instance.DisableMovement();

            //if the currentLine is higher than the count of lines in the conversation finish it.
            if (lineIndex > currentConv.lines.Count - 1)
            {
                currentConv.conversationEnded = true;
                PlayerMovement.Instance.EnableMovement();
                OnMonologueEnded?.Invoke(this, EventArgs.Empty);
                canMonologue = false;
                currentConv.OnConversationFinishedEvent?.Invoke();
                return;
            }
            OnMonologue?.Invoke(this, EventArgs.Empty);

            lineIndex++;
        }
    }

    public void SetMonologueIndex(int index)
    {
        monologueIndex = index;
        lineIndex = 0;
    }

    public void ToggleMonologue(bool enabled)
    {
        canMonologue = enabled;
    }

    public void StartMonologue()
    {
        ToggleMonologue(true);
        Monologue();
    }

    public string GetCurrentLine()
    {
        if (currentConv.conversationEnded) return null;
        if (currentConv.isLocked) return null;
        // if conversation is not finished. return the line.
        if (monologue[monologueIndex].conversationEnded) return null;
        return monologue[monologueIndex].lines[lineIndex];
    }
}