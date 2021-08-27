using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EAP_Event_Based_Asynchronous_Pattern {
  class SignalingAndEventWait {
    static EventWaitHandle _waitHandle = new AutoResetEvent(false);
    internal SignalingAndEventWait() {
      new Thread(Waiter).Start();
      Thread.Sleep(2000);                  // Pause for a second...
      _waitHandle.Set();                    // Wake up the Waiter.
    }

    private void Waiter() {
      Console.Write("Waiting... ");
      _waitHandle.WaitOne();                // Wait for notification
      Console.WriteLine("Notified");
    }
  }
}
