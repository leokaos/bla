using UnityEngine;

public class EnemyAction : MonoBehaviour {

    [Header("Attack")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private float range;

    [Header("Detection")]
    [SerializeField] private BoxCollider2D colliderAttack;
    [SerializeField] private float visionSize;

    [Header("Layer")]
    [SerializeField] private LayerMask playerLayer;

    private Animator animator;
    private Health healt;

    private PatrolAction patrolAction;

    private void Awake() {
        animator = GetComponent<Animator>();
        patrolAction = GetComponentInParent<PatrolAction>();
    }

    private float cooldownTimer = Mathf.Infinity;

    private void Update() {

        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown) {

            if (PlayerInSight()) {
                cooldownTimer = 0;
                animator.SetTrigger(Enemy.ANIMATION_TRIGGER_ATTACK);
            }
        }

        if (patrolAction != null) {
            patrolAction.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight() {

        RaycastHit2D hit = Physics2D.BoxCast(
            colliderAttack.bounds.center + transform.right * range * transform.localScale.x * visionSize,
            new Vector3(colliderAttack.bounds.size.x * range, colliderAttack.bounds.size.y, colliderAttack.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null) {
            healt = hit.collider.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void DamagePlayer() {

        if (PlayerInSight()) {
            healt.TakeDamage(damage);
        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            colliderAttack.bounds.center + transform.right * range * transform.localScale.x * visionSize,
            new Vector3(colliderAttack.bounds.size.x * range, colliderAttack.bounds.size.y, colliderAttack.bounds.size.z));
    }
}
