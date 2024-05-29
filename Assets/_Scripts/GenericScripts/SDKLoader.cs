using SupersonicWisdomSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SDKLoader : MonoBehaviour
{
    void Awake()
    {
        SupersonicWisdom.Api.AddOnReadyListener(OnSupersonicWisdomReady);
        // Then initialize
        SupersonicWisdom.Api.Initialize();

        // Subscribe
    }
    void OnSupersonicWisdomReady()
    {
        SceneManager.LoadSceneAsync(1);
        // Start your game from this point
    }
}
