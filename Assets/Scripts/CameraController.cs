using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private float cameraSpeed;
    [SerializeField] private float distanceAhead;
    [SerializeField] private Transform player;
    [SerializeField] private float limitLeft;

    private float lookAhead;

    private void Update() {

        float x = player.position.x + lookAhead;

        if (x < limitLeft) {
            x = limitLeft;
        }

        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, distanceAhead * player.localScale.x, Time.deltaTime * cameraSpeed);
    }

}
