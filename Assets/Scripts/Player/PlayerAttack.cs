using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
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

        fireballs[FindFireBall()].transform.position = firePoint.position;
        fireballs[FindFireBall()].GetComponent<Projectible>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireBall() {

        for (int i = 0; i < fireballs.Length; i++) {
            if (!fireballs[i].activeInHierarchy) {
                return i;
            }
        }

        return 0;
    }

}
