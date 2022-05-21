using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.Examples
{
    /**************************************************************
     * 
     * This file contains examples of several custom Unity Events
     * You will note that none of the example classes have a body
     * at all. UnityEvents do not rquire a body simply a defintion
     * and to be marked as Serializable such that the Unity Editor's
     * Inspector can properly draw them
     * 
     **************************************************************/

    /// <summary>
    /// Unity Events can only handle 4 paramiters so if you need more or simply if you prefer fewer and more compact paramiters 
    /// we would use a serialized object as shown below
    /// </summary>
    [System.Serializable]
    public struct ExampleComplexData
    {
        public string someString;
        public int someInt;
        public bool someBool;
    }

    /// <summary>
    /// This will act as an event that takes a single paramiter of type ExampleComplexData
    /// </summary>
    [System.Serializable]
    public class ComplexDataEvent : UnityEvent<ExampleComplexData>
    { }

    /// <summary>
    /// UnityEvents can handle up to 4 paramiters
    /// </summary>
    [System.Serializable]
    public class MultipleParamEvent : UnityEvent<string, int, bool, float>
    { }

    /// <summary>
    /// This simply shows the use of a primative type
    /// </summary>
    [System.Serializable]
    public class FloatEvent : UnityEvent<float>
    { }

    /// <summary>
    /// This shows the use of a class type
    /// </summary>
    [System.Serializable]
    public class GameObjectEvent : UnityEvent<GameObject>
    { }
}
