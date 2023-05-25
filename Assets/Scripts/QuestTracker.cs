using System.Collections.Generic;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
    [System.Serializable]
    private struct Quest
    {
        public string title;
        public List<QuestItemSO> requiredItems;
        public bool isComplete;

        public bool CheckQuestComplete()
        {
            isComplete = requiredItems.TrueForAll(x => x.isFound);
            foreach (var questItem in requiredItems)
            {
                if (!questItem.isFound)
                {
                    return false;
                }
                isComplete = true;
            }
            return isComplete;
        }
    }

    public static QuestTracker Instace;
    private bool isActive;
    [SerializeField] private List<Quest> acceptedQuests = new List<Quest>();

    private void Awake()
    {
        Instace = this;
    }

    public void CheckQuestProgress(QuestItemSO questItem)
    {
        if (!isActive) return;
        foreach (var quest in acceptedQuests)
        {
            if (quest.requiredItems.Contains(questItem))
            {
                if (quest.CheckQuestComplete())
                {
                    print(quest.title + " is Completed!");
                    print(quest.isComplete);
                }
            }
        }
    }

    public void SetTrackerActive(bool enabled)
    {
        isActive = enabled;
    }
}