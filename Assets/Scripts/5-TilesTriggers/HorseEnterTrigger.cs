using UnityEngine;

public class HorseEnterTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerMoverByTile mover = other.GetComponent<PlayerMoverByTile>();
        if (mover != null)
        {
            Debug.Log("Switching to HORSE mode!");
            mover.SetMode(PlayerMode.OnHorse);
        }
    }
}
