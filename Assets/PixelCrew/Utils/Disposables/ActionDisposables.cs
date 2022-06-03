using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PixelCrew.Utils.Disposables
{
    public class ActionDisposables : IDisposable
    {
        private Action _onDispose;

        public ActionDisposables(Action onDipose)
        {
            _onDispose = onDipose;
        }

        public void Dispose()
        {
            _onDispose?.Invoke();
            _onDispose = null;
        }
    }
}
