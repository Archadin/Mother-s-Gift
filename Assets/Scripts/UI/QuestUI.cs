using System.Linq;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    private QuestJournalUI questJournal;
    private QuestSO Quest;
    [SerializeField] private TMPro.TextMeshProUGUI QuestTitle;
    [SerializeField] private TMPro.TextMeshProUGUI QuestDescription;
    [SerializeField] private TMPro.TextMeshProUGUI QuestObjective;

    // Start is called before the first frame update
    private void Start()
    {
        QuestManager.Instace.OnQuestUpdated += QuestManager_OnQuestUpdated;
        QuestManager.Instace.OnQuestDropped += QuestManager_OnQuestDropped;
    }

    private void QuestManager_OnQuestDropped(object sender, QuestManager.QuestAcceptedEventEventArgs e)
    {
        if (e.acceptedQuest == Quest)
        {
            gameObject.SetActive(false);
        }
    }

    private void QuestManager_OnQuestUpdated(object sender, System.EventArgs e)
    {
        SetQuestDetails(Quest);
    }

    public void SetQuestJournalUI(QuestJournalUI _questJournal)
    {
        questJournal = _questJournal;
    }

    public void SetQuestDetails(QuestSO questSO)
    {
        Quest = questSO;
        QuestTitle.text = questSO.title;
        QuestDescription.text = questSO.description;
        int collectedItems = questSO.requiredItems.Where(i => i.isFound).Count();
        int questItems = questSO.requiredItems.Count;
        QuestObjective.text = $"{collectedItems}/{questItems}";
    }
}