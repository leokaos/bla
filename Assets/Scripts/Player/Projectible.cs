using UnityEngine;

public class Projectible : MonoBehaviour {

    [SerializeField] private float speed;

    private Animator animator;
    private BoxCollider2D boxCollider;

    private bool hit;
    private float direction;
    private float lifeTime;

    private void Awake() {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() {

        if (hit) {
            return;
        }

        float movimentSpeed = speed * direction * Time.deltaTime;
        transform.Translate(movimentSpeed, 0, 0);

        lifeTime += Time.deltaTime;

        if (lifeTime > 5) {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        hit = true;
        boxCollider.enabled = false;
        animator.SetTrigger(Fireball.ANIMATION_TRIGGER_EXPLODE);

        if (Tag.ENEMY.IsSame(collision)) {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    public void SetDirection(float direction) {

        this.direction = direction;
        this.hit = false;
        this.lifeTime = 0;

        gameObject.SetActive(true);
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;

        if (Mathf.Sign(localScaleX) != this.direction) {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }
}
