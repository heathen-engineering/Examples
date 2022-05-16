using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Heathen.BootstrapExample.DNDoL
{
    public class TitleLogic : MonoBehaviour
    {
        private void Start()
        {
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

            var operation = SceneManager.LoadSceneAsync(2);
            // Tell unity to activate the scene soon as its ready
            operation.allowSceneActivation = true;

            // While the game scene is loading update the progress 
            while(!operation.isDone)
            {
                //Loading the game scene is only half the effort we need to do
                LoadingScreenDisplay.Progress = operation.progress;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}