using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Heathen.BootstrapExample
{
    public class GameLogic : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(UnloadTitle());
        }

        /// <summary>
        /// Called when the player clicks the exit button
        /// </summary>
        public void ExitGameClick()
        {
            StartCoroutine(LoadTitleScene());
        }

        /// <summary>
        /// Called on start for this script.
        /// This simply unloads the title scene
        /// </summary>
        /// <returns></returns>
        private IEnumerator UnloadTitle()
        {
            // We would check if we loaded the game scene correctly and that all was ready to play without error before unloading title
            // When the validation for game was done we would start the process to unload the title scene

            var operation = SceneManager.UnloadSceneAsync(1);
            while (!operation.isDone)
            {
                //Unloading the title scene is the second half of the work to do
                LoadingScreenDisplay.Progress = 0.5f + (operation.progress * 0.5f);
                yield return new WaitForEndOfFrame();
            }

            //We are now done unloading
            LoadingScreenDisplay.Showing = false;
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

            var operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            // Tell unity to activate the scene soon as its ready
            operation.allowSceneActivation = true;

            // While the title scene is loading update the progress 
            while (!operation.isDone)
            {
                //Loading the title scene is only half the effort we need to do
                LoadingScreenDisplay.Progress = operation.progress * 0.5f;
                yield return new WaitForEndOfFrame();
            }

            //The title sceen is now loaded and its logic should be starting
            LoadingScreenDisplay.Progress = 0.5f;
        }
    }
}