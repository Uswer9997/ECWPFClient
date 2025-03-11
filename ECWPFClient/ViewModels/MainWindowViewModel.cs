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

    /// <summary>
    /// События Орион
    /// </summary>
    public ObservableCollection<TEvent> Events { get; }

    /// <summary>
    /// Выбранное событие
    /// </summary>
    public TEvent SelectedEvent { get; set; }

    /// <summary>
    /// Сервис предоставляющий события Ориона
    /// </summary>
    private TEventService eventService { get; }

    #region Constructor

    public MainWindowViewModel()
    {
      eventService = new TEventService(System.Threading.SynchronizationContext.Current);
      eventService.ProcessedEventTypes = new TEventType[] { Infrastructure.TEventAutoGenerator.DefaultEventType };
      Events = eventService.Events;

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
  }
}
