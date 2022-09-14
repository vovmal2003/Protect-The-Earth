﻿using System.Collections.Generic;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Managers
{
    public class PauseManager : IPauseHandler
    {
        private readonly List<IPauseHandler> _handlers = new();

        public bool IsPaused { get; private set; }

        public void Register(IPauseHandler handler)
        {
            _handlers.Add(handler);
        }

        public void Unregister(IPauseHandler handler)
        {
            _handlers.Remove(handler);
        }

        public void SetPaused(bool isPaused)
        {
            IsPaused = isPaused;
            foreach (var handler in _handlers)
                handler.SetPaused(isPaused);
        }
    }
}