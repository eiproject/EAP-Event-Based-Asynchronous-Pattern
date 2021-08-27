using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EAP_Event_Based_Asynchronous_Pattern {
  class InterruptAbort {
    private Thread _worker;
    private int i = 0;
    internal InterruptAbort() {
      while (true) {
        _worker = new Thread(Foo);
        _worker.Start();

        Console.ReadKey();
        _worker.Interrupt();
        
        Console.ReadKey();
      }
    }

    private void Foo() {
      try {
        while (true) {
          Console.WriteLine(i);
          i++;
          Thread.Sleep(1000);
        }
      }
      catch (ThreadInterruptedException e) {
        Console.WriteLine(e.Message);
        Console.WriteLine("Interupted...");
      }
      finally {
        Console.WriteLine("Yeayyy, done.");
        Reset();
      }
    }

    private void Reset() {
      i = 0;
    }
  }
}
