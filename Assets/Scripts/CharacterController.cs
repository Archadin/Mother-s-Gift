using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private static CharacterController Instance;
    private const string IDLE_H = "Idle_H";
    private const string IDLE_V = "Idle_V";
    private const string SPEED = "Speed";
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb2D;
    private Animator animator;

    private Vector2 movement;
    private Vector2 lastMovement;

    [SerializeField] private LayerMask interactableLayer;

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
    }

    // Start is called before the first frame update
    private void Start()
    {
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
        if (movement.sqrMagnitude > 0)
        {
            lastMovement = movement;
        }
        if (movement == Vector2.zero)
        {
            animator.SetFloat(IDLE_H, lastMovement.x);
            animator.SetFloat(IDLE_V, lastMovement.y);
        }
    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}