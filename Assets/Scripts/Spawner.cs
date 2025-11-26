using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefabToSpawn;
    [SerializeField] private SpawnCubePosition _spawnPos;
    [SerializeField] private CollisionRecolor _collisionRecolor;

    [SerializeField] private int _poolCapacity = 6;
    [SerializeField] private int _poolMaxSize = 30;

    private WaitForSeconds _delaySpawn = new WaitForSeconds(1f);

    private ObjectPool<Cube> pool;

    private void Awake()
    {
        pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefabToSpawn),
            actionOnGet: prepareGetFromPool,
            actionOnRelease: ReleasePool,
            actionOnDestroy: DestroyOnPool,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private void prepareGetFromPool(Cube cube)
    {
        cube.transform.position = _spawnPos.SpawnDetect();

        if (cube.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.linearVelocity =Vector3.zero;
        }

        cube.CallingDeath += ReleasePool;

        cube.gameObject.SetActive(true);
    }

    private void ReleasePool(Cube cube)
    {              
        _collisionRecolor.ResetToDefault(cube);

        cube.gameObject.SetActive(false);
        cube.CallingDeath -= ReleasePool;
    }

    private void DestroyOnPool(Cube cube)
    {
        Destroy(cube.gameObject);
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
