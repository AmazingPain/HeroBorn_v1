using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomExstansion
{
    public static class StringExtensions
    {
        public static void FancyDebug(this string str)
        {
            Debug.LogFormat("This string contains {0} characters.", str.Length);
        }
    }
}
