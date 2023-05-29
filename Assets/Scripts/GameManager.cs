using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Menu;
    [SerializeField] private Button QuitButton;
    [SerializeField] private Button MusicButton;
    [SerializeField] private Button VolumeButton;
    [SerializeField] private CanvasGroup FadeInCanvasGroup;

    private void Start()
    {
        MusicButton.onClick.AddListener(SoundManager.Instance.ToggleMusic);
        VolumeButton.onClick.AddListener(SoundManager.Instance.SetMusicVolume);
        QuitButton.onClick.AddListener(Quit);
        Hide();
        SoundManager.Instance.ToggleMusic(true);
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while (FadeInCanvasGroup.alpha > 0)
        {
            PlayerMovement.Instance.DisableMovement();
            FadeInCanvasGroup.alpha -= Time.deltaTime;
            yield return new WaitForSeconds(.01f);
        }
        PlayerMovement.Instance.EnableMovement();
        SoundManager.Instance.ToggleMusic(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Menu.activeInHierarchy)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void Show()
    {
        Menu.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Hide()
    {
        Menu.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}