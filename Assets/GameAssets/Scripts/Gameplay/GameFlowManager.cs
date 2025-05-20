using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SlidingSweetness
{
    public class GameFlowManager : MonoBehaviour
    {

        private CancellationTokenSource gameSceneCTS;
        void InitCancelToken()
        {
            gameSceneCTS?.Cancel();
            gameSceneCTS = new CancellationTokenSource();
        }



        private void Awake()
        {
            InitCancelToken();
            Initialize(gameSceneCTS.Token).Forget();
        }

        async UniTask Initialize(CancellationToken token)
        {
            //await init db + so container
            await BoardController.Instance.InitializeNew(token);
            await DragDropController.Instance.Initialize(token);
            token.ThrowIfCancellationRequested();

        }

        private void OnDestroy()
        {
            gameSceneCTS?.Cancel();
            gameSceneCTS?.Dispose();
            gameSceneCTS = null;
        }

    }
}