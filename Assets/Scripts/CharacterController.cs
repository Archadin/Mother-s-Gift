using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private const string SPEED = "Speed";
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb2D;
    private Animator animator;

    private Vector2 movement;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat(HORIZONTAL, movement.x);
        animator.SetFloat(VERTICAL, movement.y);

        animator.SetFloat(SPEED, movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}