using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    [SerializeField] private TriggerEvent thisisit;
    [SerializeField] private CharacterController player;
    private string previousScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void SceneManager_sceneUnloaded(Scene pScene)
    {
        previousScene = pScene.name;
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (previousScene == null) return;
        if (player == null) return;

        if (scene.name == Scenes.GameScene_Town.ToString())
        {
            TriggerEvent[] triggerEvents = FindObjectsOfType<TriggerEvent>();
            if (triggerEvents == null) return;
            foreach (var enter in triggerEvents)
            {
                if (enter.GetScene().ToString() == previousScene)
                {
                    player.transform.position = enter.GetEnterPosition();
                }
            }
        }
        else
        {
            TriggerEvent triggerEvent = FindObjectOfType<TriggerEvent>();
            if (triggerEvent != null)
            {
                player.transform.position = triggerEvent.GetEnterPosition();
            }
        }
    }

    public static void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}

public enum Scenes
{
    Menu,
    GameScene_House,
    GameScene_Town,
    GameScene_Guild,
    GameScene_Bazaar,
    GameScene_Forest
}