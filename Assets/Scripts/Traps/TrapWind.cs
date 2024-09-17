using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWind : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _distanceZone;
    [SerializeField] private float _timeChangeDirectionWind;

    private Transform _transform;
    private Vector3 _directionWind;

    private void Awake()
    {
        _transform = transform;

        StartCoroutine(Wind());
    }

    private void FixedUpdate()
    {
        if (TryGetPlayersInZone(out List<Player> players))
        {
            foreach (Player player in players)
            {
                player.GetComponent<Rigidbody>().AddForce(_directionWind * _force, ForceMode.Force);
            }
        }
    }

    private bool TryGetPlayersInZone(out List<Player> players)
    {
        RaycastHit[] hits = Physics.BoxCastAll(_transform.position, _transform.localScale, Vector3.up, new Quaternion(), _distanceZone);

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

    private IEnumerator Wind()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeChangeDirectionWind);

        bool isWork = true;

        while (isWork)
        {
            ChangeDirectionWind();

            yield return wait;
        }
    }

    private void ChangeDirectionWind()
    {
        float x = 0;
        float y = 0;

        while (x == 0 && y == 0)
        {
            x = Random.Range(-1f, 1f);
            y = Random.Range(-1f, 1f);
        }

        _directionWind = Vector3.right * Random.Range(-1f, 1f) + Vector3.forward * Random.Range(-1f, 1f);
    }
}
