// ChangeSceneButton.cs

using UnityEngine.Networking;

public class ChangeSceneButton : NetworkBehaviour
{
    [Server]
    public void ChangeScene (string sceneName)
    {
        NetworkManager.singleton.ServerChangeScene(sceneName);
        // TODO: show loading screen
    }
}