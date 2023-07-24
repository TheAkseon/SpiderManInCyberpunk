using UnityEngine;

public class DestroyOverLifetime : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private void Update()
    {
        Destroy(gameObject, _lifeTime);
    }

    public void Destroy() => Destroy(gameObject);
}
