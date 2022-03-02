using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (Tags.isTag(collision.tag, Tags.PLAYER)) {
            collision.GetComponent<Health>().TakeDamage(damage);
        }

    }
}
