using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private float _lifeTime;
    private Rigidbody _rigidbody;
    private Renderer _renderer;

    public event UnityAction<Cube> Deactivated;

    public bool IsDestroyed { get; private set; } = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _lifeTime = GetLifeTime();
    }

    public void StartDestruction()
    {
        GetRandomColor();

        IsDestroyed = true;

        Invoke(nameof(Deactivate), _lifeTime);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    public void StopMotion() 
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void GetRandomColor()
    {
        _renderer.material.color = UnityEngine.Random.ColorHSV();
    }

    private void Deactivate()
    {
        IsDestroyed = false;
        Deactivated?.Invoke(this);
    }

    private int GetLifeTime()
    {
        int minLifeTime = 2;
        int maxLifeTime = 5;

        return UnityEngine.Random.Range(minLifeTime, maxLifeTime + 1);
    }
}