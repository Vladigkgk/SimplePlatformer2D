using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PixelCrew.Utils.Disposables
{
    public class CompositDisposables : IDisposable
    {
        private readonly List<IDisposable> disposables = new List<IDisposable>();

        public void Retain(IDisposable disposable)
        {
            disposables.Add(disposable);
        }

        public void Dispose()
        {
            foreach(var disposable in disposables)
            {
                disposable.Dispose();
            }
            disposables.Clear();
        }
    }
}
