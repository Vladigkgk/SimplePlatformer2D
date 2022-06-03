using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Utils.Disposables
{
    public static class UnityEventExtentions
    {
        public static IDisposable Subacribe(this UnityEvent unityEvent, UnityAction call)
        {
            unityEvent.AddListener(call);
            return new ActionDisposables(() => unityEvent.RemoveListener(call));
        }

        public static IDisposable Subacribe<TType>(this UnityEvent<TType> unityEvent, UnityAction<TType> call)
        {
            unityEvent.AddListener(call);
            return new ActionDisposables(() => unityEvent.RemoveListener(call));
        }
    }
}
