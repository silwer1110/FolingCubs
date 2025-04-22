using UnityEngine;

public class HitHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Cube cube) && cube.IsDestroyed == false)
            cube.StartDestruction();
    }
}