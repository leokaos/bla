using UnityEngine;

public class ArrowTrap : MonoBehaviour {

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject arrowPrefab;

    private float cooldownTimer;

    private void Update() {

        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown) {
            Attack();
        }
    }

    private void Attack() {

        cooldownTimer = 0;

        Instantiate(arrowPrefab, firepoint.position,firepoint.rotation);
        
    }
}
