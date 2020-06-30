﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vocup.IO
{
    internal struct HopToThreadPoolAwaitable : INotifyCompletion
    {
        public HopToThreadPoolAwaitable GetAwaiter() => this;
        public bool IsCompleted => false;
        public void OnCompleted(Action continuation) { Task.Run(continuation); }
        public void GetResult() { }
    }
}
