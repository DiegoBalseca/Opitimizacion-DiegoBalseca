using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float _speed = 3f;

    [Header("Disparo")]
    [SerializeField] private string _bulletPoolName = "EnemyBullets";
    [SerializeField] private float _shootInterval = 1f; // disparo cada 1 segundo
    private float _shootTimer = 0f;

    private void Update()
    {
        MoveDown();
        HandleShooting();
        CheckOffScreen();
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.back * _speed * Time.deltaTime);
    }

    private void HandleShooting()
    {
        _shootTimer += Time.deltaTime;
        if (_shootTimer >= _shootInterval)
        {
            Shoot();
            _shootTimer = 0f;
        }
    }

    private void Shoot()
    {
        GameObject bullet = PoolManager.Instance.GetPooledObject(_bulletPoolName, transform.position, Quaternion.identity);

        if (bullet != null)
        {
            bullet.GetComponent<Bullet>().SetDirection(Vector3.back); // disparo hacia abajo
            bullet.SetActive(true);
        }
    }

    private void CheckOffScreen()
    {
        if (transform.position.z < -10f) // ajustar según tu escena
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) // bala del jugador
        {
            gameObject.SetActive(false);
        }
    }
}