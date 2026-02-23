using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float _speed = 10f;        // Velocidad de movimiento
    [SerializeField] private float _xLimit = 8f;       // Límite lateral fijo

    private Vector2 _moveInput;
    private InputAction _moveAction;
    private InputAction _shootAction;

    private Rigidbody _rb;

    [Header("Disparo")]
    [SerializeField] private Transform _BulletSpawn;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        // Bloquea rotaciones y movimientos no deseados
        _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }

    void Start()
    {
        _moveAction = InputSystem.actions["Move"];
        _shootAction = InputSystem.actions["Attack"];
    }

    void Update()
    {
        HandleInput();
        HandleShooting();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleInput()
    {
        if (_moveAction != null)
            _moveInput = _moveAction.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        // Movimiento horizontal con velocidad constante
        Vector3 move = new Vector3(_moveInput.x, 0, 0) * _speed * Time.fixedDeltaTime;
        Vector3 newPos = _rb.position + move;

        // Limitar dentro de los bordes fijos
        float clampedX = Mathf.Clamp(newPos.x, -_xLimit, _xLimit);
        _rb.MovePosition(new Vector3(clampedX, newPos.y, newPos.z));
    }

    private void HandleShooting()
    {
        if (_shootAction != null && _shootAction.WasPerformedThisFrame())
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = PoolManager.Instance.GetPooledObject("Balas", _BulletSpawn.position, _BulletSpawn.rotation);
        if (bullet != null)
        {
            bullet.GetComponent<Bullet>().SetDirection(Vector3.forward);
            bullet.SetActive(true);
        }
    }
}