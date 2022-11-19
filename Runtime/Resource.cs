using System;
using UnityEngine;

namespace NduGames.ResourceLoader
{
    public class Resource
    {
        [SerializeField]
        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Name", "The Name property cannot be null.");
                }
                else if (! IsNameValid(value))
                {
                    throw new ArgumentException("The Name property cannot contain a '.'. File endings should not be specified.", "Name");
                }
                else
                {
                    _name = value;
                }
            }
        }

        [SerializeField]
        private string _directoryPath;

        public string DirectoryPath
        {
            get
            {
                return _directoryPath;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("DirectoryPath", "The DirectoryPath property cannot be null.");
                }
                else if (! IsPathValid(value))
                {
                    throw new ArgumentException("The DirectoryPath property must be in the format '<dir-1>/<dir-2>/.../<dir-n>'.");
                }
                else
                {
                    _directoryPath = value;
                }
            }
        }

        public Resource(string name, string path)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name", "The name cannot be null.");
            }
            else if (path == null)
            {
                throw new ArgumentNullException("path", "The path cannot be null.");
            }
            else if (! IsNameValid(name))
            {
                throw new ArgumentException("The name connot contain a '.'. File endings must not be specified.", "name");
            }
            else if (! IsPathValid(path))
            {
                throw new ArgumentException("The path must be in the format '<dir-1>/<dir-2>/.../<dir-n>'.", "path");
            }
            else
            {
                _name = name;
                _directoryPath = path;
            }
        }

        private bool IsNameValid(string name)
        {
            return (! name.Contains('.'));
        }

        private bool IsPathValid(string path)
        {
            return (! path.Contains('\\'));
        }

        public override bool Equals(object obj)
        {
            if (obj == null || ! this.GetType().Equals(obj.GetType()))
            {
                return false;
            } 
            else
            {
                Resource resourceToCompare = obj as Resource;
                return this.Name.Equals(resourceToCompare.Name);
            }
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }

        public string GetFullPath()
        {
            return $"{_directoryPath}/{_name}";
        }
    }
}

