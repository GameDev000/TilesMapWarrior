using UnityEngine;

public class BoatEnterTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerMoverByTile mover = other.GetComponent<PlayerMoverByTile>();
        if (mover != null)
        {
            mover.SetMode(PlayerMode.OnBoat);
        }
    }
}
