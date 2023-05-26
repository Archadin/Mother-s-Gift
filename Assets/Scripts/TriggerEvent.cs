using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private string triggerPrefs;

    [SerializeField] private Transform enterPosition;
    [SerializeField] private bool canTravel = true;
    [SerializeField] private string denyMessage;
    [SerializeField] private Scenes scene;

    private void Start()
    {
        SceneLoader.Instance.OnScenLoadedEvent += SceneLoader_OnScenLoadedEvent;
    }

    private void SceneLoader_OnScenLoadedEvent(object sender, System.EventArgs e)
    {
        LoadTriggerPrefs();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterController>())
        {
            SaveTriggerPrefs();
            //change scene depending on the current scene.
            if (canTravel)
            {
                SceneLoader.LoadScene(scene);
            }
            else
            {
                Debug.Log(denyMessage);
            }
        }
    }

    public Scenes GetScene()
    {
        return scene;
    }

    public Vector3 GetEnterPosition()
    {
        return enterPosition.position;
    }

    public void SetCanTravel(bool enabled)
    {
        canTravel = enabled;
    }

    public void SaveTriggerPrefs()
    {
        PlayerPrefs.SetInt(triggerPrefs, canTravel ? 1 : 0);
    }

    public void LoadTriggerPrefs()
    {
        canTravel = PlayerPrefs.GetInt(triggerPrefs) == 1 ? true : false;
    }
}