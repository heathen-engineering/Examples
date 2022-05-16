using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Heathen.BootstrapExample.DNDoL
{
    public class BootstrapLogic : MonoBehaviour
    {
        [SerializeField]
        private LoadingScreenDisplay loadingScreenDisplay;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            LoadingScreenDisplay.instance = loadingScreenDisplay;
            StartCoroutine(Validate());
        }

        /// <summary>
        /// XML based comments like these will show up in Visual Studio intelisince making it very easy to document your own code ... try it, mouse over the Validate() on line 11 in the start method.
        /// </summary>
        /// <remarks>
        /// Always write comments about your methods to discribe what they should do and or how they should be used
        /// </remarks>
        /// <returns></returns>
        private IEnumerator Validate()
        {
            yield return null;

            // In a real validate method we would check the state of any dependent systems or integrations such as Steam and make sure we have loaded any required data such as system settings

            Debug.Log("Waiting for 3 ...");
            yield return new WaitForSeconds(1f);

            Debug.Log("Waiting for 2 ...");
            yield return new WaitForSeconds(1f);

            Debug.Log("Waiting for 1 ...");
            yield return new WaitForSeconds(1f);

            Debug.Log("Loading title scene ...");

            // In this example we simply waite for 3 seconds, in a real game you would be showing splash screens, loading data, poling hardware and confirming licenses

            // When ready would load the title scene, which we can do by index since we know its the second scene in the build. This is much faster than loading by name

            // Its very important that we load the scene additivly, this indicates that Unity should not unload any existing scenes
            // For an additive structure we want to manually control what gets unloaded and when,
            // and we never want to unload the bootstrap scene as it will house our main camera and other system level objects

            //Show the loading screen
            LoadingScreenDisplay.Progress = 0;
            LoadingScreenDisplay.Showing = true;

            var operation = SceneManager.LoadSceneAsync(1);
            // Tell unity to activate the scene soon as its ready
            operation.allowSceneActivation = true;

            // While the title scene is loading update the progress 
            while (!operation.isDone)
            {
                //Loading the title scene
                LoadingScreenDisplay.Progress = operation.progress;
                yield return new WaitForEndOfFrame();
            }

            //The title sceen is now loaded and its logic should be starting
            LoadingScreenDisplay.Progress = 1f;
        }
    }
}
