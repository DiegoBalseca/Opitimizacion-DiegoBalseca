using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 10f;
    private Vector3 _direction;
    private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        _rigidbody.linearVelocity = Vector3.zero;      // limpiar velocidad anterior
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.AddForce(_direction * _bulletSpeed, ForceMode.Impulse);
    }

    public void SetDirection(Vector3 dir)
    {
        _direction = dir;
    }

    void OnTriggerEnter(Collider other)
    {
        // Desactivar la bala al tocar cualquier enemigo o jugador
        gameObject.SetActive(false);
    }
}