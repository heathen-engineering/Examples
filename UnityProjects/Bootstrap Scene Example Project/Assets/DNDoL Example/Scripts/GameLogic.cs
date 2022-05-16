using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Heathen.BootstrapExample.DNDoL
{
    public class GameLogic : MonoBehaviour
    {
        private void Start()
        {
            LoadingScreenDisplay.Showing = false;
        }

        /// <summary>
        /// Called when the player clicks the exit button
        /// </summary>
        public void ExitGameClick()
        {
            StartCoroutine(LoadTitleScene());
        }

        /// <summary>
        /// Called when the game session is over to reload the title scene
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadTitleScene()
        {
            //First show the loading screen and wait a frame or two for it to actually display

            LoadingScreenDisplay.Progress = 0f;
            LoadingScreenDisplay.Showing = true;

            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            var operation = SceneManager.LoadSceneAsync(1);
            // Tell unity to activate the scene soon as its ready
            operation.allowSceneActivation = true;

            // While the title scene is loading update the progress 
            while (!operation.isDone)
            {
                //Loading the title scene is only half the effort we need to do
                LoadingScreenDisplay.Progress = operation.progress;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}