using UnityEngine;

public class DockTrigger : MonoBehaviour
{
    [SerializeField] bool setToFoot = true; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerMoverByTile mover = other.GetComponent<PlayerMoverByTile>();
        if (mover != null)
        {
            if (setToFoot)
                mover.SetMode(PlayerMode.OnFoot);
            else
                mover.SetMode(PlayerMode.OnBoat);
        }
    }
}
