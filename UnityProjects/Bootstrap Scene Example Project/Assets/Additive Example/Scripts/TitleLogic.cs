using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Heathen.BootstrapExample
{
    public class TitleLogic : MonoBehaviour
    {
        private void Start()
        {
            //If the game scene is currently loaded we will unload it
            //If the game scene is not currently loaded then we just came from the bootstrap and this is the first load for title this session

            if (SceneManager.GetSceneByBuildIndex(2).isLoaded)
                StartCoroutine(UnloadGameScene());
            else
                LoadingScreenDisplay.Showing = false;
        }

        /// <summary>
        /// Called when the player clicks the Play button
        /// </summary>
        public void LoadGameClicked()
        {
            StartCoroutine(LoadGameScene());
        }

        /// <summary>
        /// Called when returning from a game session
        /// </summary>
        /// <returns></returns>
        private IEnumerator UnloadGameScene()
        {
            // This happens when we return to the title from the game scene

            var operation = SceneManager.UnloadSceneAsync(2);
            while (!operation.isDone)
            {
                //Unloading the game scene is the second half of the work to do
                LoadingScreenDisplay.Progress = 0.5f + (operation.progress * 0.5f);
                yield return new WaitForEndOfFrame();
            }

            //We are now done unloading
            LoadingScreenDisplay.Showing = false;
        }

        /// <summary>
        /// Called when the player is ready to start a game session
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadGameScene()
        {
            //First show the loading screen and wait a frame or two for it to actually display

            LoadingScreenDisplay.Progress = 0f;
            LoadingScreenDisplay.Showing = true;

            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            var operation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
            // Tell unity to activate the scene soon as its ready
            operation.allowSceneActivation = true;

            // While the game scene is loading update the progress 
            while(!operation.isDone)
            {
                //Loading the game scene is only half the effort we need to do
                LoadingScreenDisplay.Progress = operation.progress * 0.5f;
                yield return new WaitForEndOfFrame();
            }

            //The game sceen is now loaded and its logic should be starting
            LoadingScreenDisplay.Progress = 0.5f;
        }
    }
}