using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

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
                    return _resources.IndexOf(resource);
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw new IndexOutOfRangeException($"The index {index} is out of range. Currently {_resources.Count} resources exist.", ex);
                }
            }
        }

        public int EditResource(Resource oldResource, Resource newResource)
        {
            if (! _resources.Contains(oldResource))
            {
                throw new ArgumentException($"The resource '{oldResource.Name}' doesn't exist.", "oldResource");
            }
            else if (_resources.Contains(newResource))
            {
                throw new ArgumentException($"The resource '{newResource.Name}' already exists.", "newResource");
            }
            else
            {
                int index = _resources.IndexOf(oldResource);
                _resources.Insert(index, newResource);
                return index;
            }
        }

        public bool RemoveResource(Resource resource)
        {
            return _resources.Remove(resource);
        }

        public T LoadResource<T>(string name) where T : UnityEngine.Object
        {
            Resource resource = new Resource("", name);
            int index = _resources.IndexOf(resource);
            if (index == -1)
            {
                throw new ArgumentException($"The resource '{name}' doesn't exist.", "name");
            }
            resource = _resources[index];
            StringBuilder stringBuilder = new StringBuilder(_subDirectoryPath);
            if (String.IsNullOrEmpty(_subDirectoryPath))
            {
                stringBuilder.Append(resource.GetFullPath());
            }
            else
            {
                stringBuilder.Append($"/{resource.GetFullPath()}");
            }
            T loadedResource = Resources.Load<T>(stringBuilder.ToString());
            if (loadedResource == null)
            {
                throw new ResourceNotFoundException(this, resource);
            }
            else
            {
                return loadedResource;
            }
        }

        private void Awake()
        {
            _resources = new List<Resource>();
        }
    }
}
