using UnityEngine;

public abstract class MinigameBehaviour : MonoBehaviour {
    protected MinigameData _data = null;
    public MinigameData Data { get => _data; set => _data = value; }
}
