using UnityEngine;

public class PlayerGoldUI : MonoBehaviour
{
    [SerializeField] private PlayerGold playerGold;
    [SerializeField] private TMPro.TextMeshProUGUI goldText;

    // Start is called before the first frame update
    private void Start()
    {
        playerGold.OnGoldAdded += PlayerGold_OnGoldAdded;
        goldText.text = "0";
    }

    private void PlayerGold_OnGoldAdded(object sender, System.EventArgs e)
    {
        goldText.text = playerGold.GetGoldAmount().ToString();
        SoundManager.Instance.PlayGold(transform.position, 1);
    }
}