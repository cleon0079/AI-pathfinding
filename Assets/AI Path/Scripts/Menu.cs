using UnityEngine;
using UnityEngine.SceneManagement;

namespace CleonAI
{
    public class Menu : MonoBehaviour
    {
        public void StartGame()
        {
            // Load the Game Scene
            SceneManager.LoadScene(1);
        }

        public void BackToMenu()
        {
            // Load the Menu Scene
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            // Quit the game in editor mode
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            // Quit the game in build
            Application.Quit();
        }
    }
}
