using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FNAF
{
    public class AwakeLoader : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void StartGame();

        private void Start()
        {
            StartGame();
        }

        public void Loading()
        {
            SceneManager.LoadScene(1);
        }
    }
}