using UnityEngine;
using UnityEngine.SceneManagement;

namespace VHS
{
    public class NextLevelLoader:MonoBehaviour
    {
        public void LoadNextLevel()
        {
            PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("PlayerLevel") + 1);
            SceneManager.LoadScene(0);
        }
    }
}