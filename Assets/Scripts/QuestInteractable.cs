using UnityEngine;

public class QuestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestItemSO questItemSO;

    public void Interact()
    {
        //add quest item to quets tracker. turn bool on. first make the quest tracker class.
        questItemSO.isFound = true;
        QuestTracker.Instace.CheckQuestProgress(questItemSO);
    }

    private void Start()
    {
        gameObject.layer = 6;
    }
}