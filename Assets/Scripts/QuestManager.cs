using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
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
                    print(quest.title + " is Completed!");
                    print(quest.isComplete);
                }
                return true;
            }
        }
        return false;
    }

    public void ActivateQuests(QuestSO quest)
    {
        quest.isActive = true;
    }
}