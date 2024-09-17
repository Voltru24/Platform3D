using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameWin gameWin;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            gameWin.Show();
        }
    }
}
