using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string targetSceneName; // اسم المشهد الذي تريد الانتقال إليه

    public void SwitchScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
