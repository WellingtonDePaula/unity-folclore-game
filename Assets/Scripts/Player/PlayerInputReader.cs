using UnityEngine;
//using UnityEngine.InputSystem;

/// <summary>
/// Responsável por ler e armazenar os inputs do jogador.
/// Centraliza todos os comandos de entrada.
/// </summary>
public class PlayerInputReader : MonoBehaviour {
    private PlayerControls _controls;
    public Vector2 MoveInput { get; private set; }
    public bool Interact { get; private set; }
    public bool MinigamePress { get; private set; }
    public bool Dash { get; private set; }
    public static PlayerInputReader Instance { get; private set; }

    private void Awake() {
        _controls = new PlayerControls();

        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }

        _controls.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        _controls.Player.Move.canceled += ctx => MoveInput = Vector2.zero;

        _controls.Player.Interact.performed += ctx => Interact = true;
        _controls.Player.Interact.canceled += ctx => Interact = false;

        _controls.Player.MinigamePress.performed += ctx => MinigamePress = true;
        _controls.Player.MinigamePress.canceled += ctx => MinigamePress = false;

        _controls.Player.Dash.performed += ctx => Dash = true;
        _controls.Player.Dash.canceled += ctx => Dash = false;
    }

    private void OnEnable() {
        _controls.Enable(); // Habilita todos os Action Maps
    }

    private void OnDisable() {
        _controls.Disable(); // Desabilita todos os Action Maps
    }

    public void ConsumeInteractInput() {
        Interact = false;
    }
    public void ConsumeMinigamePressInput() {
        MinigamePress = false;
    }
    public void ConsumeDashInput() {
        Dash = false;
    }
}
