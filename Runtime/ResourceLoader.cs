using UnityEngine;
using System;
using System.Collections.Generic;

namespace NduGames.ResourceLoader
{
    public class ResourceLoader : ScriptableObject
    {
        [SerializeField]
        private string _subDirectoryPath;

        public string SubDisectoryPath
        {
            get
            {
                return _subDirectoryPath;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("SubDirectoryPath", "The SubDirectoryPath property cannot be null.");
                }
                else if (!IsPathValid(value))
                {
                    throw new ArgumentException("The SubDirectoryPath property must be in the format '<dir-1>/<dir-2>/.../<dir-n>'.", "SubDirectoryPath");
                }
                else
                {
                    _subDirectoryPath = value;
                }
            }
        }

        [SerializeField]
        private List<Resource> _resources;

        private bool IsPathValid(string path)
        {
            return (!path.Contains('\\'));
        }

        public int AddResource(Resource resource)
        {
            if (_resources.Contains(resource))
            {
                throw new ArgumentException($"The resource: '{resource.Name}' already exists.", "resource");
            }
            else
            {
                _resources.Add(resource);
                return _resources.IndexOf(resource);
            }
        }

        public int EditResource(int index, Resource resource)
        {
            if (_resources.Contains(resource))
            {
                throw new ArgumentException($"The resource '{resource.Name}' already exists.", "resource");
            }
            else
            {
                try
                {
                    _resources[index].Name = resource.Name;
                    _resources[index].DirectoryPath = resource.DirectoryPath;
                } catch (IndexOutOfRangeException ex)
                {
                    throw ex;
                }
                return _resources.IndexOf(resource);
            }
        }

        public void RemoveResource(Resource resource)
        {
            _resources.Remove(resource);
        }

        public T LoadResource<T>(string name) where T : UnityEngine.Object
        {
            Resource resource = new Resource("", name);
            resource = _resources[_resources.IndexOf(resource)];
            return Resources.Load<T>($"{_subDirectoryPath}/{resource.GetFullPath()}");
        }
    }
}
