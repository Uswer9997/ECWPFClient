using ECWPFClient.Models.Data.Orion_SOAP;
using ECWPFClient.Infrastructure.Commands;
using ECWPFClient.Models.Services;
using ECWPFClient.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ECWPFClient.Models.Data.EC;
using ECWPFClient.Models;

namespace ECWPFClient.ViewModels
{
  internal class MainWindowViewModel : BaseViewModel, IDisposable
  {
    private string _Title = "EC Client";

    public string Title
    {
      get => _Title;
      set => Set(ref _Title, value);
    }

    /// <summary>
    /// События Орион
    /// </summary>
    public ObservableCollection<ECEvent> Events { get; }

    /// <summary>
    /// Выбранное событие
    /// </summary>
    public ECEvent SelectedEvent { get; set; }

    /// <summary>
    /// Сервис предоставляющий данные Ориона
    /// </summary>
    private OrionDataProvider OrionDataProvider { get; }

    #region Constructor

    public MainWindowViewModel()
    {
      OrionDataProvider = new OrionDataProvider();
      
      Events = OrionDataProvider.ECEvents;

      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecute, CanCloseApplicationCommandExecute);
    }
    #endregion

    #region Commands

    public ICommand CloseApplicationCommand { get; }

    private void OnCloseApplicationCommandExecute(object p)
    {
      Application.Current.Shutdown();
    }

    private bool CanCloseApplicationCommandExecute(object p) => true;

    #endregion

    #region Disposing
    // Flag: Has Dispose already been called?
    bool disposed = false;

    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
      Dispose(disposing: true);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
      if (disposed)
        return;

      if (disposing)
        OrionDataProvider?.Dispose();

      disposed = true;
    }
    #endregion
  }
}
