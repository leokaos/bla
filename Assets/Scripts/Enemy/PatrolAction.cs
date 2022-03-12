using UnityEngine;

public class PatrolAction : MonoBehaviour {

    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Moviment")]
    [SerializeField] private float speed;
    [SerializeField] private float idleDuration;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    private bool movingLeft;
    private Vector3 initialScale;
    private float idleTimer;

    private void Awake() {
        initialScale = enemy.localScale;
        movingLeft = initialScale.x < 0;
    }

    private void OnDisable() {
        animator.SetBool(Enemy.ANIMATION_BOOL_MOVING, false);
    }

    private void Update() {

        if (movingLeft) {


            if (enemy.transform.position.x <= leftEdge.position.x) {
                TriggerChangeDirection();
            } else {
                MoveInDirection(-1);
            }

        } else {


            if (enemy.transform.position.x >= rightEdge.position.x) {
                TriggerChangeDirection();
            } else {
                MoveInDirection(1);
            }
        }
    }

    private void TriggerChangeDirection() {

        idleTimer += Time.deltaTime;
        animator.SetBool(Enemy.ANIMATION_BOOL_MOVING, false);

        if (idleTimer > idleDuration) {
            movingLeft = !movingLeft;
        }
    }

    private void MoveInDirection(int direction) {

        animator.SetBool(Enemy.ANIMATION_BOOL_MOVING, true);
        idleTimer = 0;

        enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * direction, initialScale.y, initialScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);

    }

}
