using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace EAP_Event_Based_Asynchronous_Pattern {
  class BWCancelable {
    private BackgroundWorker _bw;
    private Random _rand;
    internal BWCancelable() {
      _bw = new BackgroundWorker() {
        WorkerReportsProgress = true,
        WorkerSupportsCancellation = true
      };
      _rand = new Random();
      _bw.DoWork += MainProcess;
      _bw.ProgressChanged += ProgressChanged;
      _bw.RunWorkerCompleted += OnComplete;

      _bw.RunWorkerAsync("Time to work!");

      Console.WriteLine("Press Enter to cancel");
      Console.ReadLine();
      if (_bw.IsBusy) _bw.CancelAsync();
      Console.ReadLine();
    }

    private void MainProcess(object sender, DoWorkEventArgs e) {
      int progressPercentage = 0;
      while (progressPercentage < 100) {
        if (_bw.CancellationPending) { e.Cancel = true; return; }
        progressPercentage += GetRandomNumber(1, 20);
        if (progressPercentage >= 100) {
          _bw.ReportProgress(100);
        }
        else {
          _bw.ReportProgress(progressPercentage);
        }
        Thread.Sleep(1000);
      }
      e.Result = 100;
    }

    private void ProgressChanged(object sender, ProgressChangedEventArgs e) {
      Console.WriteLine("Reached " + e.ProgressPercentage + "%");
    }

    private void OnComplete(object sender, RunWorkerCompletedEventArgs e) {
      if (e.Cancelled)
        Console.WriteLine("Canceled!");
      else if (e.Error != null)
        Console.WriteLine("Worker exception: " + e.Error.ToString());
      else
        Console.WriteLine("Complete: " + e.Result);      // from DoWork
    }

    private int GetRandomNumber(int min, int max) {
      return _rand.Next(min, max);
    }
  }
}
