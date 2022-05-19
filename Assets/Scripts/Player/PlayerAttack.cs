using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireball;
    [SerializeField] private AudioClip sound;

    private Animator animator;
    private PlayerMoviment playerMoviment;

    private float cooldownTimer = Mathf.Infinity;

    private void Awake() {
        animator = GetComponent<Animator>();
        playerMoviment = GetComponent<PlayerMoviment>();
    }

    private void Update() {

        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMoviment.CanAttack()) {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack() {

        SoundManager.instance.PlaySound(sound);

        animator.SetTrigger(Player.ANIMATION_TRIGGER_ATTACK);
        cooldownTimer = 0;

        GameObject projectible = Instantiate(fireball, firePoint.position, firePoint.rotation);
        projectible.GetComponent<Projectible>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

}
