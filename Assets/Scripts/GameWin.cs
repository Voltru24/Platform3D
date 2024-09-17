using TMPro;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _content;
    [SerializeField] private Timer _timer;
    [SerializeField] private TMP_Text _textTimer;

    private void Awake()
    {
        Close();
    }

    public void Show()
    {
        _textTimer.text = $"Вы прошли уровень за: {_textTimer.text = _timer.GetTime().ToString()} cек.";

        _player.Stun();

        _content.SetActive(true);
    }

    public void OnRestart()
    {
        _player.Restart();

        Close();
    }

    private void Close()
    {
        _content.SetActive(false);
    }

}
