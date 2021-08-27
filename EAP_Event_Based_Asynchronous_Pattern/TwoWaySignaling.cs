using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EAP_Event_Based_Asynchronous_Pattern {
  class TwoWaySignaling {
    EventWaitHandle _ready = new AutoResetEvent(false);
    EventWaitHandle _go = new AutoResetEvent(false);
    readonly object _locker = new object();
    string _message;

    internal TwoWaySignaling() {
      new Thread(Work).Start();

      _ready.WaitOne();                  // First wait until worker is ready
      lock (_locker) _message = "ooo";
      _go.Set();                         // Tell worker to go

      _ready.WaitOne();
      lock (_locker) _message = "ahhh";  // Give the worker another message
      _go.Set();
      _ready.WaitOne();
      lock (_locker) _message = null;    // Signal the worker to exit
      _go.Set();
    }

    private void Work() {
      while (true) {
        _ready.Set();                          // Indicate that we're ready
        _go.WaitOne();                         // Wait to be kicked off...
        lock (_locker) {
          if (_message == null) return;        // Gracefully exit
          Console.WriteLine(_message);
        }
      }
    }
  }
}
