using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballAudio;

    private Animator animator;
    private PlayerMoviment playerMoviment;

    private float cooldownTimer = Mathf.Infinity;

    private void Awake() {
        animator = GetComponent<Animator>();
        playerMoviment = GetComponent<PlayerMoviment>();
    }

    private void Update() {

        if (Input.GetMouseButton(0) && cooldownTimer > attackCoolDown && playerMoviment.canAttack()) {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }
    private void Attack() {

        SoundManager.instance.playSound(fireballAudio);

        animator.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[FindFireballs()].transform.position = firePoint.position;
        fireballs[FindFireballs()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireballs() {

        for (int i = 0; i < fireballs.Length; i++) {

            if (!fireballs[i].activeInHierarchy) {
                return i;
            }
        }

        return 0;
    }
}
