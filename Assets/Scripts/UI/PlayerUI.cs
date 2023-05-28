using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private PlayerMonologue monologue;
    [SerializeField] private TMPro.TextMeshProUGUI text;

    private void Start()
    {
        monologue.OnMonologue += Monologue_OnMonologue;
        monologue.OnMonologueEnded += Monologue_OnMonologueEnded;
        Hide();
    }

    private void Monologue_OnMonologue(object sender, System.EventArgs e)
    {
        if (!string.IsNullOrEmpty(monologue.GetCurrentLine()))
        {
            Show();
            text.SetText(monologue.GetCurrentLine());
        }
    }

    private void Monologue_OnMonologueEnded(object sender, System.EventArgs e)
    {
        Hide();
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