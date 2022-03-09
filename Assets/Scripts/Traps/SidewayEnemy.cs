using UnityEngine;

public class SidewayEnemy : MonoBehaviour {

    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float movimentDistance;

    private bool movingLeft;
    private float maxLeft;
    private float maxRight;

    private void Awake() {
        maxLeft = transform.position.x - movimentDistance;
        maxRight = transform.position.x + movimentDistance;
    }

    private void Update() {

        if (movingLeft) {

            if (transform.position.x > maxLeft) {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            } else {
                movingLeft = false;
            }

        } else {
            
            if (transform.position.x < maxRight) {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            } else {
                movingLeft = true;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (Tag.PLAYER.IsSame(collision.tag)) {
            collision.GetComponent<Health>().TakeDamage(damage);
        }

    }

}
