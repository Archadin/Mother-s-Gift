using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Menu;
    [SerializeField] private Button QuitButton;
    [SerializeField] private Button MusicButton;
    [SerializeField] private Button VolumeButton;
    [SerializeField] private TMPro.TextMeshProUGUI TheEnd;
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
            FadeInCanvasGroup.alpha -= Time.deltaTime * 3;
            yield return new WaitForSeconds(.001f);
        }
        PlayerMovement.Instance.EnableMovement();
        SoundManager.Instance.ToggleMusic(false);
    }

    public void GameEnd()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        PlayerMovement.Instance.DisableMovement();
        SoundManager.Instance.ToggleMusic(true);
        while (FadeInCanvasGroup.alpha < 1)
        {
            FadeInCanvasGroup.alpha += Time.deltaTime * 3;
            yield return new WaitForSeconds(.001f);
        }
        TheEnd.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        // load main menu.
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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