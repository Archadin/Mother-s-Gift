using System.Collections.Generic;
using UnityEngine;

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

    //public event EventHandler OnQuestCompleteEvent;

    public bool CheckQuestComplete()
    {
        isComplete = requiredItems.TrueForAll(x => x.isFound);
        if (isComplete)
        {
            //OnQuestCompleteEvent.Invoke(this, EventArgs.Empty);
        }
        foreach (var questItem in requiredItems)
        {
            if (!questItem.isFound)
            {
                return false;
            }
        }
        return isComplete;
    }

    public void Reset()
    {
        isActive = false;
        rewardGiven = false;
        isComplete = false;
    }
}