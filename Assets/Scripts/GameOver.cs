using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _content;

    private void Awake()
    {
        Close();
    }

    private void OnEnable()
    {
        _player.Died += Show;
    }

    private void OnDisable()
    {
        _player.Died -= Show;
    }

    public void OnRestart()
    {
        _player.Restart();

        Close();
    }

    private void Show()
    {
        _content.SetActive(true);
    }

    private void Close()
    {
        _content.SetActive(false);
    }
}
