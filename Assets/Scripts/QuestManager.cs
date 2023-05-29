using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public class QuestAcceptedEventEventArgs : EventArgs
    {
        public QuestSO acceptedQuest;
    }

    public event EventHandler<QuestAcceptedEventEventArgs> OnQuestAccepted;
    public event EventHandler<QuestAcceptedEventEventArgs> OnQuestDropped;

    public event EventHandler OnQuestUpdated;

    public UnityEvent OnQuestCompleted;

    public static QuestManager Instace;
    [SerializeField] private List<QuestSO> acceptedQuests = new List<QuestSO>();

    private void Awake()
    {
        Instace = this;
    }

    public bool CheckQuestProgress(QuestItemSO questItem)
    {
        foreach (var quest in acceptedQuests)
        {
            if (quest.requiredItems.Contains(questItem))
            {
                if (!quest.isActive) return false;
                if (quest.CheckQuestComplete())
                {
                    if (quest.isComplete)
                    {
                        OnQuestCompleted?.Invoke();
                        //SoundManager.Instance.PlayInstrument(transform.position, 1);
                    }
                }
                return true;
            }
        }
        return false;
    }

    public void QuestUpdated()
    {
        OnQuestUpdated?.Invoke(this, EventArgs.Empty);
    }

    public void ActivateQuests(QuestSO quest)
    {
        if (quest.isActive) return;
        quest.isActive = true;
        OnQuestAccepted?.Invoke(this, new QuestAcceptedEventEventArgs { acceptedQuest = quest });
    }


    public void DeactivateQuests(QuestSO quest)
    {
        if (!quest.isActive) return;
        quest.isActive = false;
        OnQuestDropped?.Invoke(this, new QuestAcceptedEventEventArgs { acceptedQuest = quest });
    }

}