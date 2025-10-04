using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/NoteData", order = 1)]
public class NoteData : ScriptableObject {
    public string content;
    public Sprite background;
    public int noteId;
}
