using UnityEngine;

public class ArrowAction : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float maxLifeTIme;

    private Rigidbody2D rb;

    private float currentLifeTime;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        rb.velocity = transform.right * speed;
    }

    private void Update() {

        currentLifeTime += Time.deltaTime;

        if (currentLifeTime >= maxLifeTIme) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (Tag.PLAYER.IsSame(collision.tag)) {
            collision.GetComponent<Health>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
