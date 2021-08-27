using System;
using System.Collections.Generic;
using System.Text;

namespace EAP_Event_Based_Asynchronous_Pattern {
  class TheCanceler {
    private object _locker = new object();
    private bool _isCancelRequested;
    internal bool IsCancelRequested {
      get {
        lock (_locker) {
          return _isCancelRequested;
        }
      }
    }

    internal TheCanceler() { }

    internal void Cancel() {
      lock (_locker) {
        _isCancelRequested = true;
      }
    }

    internal void ThrowIfCancelRequested() {
      if (IsCancelRequested) throw new OperationCanceledException();
    }
  }
}
