using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EAP_Event_Based_Asynchronous_Pattern {
  class SafeInterrupt {
    private TheCanceler _canceler;
    private Thread _thread;
    private Thread _thread2;
    internal SafeInterrupt() {
      _canceler = new TheCanceler();

      _thread = new Thread(Start);
      _thread.Start();
      _thread2 = new Thread(Start);
      _thread2.Start();

      Console.ReadKey();
      _canceler.Cancel();
    }

    private void Start() {
      try {
        MainProcess(_canceler);
      }
      catch (OperationCanceledException e){
        Console.WriteLine(e.Message);
      }
      finally {

      }
    }

    private void MainProcess(TheCanceler c) {
      while (true) {
        try {
          c.ThrowIfCancelRequested();
          SubProcess(c);
          Console.WriteLine(DateTime.Now);
          Thread.Sleep(1001);
        }
        finally { }
      }
    }

    private void SubProcess(TheCanceler c) {
      c.ThrowIfCancelRequested();
    }
  }
}
