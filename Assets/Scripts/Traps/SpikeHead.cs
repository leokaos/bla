using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : EnemyDamage {

    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;

    private Vector3 destination;

    private bool attacking;
    private float checkTimer;
    private Vector3[] directions = new Vector3[4];

    void Update() {

        if (attacking) {
            transform.Translate(destination * speed * Time.deltaTime);
        } else {

            checkTimer += Time.deltaTime;

            if (checkTimer > checkDelay) {
                CheckForPlayer();
            }
        }
    }

    private void OnEnable() {
        Stop();
    }

    private void CheckForPlayer() {
        CalculateDirections();

        for (int i = 0; i < directions.Length; i++) {

            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking) {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void Stop() {
        destination = transform.position;
        attacking = false;
    }

    private void CalculateDirections() {

        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
