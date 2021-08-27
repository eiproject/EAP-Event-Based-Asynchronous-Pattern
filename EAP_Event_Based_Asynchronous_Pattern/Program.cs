using System;

namespace EAP_Event_Based_Asynchronous_Pattern {
  class Program {
    static void Main(string[] args) {
      Console.WriteLine("EAP!");
      // BWSimple bw = new BWSimple();
      // BWCancelable bwc = new BWCancelable();
      // InterruptAbort ia = new InterruptAbort();
      // SafeInterrupt si = new SafeInterrupt();
      // SignalingAndEventWait se = new SignalingAndEventWait();
      TwoWaySignaling tws = new TwoWaySignaling();
    }
  }
}
