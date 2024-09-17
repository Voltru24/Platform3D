using System;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    [SerializeField] private int _maxHealth;
    [SerializeField] private Transform _pointSpawn;

    public event Action ChangeHealth;
    public event Action Died;
    public event Action<bool> ChangeStun;

    private int _health;
    private Transform _transform;

    public bool IsAlive => _health > 0;
    public int Health => _health;
    public int MaxHealth => _maxHealth;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        Restart();
    }

    public void TakeDamage(int value)
    {
        _health -= value;

        Debug.Log("hp: " + _health) ;

        if (IsAlive == false)
        {
            Kill();
        }
        else
        {
            ChangeHealth?.Invoke();
        }
    }

    public void Kill()
    {
        _health = 0;

        ChangeHealth?.Invoke();

        Died?.Invoke();
    }

    public void Stun()
    {
        ChangeStun?.Invoke(true);
    }

    public void Restart()
    {
        _health = _maxHealth;
        _transform.position = _pointSpawn.position;
        ChangeStun?.Invoke(false);
        ChangeHealth?.Invoke();
    }
}
