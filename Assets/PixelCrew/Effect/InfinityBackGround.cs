using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Effect
{
    public class InfinityBackGround : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _container;

        private Bounds _countainerBounds;
        private Bounds _allBounds;

        private Vector3 _boundsToTranformDelta;
        private Vector3 _containerDelta;

        private Vector3 _screenSize;

        private void Start()
        {
            var sprites = _container.GetComponentsInChildren<SpriteRenderer>();
            _countainerBounds = sprites[0].bounds;
            foreach (var sprite in sprites)
            {
                _countainerBounds.Encapsulate(sprite.bounds);
            }
            _allBounds = _countainerBounds;

            _boundsToTranformDelta = transform.position - _allBounds.center;
            _containerDelta = _container.position - _allBounds.center;
        }

        private void LateUpdate()
        {
            var min = _camera.ViewportToWorldPoint(Vector3.zero);
            var max = _camera.ViewportToWorldPoint(Vector3.one);

            _screenSize = new Vector3(max.x - min.x, max.y - min.y);

            _allBounds.center = transform.position - _boundsToTranformDelta;
            var camPosition = _camera.transform.position.x;

            var screenLeft = new Vector3(camPosition - _screenSize.x / 2, _countainerBounds.center.y);
            var screenRight = new Vector3(camPosition + _screenSize.x / 2, _countainerBounds.center.y);

            if (!_allBounds.Contains(screenLeft))
            {
                InstantiateContainer(_allBounds.min.x - _countainerBounds.extents.x);
            }

            if (!_allBounds.Contains(screenRight))
            {
                InstantiateContainer(_allBounds.max.x + _countainerBounds.extents.x);
            }




        }

        private void InstantiateContainer(float boundCenterX)
        {
            var newBounds = new Bounds(new Vector3(boundCenterX, _countainerBounds.center.y), _countainerBounds.size);
            _allBounds.Encapsulate(newBounds);

            _boundsToTranformDelta = transform.position - _allBounds.center;
            var newContainerXPos = boundCenterX - _containerDelta.x;

            var newPosition = new Vector3(newContainerXPos, transform.position.y);

            Instantiate(_container, newPosition, Quaternion.identity, transform);
        }

        private void OnDrawGizmosSelected()
        {
            DrawGizmos.DrawBounds(_allBounds, Color.magenta);
        }
    }
}
