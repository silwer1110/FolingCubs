using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Renderer _renderer;

    public event Action<Cube> Deactivated;

    public bool IsCollided { get; private set; } = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void StartDestruction()
    {
        StartCoroutine(LifeTimeCauntUp());

        GetRandomColor();

        IsCollided = true;
    }

    public void Init(Vector3 position, Color color)
    {
        transform.position = position;

        _renderer.material.color = color;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private IEnumerator LifeTimeCauntUp()
    {
        float delay = 1;
        float lifeTime = GetLifeTime();

        WaitForSecondsRealtime wait = new(delay);

        while (lifeTime > 0)
        {
            yield return wait;
            lifeTime--;
        }

        Deactivate();
        StopCoroutine(LifeTimeCauntUp());
    }

    private void GetRandomColor()
    {
        _renderer.material.color = UnityEngine.Random.ColorHSV();
    }

    private void Deactivate()
    {
        IsCollided = false;
        Deactivated?.Invoke(this);
    }

    private int GetLifeTime()
    {
        int minLifeTime = 2;
        int maxLifeTime = 5;

        return UnityEngine.Random.Range(minLifeTime, maxLifeTime + 1);
    }
}