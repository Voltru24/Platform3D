using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFire : MonoBehaviour
{
    [SerializeField] private float _timeActivate;
    [SerializeField] private float _timeFire;
    [SerializeField] private float _timeRestart;
    [SerializeField] private int _forceDamage;

    [SerializeField] private Material _materialDefualt;
    [SerializeField] private Material _materialActivate;
    [SerializeField] private Material _materialFire;

    private MeshRenderer _renderer;
    private Transform _transform;

    private bool _isTimerActivate = false;
    private float _distanceFire = 0.1f;

    private void Awake()
    {
        _transform = transform;

        _renderer = GetComponent<MeshRenderer>();

        _renderer.material = _materialDefualt;
    }

    private void FixedUpdate()
    {
        if (_isTimerActivate == false)
        {
            if (TryGetPlayersOnGround(out _))
            {
                StartCoroutine(Activate());
                _isTimerActivate = true;
            }
        }
    }

    private IEnumerator Activate()
    {
        _renderer.material = _materialActivate;

        yield return new WaitForSeconds(_timeActivate);

        _renderer.material = _materialFire;

        ApplyDamage();

        yield return new WaitForSeconds(_timeFire);

        _renderer.material = _materialDefualt;

        Invoke(nameof(TimerActivate), _timeRestart);
    }

    private void ApplyDamage()
    {
        if (TryGetPlayersOnGround(out List<Player> players))
        {
            foreach (Player player in players)
            {
                player.TakeDamage(_forceDamage);
            }
        }
    }

    private void TimerActivate()
    {
        _isTimerActivate = false;
    }

    private bool TryGetPlayersOnGround(out List<Player> players)
    {
        RaycastHit[] hits = Physics.BoxCastAll(_transform.position, _transform.localScale, Vector3.up, new Quaternion(), _distanceFire);

        players = new List<Player>();

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.TryGetComponent(out Player player))
            {
                players.Add(player);
            }
        }

        if (players.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
