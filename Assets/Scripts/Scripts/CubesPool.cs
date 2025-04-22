using UnityEngine;
using UnityEngine.Pool;

public class CubesPool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private ObjectPool<Cube> _cubes;
    private readonly int _capacity = 10;

    public int MaxSize { get; private set; } = 20;
    public ObjectPool<Cube> Cubes => _cubes;

    public void CreateObjectPool()
    {
        _cubes = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (cube) => cube.gameObject.SetActive(true),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            defaultCapacity: _capacity,
            maxSize: MaxSize
        );
    }
}