﻿using System;
using System.Windows.Input;

namespace ECWPFClient.Infrastructure.Commands.Base
{
  internal abstract class BaseCommand : ICommand
  {
    public event EventHandler CanExecuteChanged
    {
      add => CommandManager.RequerySuggested += value;
      remove => CommandManager.RequerySuggested -= value;
    }

    public abstract bool CanExecute(object parameter);

    public abstract void Execute(object parameter);
  }
}
