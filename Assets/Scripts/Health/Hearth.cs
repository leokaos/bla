using UnityEngine;

public class Hearth : MonoBehaviour {

    [SerializeField] private float healthIncrement;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (Tag.PLAYER.IsSame(collision.tag)) {

            collision.GetComponent<Health>().AddHeart(healthIncrement);

            gameObject.SetActive(false);
        }
    }
}
