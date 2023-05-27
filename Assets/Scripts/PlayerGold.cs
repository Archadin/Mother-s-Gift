using System;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    private int goldAmount = 0;

    public event EventHandler OnGoldAdded;

    public void AddGold(QuestSO quest)
    {
        if (quest.rewardGiven) return;
        goldAmount += quest.reward;
        quest.rewardGiven = true;
        OnGoldAdded?.Invoke(this, EventArgs.Empty);
    }

    public int GetGoldAmount()
    {
        return goldAmount;
    }
}