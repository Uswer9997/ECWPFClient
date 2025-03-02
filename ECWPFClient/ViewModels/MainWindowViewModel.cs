using ECWPFClient.Infrastructure.Commands;
using ECWPFClient.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ECWPFClient.ViewModels
{
  internal class MainWindowViewModel : BaseViewModel
  {
    private string _Title = "EC Client";

    public string Title
    {
      get => _Title;
      set => Set(ref _Title, value);
    }

    #region Commands

    public ICommand CloseApplicationCommand { get; }

    private void OnCloseApplicationCommandExecute(object p)
    {
      Application.Current.Shutdown();
    }

    private bool CanCloseApplicationCommandExecute(object p) => true;

    #endregion

    #region Constructor

    public MainWindowViewModel()
    {
      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecute, CanCloseApplicationCommandExecute);
    }
    #endregion
  }
}
