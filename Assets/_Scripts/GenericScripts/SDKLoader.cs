using UnityEngine;
using UnityEngine.SceneManagement;

public class SDKLoader : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
