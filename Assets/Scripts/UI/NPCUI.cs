using UnityEngine;

public class NPCUI : MonoBehaviour
{
    [SerializeField] private NPCInteractable NPC;
    [SerializeField] private Sprite NPC_UI_Sprite;
    [SerializeField] private UnityEngine.UI.Image NPCSprite;
    [SerializeField] private TMPro.TextMeshProUGUI text;

    private void Start()
    {
        NPC.OnInteractEvent += NPC_OnInteractEvent;
        NPC.OnLinesFinished += NPC_OnLinesFinished;
        if (NPCSprite != null)
        {
            NPCSprite.sprite = NPC_UI_Sprite;
        }
        Hide();
    }

    private void NPC_OnLinesFinished(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void NPC_OnInteractEvent(object sender, System.EventArgs e)
    {
        if (!string.IsNullOrEmpty(NPC.GetCurrentLine()))
        {
            Show();
            text.SetText(NPC.GetCurrentLine());
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}