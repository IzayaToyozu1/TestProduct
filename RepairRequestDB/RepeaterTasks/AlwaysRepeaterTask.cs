using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestDB.RepeaterTasks
{
    public class AlwaysRepeaterTask
    {
        private Action _action;
        private int _millisecond;
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _token;

        public event EventHandler StartMethodEvent;
        public event EventHandler EndMethodEvent;
        public event EventHandler<Exception> ErroredMethodEvent;
        public event EventHandler<OperationCanceledException> OperationCanceledEvent;


        public AlwaysRepeaterTask(Action action, int millisecond)
        {
            _action = action;
            _millisecond = millisecond;
        }

        public void Execute()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _token = _cancellationTokenSource.Token;
            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        StartMethodEvent?.Invoke(this, EventArgs.Empty);
                        _action?.Invoke();
                        if(_token.IsCancellationRequested)
                            _token.ThrowIfCancellationRequested();
                        EndMethodEvent?.Invoke(this, EventArgs.Empty);
                        Thread.Sleep(_millisecond);
                    }
                }
                catch (OperationCanceledException ex)
                {
                    _cancellationTokenSource.Dispose();
                    OperationCanceledEvent?.Invoke(this, ex);
                }
                catch (Exception ex)
                {
                    _cancellationTokenSource.Dispose();
                    ErroredMethodEvent?.Invoke(this, ex);
                }
            }, _cancellationTokenSource.Token);
        }

        public void Cancel()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
        }
    }
}
