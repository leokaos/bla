using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour {

    [SerializeField] private float damage;

    [Header("Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool triggered;
    private bool active;

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (Tag.PLAYER.IsSame(collision.tag)) {

            if (!triggered) {
                StartCoroutine(ActiveTrap());
            }

            if (active) {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator ActiveTrap() {

        triggered = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(activationDelay);

        spriteRenderer.color = Color.white;
        active = true;
        animator.SetBool("activate", true);
        yield return new WaitForSeconds(activeTime);

        active = false;
        triggered = false;
        animator.SetBool("activate", false);
    }

}
