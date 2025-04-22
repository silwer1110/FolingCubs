using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubesPool _cubes;

    private Color _baseColor = Color.white;
    private readonly float _spawnInterval = 0.5f;

    private void Start()
    {
        _cubes.CreateObjectPool();
        InvokeRepeating(nameof(Spawn), 0.0f, _spawnInterval);
    }

    private void Spawn()
    {
        if (_cubes.Cubes.CountActive < _cubes.MaxSize)
            SetUpCube(_cubes.Cubes.Get());
    }

    private void SetUpCube(Cube cube)
    {
        cube.Deactivated += BackTooPool;

        cube.StopMotion();

        cube.SetColor(_baseColor);

        cube.SetPosition(GetRandomPosition());
    }

    private void BackTooPool(Cube cube)
    {
        _cubes.Cubes.Release(cube);

        cube.Deactivated -= BackTooPool;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 position = transform.position;

        float radius = 50f;

        position += (UnityEngine.Random.insideUnitSphere + Vector3.left + Vector3.right) * radius;

        return position;
    }
}