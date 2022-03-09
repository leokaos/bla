using UnityEngine;

public class PlayerMoviment : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private float walljumpCooldown;
    private float horizontalInput;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() {

        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f) {
            transform.localScale = Vector3.one;
        } else if (horizontalInput < -0.01f) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        animator.SetBool(Player.ANIMATION_BOOL_RUN, horizontalInput != 0);
        animator.SetBool(Player.ANIMATION_BOOL_GROUNDED, IsGrounded());


        if (walljumpCooldown > 0.2f) {

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (OnWall() && !IsGrounded()) {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            } else {
                body.gravityScale = 3;
            }

            if (Input.GetKey(KeyCode.Space)) {
                Jump();
            }

        } else {
            walljumpCooldown += Time.deltaTime;
        }
    }

    private void Jump() {

        if (IsGrounded()) {
            animator.SetTrigger(Player.ANIMATION_TRIGGER_JUMP);
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        } else if (OnWall()) {

            if (horizontalInput == 0) {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            } else {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }

            walljumpCooldown = 0;
        }

    }

    public bool CanAttack() {
        return horizontalInput == 0 && IsGrounded() && !OnWall();
    }

    public bool IsGrounded() {
        return CreateRaycast(groundLayer, Vector2.down).collider != null;
    }

    public bool OnWall() {
        return CreateRaycast(wallLayer, new Vector2(transform.localScale.x, 0)).collider != null;
    }

    private RaycastHit2D CreateRaycast(LayerMask layer, Vector2 direction) {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, direction, 0.1f, layer);
    }
}
