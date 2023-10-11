using System;
using com.ruffgames.core.Scripts.Storage;
using UnityEngine;

namespace _game.Storage
{
    public abstract class IStorage : IDisposable
    {
        public StorageData StorageData { get; protected set; }
        
        public abstract void Save();
        public abstract void Load();
        public abstract void Clear();

        protected IStorage()
        {
            Debug.Log("Storage Initialized...");
            Load();
        }

        public void Dispose()
        {
            Debug.Log("Storage Disposed...");
            Save();
        }
    }
}
