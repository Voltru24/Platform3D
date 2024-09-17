using UnityEngine;

public class TriggerFall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.Kill();
        }
    }
}
