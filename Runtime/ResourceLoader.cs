using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NduGames.ResourceLoader
{
    public class ResourceLoader : ScriptableObject
    {
        [SerializeField]
        private string _subFolderPath;

        [SerializeField]
        private List<Resource> _resources;

    public UnityEngine.Object LoadResource(string name)
        {
            return null;
        }
    }
}
