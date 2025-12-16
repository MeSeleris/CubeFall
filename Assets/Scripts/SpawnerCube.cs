using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerCube : MonoBehaviour
{
    [SerializeField] private Cube _prefabToSpawn;
    [SerializeField] private SpawnPosition _spawnPos;
    [SerializeField] private Recolor _collisionRecolor;

    [SerializeField] private int _poolCapacity = 6;
    [SerializeField] private int _poolMaxSize = 15;

    private WaitForSeconds _delaySpawn = new WaitForSeconds(1f);

    private ObjectPool<Cube> pool;

    private void Awake()
    {
        pool = new ObjectPool<Cube>(
            createFunc : () => Instantiate(_prefabToSpawn),
            actionOnGet: PrepareGetCube,
            actionOnRelease: PrepareReleaseCube,
            actionOnDestroy: DestroyCube,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private void PrepareGetCube(Cube cube)
    {
        cube.RequestRelease += OnCubeRequestRelease;      
        cube.transform.position = _spawnPos.SpawnDetect();      
        cube.gameObject.SetActive(true);
    }

    private void PrepareReleaseCube(Cube cube)
    {
        cube.ResetState();
        cube.gameObject.SetActive(false);
    }

    private void DestroyCube(Cube cube)
    {       
        Destroy(cube.gameObject);
    }
    private void OnCubeRequestRelease(Cube cube)
    {
        pool.Release(cube);
        cube.RequestRelease -= OnCubeRequestRelease;
    }

    private IEnumerator SpawnCoroutine()
    {
        while (enabled)
        {
            pool.Get();
            yield return _delaySpawn;
        }
    }
}
