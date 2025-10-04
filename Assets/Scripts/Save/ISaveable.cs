using UnityEngine;

public interface ISaveable {
    public void SaveObject(string sceneName);
    public void LoadObject(string sceneName);
    public void Deactive();
}
