using System;
using Sources.InfrastructureInterfaces.Listeners;
using Sources.Presentation.Views.Cameras;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Handlers.Pointers
{
    public class GameplayInteractPointerHandler : IPointerHandler
    {
        private readonly GameplayCamera _gameplayCamera;
        private readonly Action<Vector2Int> _onClickAction;
        private readonly Tilemap _tilemap;

        public GameplayInteractPointerHandler(
            GameplayCamera gameplayCamera,
            Action<Vector2Int> onClickAction
        )
        {
            _gameplayCamera = gameplayCamera;
            _onClickAction = onClickAction;
            _tilemap = Object.FindObjectOfType<Tilemap>();
        }

        public void OnTouchStart(Vector3 position, bool isPointerOverUI)
        {
            Ray ray = _gameplayCamera.Camera.ScreenPointToRay(position);
            int layer = 1 << LayerMask.NameToLayer("GameplayGrid");

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer) == false)
                return;

            Vector3Int gridPosition = _tilemap.WorldToCell(hit.point);

            if (_tilemap.HasTile(gridPosition) == false)
                return;

            _onClickAction.Invoke(new Vector2Int(gridPosition.x, gridPosition.y));
        }

        public void OnTouchMove(Vector3 position, bool isPointerOverUI)
        {
        }

        public void OnTouchEnd(Vector3 position, bool isPointerOverUI)
        {
        }
    }
}