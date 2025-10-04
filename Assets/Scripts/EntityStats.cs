using UnityEngine;

public class EntityStats : MonoBehaviour {
    [Header("Attack")]
    [SerializeField] float _attackDamage;
    [SerializeField] float _attackSpeed;
    [SerializeField] float _criticalChance;
    [SerializeField] float _expMultiplier;
    [Header("Movement")]
    [SerializeField] float _baseSpeed;
    [SerializeField] float _baseDashSpeed;
    [Header("Health")]
    [SerializeField] float _maxHp;
    float _hp;
    public float AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }
    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
    public float BaseSpeed { get { return _baseSpeed; } set { _baseSpeed = value; } }
    public float BaseDashSpeed { get { return _baseDashSpeed; } set { _baseDashSpeed = value; } }
    public float MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public float Hp { get { return _hp; } set { _hp = Mathf.Clamp(value, 0, _maxHp); if (_hp < 0) { Die(); } } }
    public float CriticalChance { get { return _criticalChance; } set { _criticalChance = value; } }
    public float ExpMultiplier { get { return _expMultiplier; } set { _expMultiplier = value; } }

    private void Awake() {
        _hp = _maxHp;
    }

    private void Die() {
        Destroy(this.gameObject);
    }
}
