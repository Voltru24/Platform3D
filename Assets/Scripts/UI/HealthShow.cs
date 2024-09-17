using UnityEngine;
using UnityEngine.UI;

public class HealthShow : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _slider;

    private void Start()
    {
        Show();
    }

    private void OnEnable()
    {
        _player.ChangeHealth += Show;
    }

    private void OnDisable()
    {
        _player.ChangeHealth -= Show;
    }

    private void Show()
    {
        _slider.maxValue = _player.MaxHealth;
        _slider.value = _player.Health;
    }
}
