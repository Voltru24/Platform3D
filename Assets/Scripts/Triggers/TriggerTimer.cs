using UnityEngine;

public class TriggerTimer : MonoBehaviour
{
    [SerializeField] private Timer timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            timer.Run();
        }
    }
}
