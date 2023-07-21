using UnityEngine;
using UnityEngine.Events;

public class Force : MonoBehaviour
{
    [SerializeField] private int _forceValue;

    public int ForceValue => _forceValue;

    public event UnityAction<Force> Offend;

    private void OnTriggerEnter(Collider other)
    {
        Offend?.Invoke(this);
    }
}
