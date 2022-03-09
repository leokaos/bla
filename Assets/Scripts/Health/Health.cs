using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour {

    [Header("Health")]
    [SerializeField] private float startingHealth;

    [Header("Iframes")]
    [SerializeField] private float invunerabilityTime;
    [SerializeField] private float numberOfFlashes;

    public float currentHealth { get; private set; }
    private bool dead;

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
            animator.SetTrigger(Player.ANIMATION_TRIGGER_HURT);
            StartCoroutine(Invunerability());
        } else {

            if (!dead) {
                animator.SetTrigger(Player.ANIMATION_TRIGGER_DEAD);
                GetComponent<PlayerMoviment>().enabled = false;
                dead = true;
            }
        }
    }

    public void AddHeart(float health) {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, startingHealth);
    }

    private IEnumerator Invunerability() {

        Physics2D.IgnoreLayerCollision(Layer.PLAYER.value, Layer.ENEMY.value, true);

        for (int i = 0; i < numberOfFlashes; i++) {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(invunerabilityTime / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(invunerabilityTime / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(Layer.PLAYER.value, Layer.ENEMY.value, false);
    }
}
