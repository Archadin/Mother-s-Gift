using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private Transform enterPosition;
    [SerializeField] private bool canTravel = true;
    [SerializeField] private string denyMessage;
    [SerializeField] private Scenes scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterController>())
        {
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
}