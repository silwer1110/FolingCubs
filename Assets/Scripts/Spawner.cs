using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubesPool _cubes;

    private Color _baseColor = Color.white;
    private readonly float _spawnInterval = 0.5f;
    private int _maxCubeCount = 20;

    private void Start()
    {
        _cubes.CreateObjectPool();
        StartCoroutine(SpawnLoop(_spawnInterval));
    }

    private IEnumerator SpawnLoop(float delay)
    {
        WaitForSeconds wait = new(delay);

        while (true)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        Cube cube;

        if (_maxCubeCount >= _cubes.GetActiveCubeCount())
        {
            cube = _cubes.GetCube();

            cube.Deactivated += ReturnToPool;

            cube.Init(GetRandomPosition(), _baseColor);
        }
    }

    private void ReturnToPool(Cube cube)
    {
        _cubes.ReleaseCube(cube);

        cube.Deactivated -= ReturnToPool;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 position = transform.position;

        float radius = 50f;

        position += (UnityEngine.Random.insideUnitSphere + Vector3.left + Vector3.right) * radius;

        return position;
    }
}