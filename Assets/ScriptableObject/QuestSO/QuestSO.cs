using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "QuestSO", menuName = "ScriptableObjects/Quest")]
public class QuestSO : ScriptableObject
{
    public string title;
    public string description;
    public bool isActive;
    public List<QuestItemSO> requiredItems;
    public bool isComplete;
    public int reward;
    public bool rewardGiven;
    public UnityEvent OnQuestComplete;

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
            OnQuestComplete?.Invoke();
        }
        return isComplete;
    }
}
