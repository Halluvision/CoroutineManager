using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Halluvision.Coroutine
{
    public class CoroutineManager : MonoBehaviour
    {
        public static CoroutineManager Instance;

        List<CoroutineHandle> coroutineHandles;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                coroutineHandles = new List<CoroutineHandle>();
            }
            else
            {
                Destroy(this.gameObject);
                Debug.LogWarning("Multiple instances of CoroutineManager in the scene.");
            }

            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
            GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        }

        IEnumerator CoroutineCaller(CoroutineHandle coroutineHandle)
        {
            while (coroutineHandle.IsRunning)
            {
                if (coroutineHandle.IsPaused)
                    yield return null;
                else
                {
                    if (coroutineHandle.coroutine != null && coroutineHandle.coroutine.MoveNext())
                    {
                        yield return coroutineHandle.coroutine.Current;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            coroutineHandle.Finished();
            if (coroutineHandles.Contains(coroutineHandle))
                coroutineHandles.Remove(coroutineHandle);
        }

        public void StartCoroutine(CoroutineHandle coroutineHandle)
        {
            StartCoroutine(CoroutineCaller(coroutineHandle));
            coroutineHandles.Add(coroutineHandle);
        }

        private void OnGameStateChanged(GameState newGameState)
        {
            if (newGameState == GameState.Pause)
            {
                foreach (var coroutine in coroutineHandles)
                {
                    coroutine.Pause();
                }
            }
            else
            {
                foreach (var coroutine in coroutineHandles)
                {
                    if (coroutine.IsPaused)
                        coroutine.Resume();
                }
            }
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
        }
    }
}