using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 pos = target.position;
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
