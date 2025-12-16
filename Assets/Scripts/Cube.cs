using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action<Cube> RequestRelease;

    [SerializeField] private Recolor _recolor;
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private bool _isFirstCollision;
    private Coroutine _lifeRoutine;

    public void ResetState()
    {
        _isFirstCollision = false;

        if (_lifeRoutine != null)
        {
            StopCoroutine(_lifeRoutine);
            _lifeRoutine = null;
        }

        _recolor.ResetToDefault(this);

        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isFirstCollision)
            return;

        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform) == false)
            return;

        _isFirstCollision = true;
        _recolor.SetRandomColor(this);
        float time = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
        StartCoroutine(StartLifeTimer(time));
    }

    private IEnumerator StartLifeTimer(float time)
    {
        yield return new WaitForSeconds(time);
        RequestRelease?.Invoke(this);
    }
}
