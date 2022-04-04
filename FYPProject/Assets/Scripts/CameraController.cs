using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;
    // private float orthoSize;

    // public float smoothTime;
    // public Transform player;


    [HideInInspector]
    public int worldSize;

    // public void spawn(Vector3 pos)
    // {
    //     GetComponent<Transform>().position = pos;
    //     orthoSize = GetComponent<Camera>().orthographicSize;
    // }

    private void Update()
    {
        // Vector3 cameraView = GetComponent<Transform>().position;
        // cameraView.x = Mathf.Lerp(cameraView.x, player.position.x, smoothTime);
        // cameraView.y = Mathf.Lerp(cameraView.y, player.position.y, smoothTime);
        // //prevent camera from looking out of map
        // cameraView.x = Mathf.Clamp(cameraView.x, 0 + (orthoSize * 2.2f), worldSize - (orthoSize * 2.2f));
        // GetComponent<Transform>().position = cameraView;

        //room camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);
        if (player.transform.hasChanged)
        {
            //transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
            //lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);

        }
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        //currentPosX = _newRoom.position.x;
    }
}
