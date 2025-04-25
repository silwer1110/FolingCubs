using UnityEngine;
using UnityEngine.Pool;

public class CubesPool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private ObjectPool<Cube> _cubes;
    private int _maxSize = 20;
    private readonly int _capacity = 10;

    public void CreateObjectPool()
    {
        _cubes = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (cube) => cube.gameObject.SetActive(true),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            defaultCapacity: _capacity,
            maxSize: _maxSize
        );
    }

    public int GetActiveCubeCount()
    {
        return _cubes.CountActive;
    }

    public Cube GetCube()
    {
        return _cubes.Get();
    }

    public void ReleaseCube(Cube cube)
    {
        _cubes.Release(cube);
    }
}