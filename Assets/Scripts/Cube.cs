using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private CollisionRecolor _recolor;
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;
    private bool isDie = false;

    public event Action<Cube> DeathTime;

    private void OnCollisionEnter(Collision collision)
    {
        if (isDie) return;

        _recolor.ChangeColorOnce(this);

        float time = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime + 1f);
        StartCoroutine(Timer(time));

        isDie = true;
    }

    private void TriggerRelease()
    {
        DeathTime?.Invoke(this);
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        TriggerRelease();
    }
}
