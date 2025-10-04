using UnityEngine;
using DG.Tweening; // Importe o namespace DOTween

public class UIShaker : MonoBehaviour {
    [Header("Configurações do Shake")]
    public float duration = 0.5f;   // Duração total do shake
    public float strength = 10f;    // Intensidade do shake (amplitude do movimento em pixels)
    public int vibrato = 10;        // Quantidade de "vibrações" durante o shake
    public float randomness = 90f;  // Aleatoriedade do shake (0 a 180)
    public bool fadeOut = true;     // Se o shake deve diminuir a intensidade ao longo do tempo

    private RectTransform rectTransform;

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null) {
            Debug.LogError("UIShaker: Este script deve estar em um GameObject com RectTransform.", this);
            enabled = false;
        }
    }

    public void DoUIShake() {
        rectTransform.DOKill(true);

        rectTransform.DOShakeAnchorPos(duration, strength, vibrato, randomness, fadeOut)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => {
                rectTransform.anchoredPosition = Vector2.zero;
            });
    }
}