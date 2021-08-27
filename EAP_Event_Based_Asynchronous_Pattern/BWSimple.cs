using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EAP_Event_Based_Asynchronous_Pattern {
  class BWSimple {
    private BackgroundWorker _bw;
    internal BWSimple() {
      _bw = new BackgroundWorker();
      _bw.DoWork += Foo;
      _bw.RunWorkerAsync("Heyyyaa");
      Console.ReadKey();
    }
    private void Foo(object sender, DoWorkEventArgs e) {
      Console.WriteLine(e.Argument);
    }
  }
}
