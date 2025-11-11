using System;
using UnityEngine;
using UnityEngine.Pool;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;
    
    public event Action<Cube> OnCollision;
    public event Action<Cube> DeathTime;

    private void OnCollisionEnter(Collision collision)
    {
        OnCollision?.Invoke(this);

        float time = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime + 1f);
        Invoke(nameof(TriggerRelease), time);
    }

    private void TriggerRelease()
    {
        DeathTime?.Invoke(this);
    }
}
