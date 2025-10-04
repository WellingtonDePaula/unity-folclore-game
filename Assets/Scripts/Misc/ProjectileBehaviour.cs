using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {
    [SerializeField] float _damageMultiplier;
    Vector3 _direction;
    EntityStats _shooterStats;

    public Vector3 Direction { get => _direction; set => _direction = value; }
    public EntityStats ShooterStats { get => _shooterStats; set => _shooterStats = value; }

    private void Start() {
        Destroy(gameObject, 5);
    }

    private void FixedUpdate() {
        if (_shooterStats) {
            Move();
        }
    }

    void Move() {
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = _direction * _shooterStats.AttackSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (_shooterStats) {
            if (collision.CompareTag("Enemy")) {
                collision.gameObject.GetComponent<EntityStats>().Hp -= _shooterStats.AttackDamage * _damageMultiplier;
            }
            if (collision.CompareTag("Wall")) {
                Debug.Log("ué");
                Destroy(gameObject);
            }
        }
    }
}
