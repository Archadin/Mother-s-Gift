using UnityEngine;

public class QuestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestItemSO questItemSO;

    public void Interact()
    {
        //add quest item to quets tracker. turn bool on. first make the quest tracker class.
        questItemSO.isFound = QuestManager.Instace.CheckQuestProgress(questItemSO);

        if (questItemSO.isFound)
        {
            QuestManager.Instace.QuestUpdated();
            Hide();
        }
    }

    private void Start()
    {
        gameObject.layer = 6;
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        SoundManager.Instance.PlayPickup(transform.position, 1);
        gameObject.SetActive(false);
    }
}