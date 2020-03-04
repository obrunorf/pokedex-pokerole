﻿using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Pokedex.Pokerole.Controls
{
    // https://stackoverflow.com/questions/15003827/async-implementation-of-ivalueconverter
    public sealed class TaskCompletionNotifier<TResult> : INotifyPropertyChanged where TResult : class
    {
        public TaskCompletionNotifier(Task<TResult> task)
        {
            Task = task;
            if (!task.IsCompleted)
            {
                var scheduler = (SynchronizationContext.Current == null) ? TaskScheduler.Current : TaskScheduler.FromCurrentSynchronizationContext();
                task.ContinueWith(t =>
                {
                    var propertyChanged = PropertyChanged;
                    if (propertyChanged != null)
                    {
                        propertyChanged(this, new PropertyChangedEventArgs("IsCompleted"));
                        if (t.IsCanceled)
                        {
                            propertyChanged(this, new PropertyChangedEventArgs("IsCanceled"));
                        }
                        else if (t.IsFaulted)
                        {
                            propertyChanged(this, new PropertyChangedEventArgs("IsFaulted"));
                            propertyChanged(this, new PropertyChangedEventArgs("ErrorMessage"));
                        }
                        else
                        {
                            propertyChanged(this, new PropertyChangedEventArgs("IsSuccessfullyCompleted"));
                            propertyChanged(this, new PropertyChangedEventArgs("Result"));
                        }
                    }
                },
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                scheduler);
            }
        }

        // Gets the task being watched. This property never changes and is never <c>null</c>.
        public Task<TResult> Task { get; private set; }

        // Gets the result of the task. Returns the default value of TResult if the task has not completed successfully.
        public TResult Result
        {
            get
            {
                if (Task.Status == TaskStatus.RanToCompletion)
                    return Task.Result;
                else
                {
                    if (typeof(TResult) == typeof(Uri))
                    {
                        return new Uri("about:blank") as TResult;
                    }
                    
                    return default;
                }
            }
        }

        // Gets whether the task has completed.
        public bool IsCompleted { get { return Task.IsCompleted; } }

        // Gets whether the task has completed successfully.
        public bool IsSuccessfullyCompleted { get { return Task.Status == TaskStatus.RanToCompletion; } }

        // Gets whether the task has been canceled.
        public bool IsCanceled { get { return Task.IsCanceled; } }

        // Gets whether the task has faulted.
        public bool IsFaulted { get { return Task.IsFaulted; } }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
