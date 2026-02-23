using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float _speed = 5f; // velocidad de bajada

    [Header("Disparo")]
    [SerializeField] private string _bulletPoolName = "EnemyBullets"; // pool de balas enemigas
    [SerializeField] private float _shootInterval = 1.5f; // tiempo entre disparos
    private float _shootTimer = 0f;

    private void OnEnable()
    {
        // Forzar que la nave siempre aparezca recta
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _shootTimer = 0f; // reinicia timer al aparecer
    }

    private void Update()
    {
        MoveDown();
        HandleShooting();
        CheckOffScreen();
    }

    // Movimiento hacia abajo
    private void MoveDown()
    {
        transform.Translate(Vector3.back * _speed * Time.deltaTime);
    }

    // Control del disparo con timer
    private void HandleShooting()
    {
        _shootTimer += Time.deltaTime;

        if (_shootTimer >= _shootInterval)
        {
            Shoot();
            _shootTimer = 0f; // reinicia timer
        }
    }

    // Disparo usando PoolManager
    private void Shoot()
    {
        GameObject bullet = PoolManager.Instance.GetPooledObject(_bulletPoolName, transform.position, Quaternion.identity);

        if (bullet != null)
        {
            bullet.GetComponent<Bullet>().SetDirection(Vector3.back); // disparo hacia abajo
            bullet.SetActive(true);
        }
    }

    // Desactivar enemigo al salir de la pantalla
    private void CheckOffScreen()
    {
        if (transform.position.z < -10f) // ajustar según tu escena
        {
            gameObject.SetActive(false);
        }
    }

    // Detectar colisión con balas del jugador
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }
    }
}