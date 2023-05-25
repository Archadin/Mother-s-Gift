using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private bool canTravel = true;
    [SerializeField] private string denyMessage;
    [SerializeField] private int sceneToTravelIndex = -1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterController>())
        {
            //change scene depending on the current scene.
            if (canTravel)
            {

            }
            else
            {
            }
        }
    }
}