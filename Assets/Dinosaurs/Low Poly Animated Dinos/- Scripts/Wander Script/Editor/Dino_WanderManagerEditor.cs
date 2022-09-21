using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Polyperfect.Common;

#if UNITY_EDITOR
namespace Polyperfect.Dinos
{
    [CustomEditor(typeof(Dino_WanderManagerEditor))]
    public class Dino_WanderManagerEditor : Common_WanderManagerEditor { }
}
#endif