using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Polyperfect.Common;

#if UNITY_EDITOR
namespace Polyperfect.Dinos
{
    [CustomEditor(typeof(Dino_WanderScript))]
    [CanEditMultipleObjects]
    public class Dino_WanderScriptEditor : Common_WanderScriptEditor {
    }
}
#endif