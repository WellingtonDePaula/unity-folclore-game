using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour, ISaveable {

    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    [SerializeField] Rigidbody2D _body;
    [SerializeField] EntityStats _stats;
    [SerializeField] Animator _animator;

    [SerializeField] float _moveSpeed;
    Vector2 _direction;

    PlayerMovement _movement;
    PlayerInputReader _inputReader = null;

    [SerializeField] string _sprite = "Idle";
    [SerializeField] string _orientation = "Front";

    [SerializeField] GameObject _projectileRef;

    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Rigidbody2D Body { get { return _body; } }
    public EntityStats Stats { get { return _stats; } }
    public Animator Animator { get { return _animator; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public Vector2 Direction { get { return _direction; } set { _direction = value; } }
    public PlayerMovement Movement { get { return _movement; } }
    public PlayerInputReader InputReader { get { return _inputReader; } }
    public string Sprite { get => _sprite; set => _sprite = value; }
    public string Orientation { get => _orientation; set => _orientation = value; }
    public PlayerStateFactory States { get => _states; }

    private void Awake() {
        _movement = new PlayerMovement(this);

        _moveSpeed = _stats.BaseSpeed;

        _states = new PlayerStateFactory(this);
        _currentState = _states.Idle();
        if(SaveManager.Instance) {
            SaveManager.Instance.RegisterSaveable(this);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _inputReader = PlayerInputReader.Instance;
    }

    // Update is called once per frame
    void Update() {
        if(!GameManager.Instance.GamePaused) {
            SwitchSpriteDirection();

            _currentState.UpdateState();
        }
    }

    private void FixedUpdate() {
        if (!GameManager.Instance.GamePaused) {
            _currentState.FixedUpdateState();
        }
    }

    private void SwitchSpriteDirection() {
        float xVelocity = _direction.x;
        float yVelocity = _direction.y;

        if(yVelocity != 0) {
            if(yVelocity > 0) {
                _orientation = "Back";
            } else {
                _orientation = "Front";
            }
        }
        if (xVelocity != 0) {
            _orientation = "Side";
            gameObject.transform.localScale = new Vector2(Mathf.Sign(xVelocity), gameObject.transform.localScale.y);
        }
        PlayAnimation();
    }

    public void Shoot(Vector3 direction) {
        GameObject projectile = Instantiate(_projectileRef, gameObject.transform.position, Quaternion.identity);
        projectile.GetComponent<ProjectileBehaviour>().ShooterStats = _stats;

        projectile.GetComponent<ProjectileBehaviour>().Direction = new Vector3(direction.x, direction.y).normalized;
    }

    public void PlayAnimation() {
        string targetAnimationName = _sprite + _orientation;

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (!stateInfo.IsName(targetAnimationName)) {
            _animator.Play(targetAnimationName, 0, 0);
        }
    }

    public void SaveObject(string sceneName) {
        PlayerPrefs.SetFloat($"{sceneName}:playerX", gameObject.transform.position.x);
        PlayerPrefs.SetFloat($"{sceneName}:playerY", gameObject.transform.position.y);
    }

    public void LoadObject(string sceneName) {
        float playerX, playerY;
        playerX = PlayerPrefs.GetFloat($"{sceneName}:playerX", transform.position.x);
        playerY = PlayerPrefs.GetFloat($"{sceneName}:playerY", transform.position.y);
        gameObject.transform.position = new Vector3(playerX, playerY, 0);
    }

    public void Deactive() {
        if(SaveManager.Instance) {
            SaveManager.Instance.RemoveSaveable(this);
        }
    }

    private void OnDestroy() {
        Deactive();
    }
}
