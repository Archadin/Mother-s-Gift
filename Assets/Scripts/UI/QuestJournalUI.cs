using UnityEngine;

public class QuestJournalUI : MonoBehaviour
{
    [SerializeField] private Transform questList;
    [SerializeField] private Transform questPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        QuestManager.Instace.OnQuestAccepted += QuestManager_OnQuestAccepted;
    }

    private void QuestManager_OnQuestAccepted(object sender, QuestManager.QuestAcceptedEventEventArgs e)
    {
        Transform quest = Instantiate(questPrefab, questList);
        if (quest.TryGetComponent(out QuestUI questUI))
        {
            questUI.SetQuestJournalUI(this);
            questUI.SetQuestDetails(e.acceptedQuest);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}