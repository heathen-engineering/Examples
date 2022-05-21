using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Heathen.Examples
{
    public class ExampleBehaviour : MonoBehaviour
    {
        public ComplexDataEvent complexDataEvent;
        public MultipleParamEvent mutlipleParamEvent;
        public FloatEvent floatEvent;
        public GameObjectEvent gameObjectEvent;

        public List<string> bonusExamplesData = new List<string>();

        private void Start()
        {
            //Because this event wont show in editor we must add listeners to it in code
            mutlipleParamEvent.AddListener(HandleMultipleParamEvent);

            //This is just a bit of extra showing the use of delegates in LINQ using Lambda expression and is meant to inspire you to learn more
            //Less so is it meant to teach anything practical ... we may do a seperate sample on LINQ and another on Lambda if there is call for it
            LambdaExpressionOfDelegates();

            //This calls the async method which uses our Action callbacks
            StartCoroutine(AsyncMethod(HandleProgressCallback, HandleCompletedCallback));
        }

        private void OnDestroy()
        {
            //This is really just here to show you how to remove a listener from code
            mutlipleParamEvent.RemoveListener(HandleMultipleParamEvent);
        }

        private void HandleProgressCallback(float arg0)
        {
            Debug.Log($"HandleProgressCallback invoked with arg0 = {arg0}");
        }

        private void HandleCompletedCallback(bool arg0, string arg1)
        {
            Debug.Log($"HandleCompletedCallback invoked with arg0 = {arg0} and arg1 = {arg1}");
        }

        /// <summary>
        /// We attach this event via script in the Start and remove it in the OnDestroy
        /// </summary>
        private void HandleMultipleParamEvent(string arg0, int arg1, bool arg2, float arg3)
        {
            // Bonus tip, in C#6 and later you can use string interpolation
            // Notice the $ at the start before the first "
            // That tells the compiler that this string uses variables 
            // Interpolated strings can do some crazy things read more here https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated
            Debug.Log($"HandleWontShowInEditor was invoked with arg0 = {arg0}, arg1 = {arg1}, arg2 = {arg2}, arg3 = {arg3}");
        }

        /// <summary>
        /// This is attached in the Unity Editor to the Complex Data Event 
        /// </summary>
        /// <param name="arg0"></param>
        public void HandleComplexDataEvent(ExampleComplexData arg0)
        {
            Debug.Log($"HandleComplexDataEvent was invoked with arg0 = {{someBool = {arg0.someBool} someInt = {arg0.someInt} and someString = {arg0.someString}}}");
        }

        /// <summary>
        /// This is attached in the Unity Editor to the Float Event
        /// </summary>
        /// <param name="arg0"></param>
        public void HandleFloatEvent(float arg0)
        {
            Debug.Log($"HandleFloatEvent was invoked with arg0 = {arg0}");
        }

        /// <summary>
        /// This is attached in the editor to the GameObject Event
        /// </summary>
        /// <param name="arg0"></param>
        public void HandleGameObjectEvent(GameObject arg0)
        {
            Debug.Log($"HandleFloatEvent was invoked with arg0 = {(arg0 == null ? "null" : arg0.name)}");
        }

        /// <summary>
        /// This is simply an example of how to invoke an event ... you would usually invoke an event as part of some larger process
        /// in this case I simply wanted an easy way for you to see the event get invoked so I make it so I could attach it to a button
        /// this is a silly use case but shows you code working and invoking the event
        /// </summary>
        public void InvokeComplexDataEvent()
        {
            complexDataEvent.Invoke(new ExampleComplexData
            {
                someBool = true,
                someInt = 42,
                someString = "Hello delegate usage!"
            });
        }

        /// <summary>
        /// This shows the invoke of a multi-param event
        /// </summary>
        public void InvokeMultipleParamEvent()
        {
            mutlipleParamEvent.Invoke("Hello mutli-param delegate", 84, true, 4.2f);
        }

        /// <summary>
        /// This shows the invoke of a float event passing in a value of 42.5
        /// </summary>
        public void InvokeFloatEvent()
        {
            floatEvent.Invoke(42.5f);
        }

        /// <summary>
        /// This shows the invoke of the game object event passing in a null
        /// </summary>
        public void InvokeGameObjectEvent()
        {
            gameObjectEvent.Invoke(null);
        }

        /// <summary>
        /// This simply gives us some async process so we can demonstrate the use of the callbacks
        /// </summary>
        /// <param name="progressCallback">
        /// Action works a lot like UnityEvent in that it defines the type of the paramiters the delegate expects
        /// This delegate will be called mutliple times and represents progress completed in this hypothetical use case
        /// </param>
        /// <param name="completedCallback">
        /// Action works a lot like UnityEvent in that it defines the type of the paramiters the delegate expects
        /// This delegate will be called at the end of the process and represents completion in this hypothetical use case
        /// </param>
        /// <returns></returns>
        private IEnumerator AsyncMethod(Action<float> progressCallback, Action<bool, string> completedCallback)
        {
            yield return new WaitForSeconds(1);
            progressCallback?.Invoke(0.1f);
            yield return new WaitForSeconds(2);
            progressCallback?.Invoke(0.2f);
            yield return new WaitForSeconds(3);
            progressCallback?.Invoke(0.3f);
            yield return new WaitForSeconds(4);
            progressCallback?.Invoke(0.4f);
            yield return new WaitForSeconds(5);
            progressCallback?.Invoke(0.5f);
            yield return new WaitForSeconds(6);
            progressCallback?.Invoke(0.6f);
            yield return new WaitForSeconds(7);
            progressCallback?.Invoke(0.7f);
            yield return new WaitForSeconds(8);
            progressCallback?.Invoke(0.8f);
            yield return new WaitForSeconds(9);
            progressCallback?.Invoke(0.9f);
            yield return new WaitForSeconds(10);
            progressCallback?.Invoke(1f);

            completedCallback?.Invoke(true, "All Done!");
        }

        /// <summary>
        /// This method uses lambda expression to define delegates used in various LINQ searchs
        /// LINQ can be used efficently ... this is not a demonstration of that
        /// rather this demonstration is meant to excite you about the possible use cases of LINQ and Lambda expression and get you to do some research on you own ;)
        /// </summary>
        private void LambdaExpressionOfDelegates()
        {
            //This first example is just a bit dumb :) 
            //It is not the best or even a reasonable way to do this
            //What I am showing is that you can test and see if a collection contains a value by using a comparison delegate defined in lambda expression
            if(bonusExamplesData.Any(value => value == "Hello World"))
            {
                //Similarlly I am showing that you can use LINQ to return the reference that matches the delgate if found or a defult value if not
                var indexOf = bonusExamplesData.FirstOrDefault(value => value == "Hello World");
                Debug.Log($"I found the string '{indexOf}'");
            }

            //This simply shows that you can compare fields and attributes within the elements not just the elements them selves
            // So in this case we are saying for each value in the collection see if its length is longer than 20 and if so stop and return that object if none found return default
            var TwintyOrLonger = bonusExamplesData.FirstOrDefault(value => value.Length > 20);
            if (TwintyOrLonger != default)
                Debug.Log($"Found a value of {TwintyOrLonger} which is more than 20 characters long");
            else
                Debug.Log("No match found longer than 20 characters");
        }
    }
}
