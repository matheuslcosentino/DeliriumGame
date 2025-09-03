using UnityEngine;

public class NameManager : MonoBehaviour
{
    public static NameManager Instance { get; private set; }
    public string PlayerName { get; private set; }

    const string Key = "PLAYER_NAME";

private void Awake()
{
    if (Instance != null && Instance != this) { Destroy(gameObject); return; }
    Instance = this;

    if (transform.parent == null)
        DontDestroyOnLoad(gameObject);

    PlayerName = PlayerPrefs.GetString(Key, "");
}

    public void SetName(string value, bool save = true)
    {
        PlayerName = (value ?? "").Trim();
        if (save) PlayerPrefs.SetString(Key, PlayerName);
    }

    public bool HasName() => !string.IsNullOrEmpty(PlayerName);
}
