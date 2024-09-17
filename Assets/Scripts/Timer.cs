using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _starTime;

    public void Run()
    {
        _starTime = Time.time;
    }

    public float GetTime()
    {
        return Time.time - _starTime;
    }
}
