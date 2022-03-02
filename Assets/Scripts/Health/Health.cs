using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour {

    [Header("Health")]
    [SerializeField] private float startingHealth;

    [Header("IFrame")]
    [SerializeField] private float iframesDuration;
    [SerializeField] private float numberOfFlashes;

    private bool dead;
    public float currentHealth { get; private set; }

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake() {

        currentHealth = startingHealth;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage) {

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0) {
            animator.SetTrigger("hurt");
            StartCoroutine(invunerability());
        } else {

            if (!dead) {
                animator.SetTrigger("die");
                GetComponent<PlayerMoviment>().enabled = false;
                dead = true;
            }
        }
    }

    public void AddHealth(float health) {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, startingHealth);
    }

    private IEnumerator invunerability() {

        Physics2D.IgnoreLayerCollision(Layers.PLAYER, Layers.ENEMY, true);

        for (int i = 0; i < numberOfFlashes; i++) {

            spriteRenderer.color = new Color(1, 0, 1, 0.5f);
            yield return new WaitForSeconds(iframesDuration / (numberOfFlashes * 2));

            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iframesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(Layers.PLAYER, Layers.ENEMY, false);
    }

}
