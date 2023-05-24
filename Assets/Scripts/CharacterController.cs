using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private const string SPEED = "Speed";
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 movement;

    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private LayerMask interactableLayer;

    // Start is called before the first frame update
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D hit2D = Physics2D.CircleCast(transform.position, 1, Vector2.down, 1, layerMask: interactableLayer);
        if (Input.GetKeyDown(KeyCode.E))
        {
            //RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down * 2,interactableLayer);
            if (hit2D && hit2D.transform.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat(HORIZONTAL, movement.x);
        animator.SetFloat(VERTICAL, movement.y);
        SetSprite();

        animator.SetFloat(SPEED, movement.sqrMagnitude);
    }

    private void SetSprite()
    {
        if (movement.sqrMagnitude > .01f) return;
        if (movement.x > 0)
        {
            spriteRenderer.sprite = rightSprite;
        }
        else if (movement.x < 0)
        {
            spriteRenderer.sprite = leftSprite;
        }

        if (movement.y > 0)
        {
            spriteRenderer.sprite = upSprite;
        }
        else if (movement.y < 0)
        {
            spriteRenderer.sprite = downSprite;
        }
    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}