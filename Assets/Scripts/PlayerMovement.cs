using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    private const string IDLE_H = "Idle_H";
    private const string IDLE_V = "Idle_V";
    private const string SPEED = "Speed";
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maskSpeed = 2f;
    private Rigidbody2D rb2D;
    private Animator animator;

    private Vector2 movement;
    private Vector2 lastMovement;
    private SpriteMask spriteMask;
    [SerializeField] private LayerMask interactableLayer;
    private bool canMove = true;
    private Vector2 direction;

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
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteMask = GetComponentInChildren<SpriteMask>();
        EnableMovement();
    }

    // Update is called once per frame
    private void Update()
    {
        if (lastMovement != Vector2.zero)
        {
            direction = lastMovement.normalized;
        }
        RaycastHit2D hit2D = Physics2D.CircleCast(transform.position, .8f, direction, .8f, interactableLayer);
        if (Input.GetKeyDown(KeyCode.E))
        {
            //RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down * 2,interactableLayer);
            if (hit2D && hit2D.transform.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }

        if (!canMove) return;

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

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        lastMovement = Vector2.zero;
        movement = Vector2.zero;
        rb2D.velocity = Vector2.zero;
        animator.SetFloat(SPEED, 0);
        canMove = false;
    }

    public void EnableSpriteMask()
    {
        spriteMask.gameObject.SetActive(true);
        StartCoroutine(IncreaseSize());
    }

    private IEnumerator IncreaseSize()
    {
        while (spriteMask.gameObject.transform.localScale.x < 35)
        {
            DisableMovement();
            spriteMask.gameObject.transform.localScale += Vector3.one * maskSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        EnableMovement();
    }

    public void DisableSpriteMask()
    {
        spriteMask.gameObject.SetActive(false);
    }
}