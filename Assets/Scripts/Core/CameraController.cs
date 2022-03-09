using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private Transform player;
    [SerializeField] private GameObject boundObject;
    [SerializeField] private float aheaDistance;
    [SerializeField] private float cameraSpeed;


    private float lookAhead;

    private void Update() {

        float x = Mathf.Clamp(player.position.x + lookAhead, 0, Mathf.Infinity);

        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheaDistance * player.localScale.x), Time.deltaTime * cameraSpeed);

    }


}
