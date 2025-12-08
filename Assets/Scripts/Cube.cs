using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private CollisionRecolor _recolor;
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    public string targetTag = "Walls";
    private bool isTimerStart = false;
    private bool isEndTime = false;
    private bool haveFirstRecolor = false;

    public event Action<Cube> CallingDeath;
     
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            if (isTimerStart)
                return;

            if (isEndTime)
            {
                haveFirstRecolor = false;
                _recolor.ResetToDefault(this);
            }
           
            if (!haveFirstRecolor)
            {
                _recolor.SetRandomColor(this);
                haveFirstRecolor = true;
            }

            _recolor.SetRandomColor(this);

            float time = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime + 1f);
            StartCoroutine(StartLifeTimer(time));

            isTimerStart = true;
        }      
    }

    private void TriggerRelease()
    {
        CallingDeath?.Invoke(this);
    }

    private IEnumerator StartLifeTimer(float time)
    {
        yield return new WaitForSeconds(time);
        isEndTime = true;
        TriggerRelease();
    }

    private void OnDisable()
    {
        haveFirstRecolor = false;
        isEndTime = false;
        isTimerStart = false;
    }
}
