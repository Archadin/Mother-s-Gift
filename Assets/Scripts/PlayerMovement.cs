using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float maxVelocity = 2f;
    [SerializeField] private float jumpForce = 20f;

    private Vector2 move;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    private void Update()
    {
        move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");
        print(move);

        spriteRenderer.flipX = move.x < 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
        rigidbody2D.velocity = move;
    }
}