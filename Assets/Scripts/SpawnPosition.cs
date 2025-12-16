using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float _numRandomMax;

    private void Reset()
    {
        if (target == null)
            target = transform;
    }

    public Vector3 SpawnDetect()
    {
        if (target == null)
            return Vector3.zero;

        Vector3 spawnPosition = target.position;

        float shiftNumber = UnityEngine.Random.Range(0, _numRandomMax);

        bool isAdditionX = UnityEngine.Random.Range(0, 2) == 0;
        bool isAdditionZ = UnityEngine.Random.Range(0, 2) == 0;

        spawnPosition.x += isAdditionX ? shiftNumber : -shiftNumber; 
        spawnPosition.z += isAdditionZ ? shiftNumber : -shiftNumber;
        

        return spawnPosition;
    }
}
