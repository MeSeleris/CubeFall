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

    public ObjectPool<Cube> Pool { get; private set; }   

    private void Awake()
    {
        Pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefabToSpawn),
            actionOnGet: GetFromPool,
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

    private void GetFromPool(Cube obj)
    {
        obj.transform.position = _spawnPos.SpawnDetect();

        if (obj.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.linearVelocity =Vector3.zero;
        }

        obj.DeathTime += ReleasePool;

        obj.gameObject.SetActive(true);
    }

    private void ReleasePool(Cube obj)
    {        
        obj.DeathTime -= ReleasePool;
        _collisionRecolor.ResetToDefault(obj);

        obj.gameObject.SetActive(false);
    }

    private void DestroyOnPool(Cube obj)
    {
        Destroy(obj.gameObject);
    }

    private IEnumerator SpawnCoroutine()
    {
        while (enabled)
        {
            Pool.Get();
            yield return _delaySpawn;
        }
    }
}
