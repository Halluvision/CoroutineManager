using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Halluvision.Coroutine
{
    public class CoroutineHandle
    {
        public IEnumerator coroutine;

        public bool IsRunning { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsFinished { get; private set; }

        public CoroutineHandle()
        {
            coroutine = null;
            IsRunning = false;
            IsPaused = false;
            IsFinished = true;
        }

        public CoroutineHandle(IEnumerator coroutine, bool autoStart = true)
        {
            this.coroutine = coroutine;
            IsRunning = false;
            IsPaused = false;
            IsFinished = false;

            if (autoStart)
                Start();
        }

        public void Start()
        {
            if (IsRunning)
                return;

            if (IsFinished)
            {
                Debug.LogError("This coroutine handle has been used and not valid yet, Create new one!");
                return;
            }

            IsRunning = true;
            CoroutineManager.Instance.StartCoroutine(this);
        }

        public void Stop()
        {
            IsRunning = false;
            IsFinished = true;
        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
        }

        public void Finished()
        {
            IsRunning = false;
            IsPaused = false;
            IsFinished = true;
        }
    }
}