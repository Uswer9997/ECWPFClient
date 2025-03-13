using ECWPFClient.Models.Data.EC;
using ECWPFClient.Models.Data.Orion_SOAP;
using ECWPFClient.Models.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECWPFClient.Models
{
  /// <summary>
  /// Является центральным звеном, связывающим сервисы 
  /// и предоставляющий централизованный доступ к данным Ориона.
  /// Является связующим звеном между SOAP сервисом Ориона и ViewModel главного окна.
  /// </summary>
  /// <remarks>
  /// Помимо централизованного доступа внутри класса производится преобразование типов. 
  /// </remarks>
  internal class OrionDataProvider 
  {
    #region Fields

    /// <summary>
    /// Сервис предоставляющий события Ориона
    /// </summary>
    private TEventService eventService { get; }


    private TComputer[] computers;

    #endregion

    #region Properties
    /// <summary>
    /// События Ориона преобразованные для отображения
    /// </summary>
    public ObservableCollection<ECEvent> ECEvents { get; }


    #endregion

    #region Constructor

    public OrionDataProvider()
    {
      eventService = new TEventService(System.Threading.SynchronizationContext.Current);
      eventService.ProcessedEventTypes = Infrastructure.TEventAutoGenerator.GenerateEventTypes;
      eventService.Events.CollectionChanged += TEventsChangedHandler;

      computers = new TComputer[] { new TComputer() { Id = 1, Name = "This copm", Ip = "127.0.0.1" } };

      ECEvents = new ObservableCollection<ECEvent>();
    }
    #endregion

    /// <summary>
    /// Обработчик собитий изменения коллекции событий TEvent в сервисе
    /// </summary>
    /// <param name="sender">Коллекция событий</param>
    /// <param name="e">Аргумент события</param>
    private void TEventsChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
    {
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
          foreach (TEvent elTEvent in e.NewItems)
          {
            ECWPFClient.Models.Data.EC.ECEvent _ECEvent = new ECEvent(elTEvent.EventId)
            {
              EventDate = elTEvent.EventDate,
              Description = elTEvent.Description,
            };
            _ECEvent.EventType = GetEventTypeById(elTEvent.EventTypeId);
            _ECEvent.Computer = GetComputerById(elTEvent.ComputerId);
            /************************************** ВРЕМЕННО *************************************/
            _ECEvent.Section = "TestSection";
            /************************************** ВРЕМЕННО *************************************/
            ECEvents.Add(_ECEvent);
          }
          break;
        case NotifyCollectionChangedAction.Remove:
          ECEvents.RemoveAt(e.OldStartingIndex);
          break;
      }

    }
    #region Metods


    public TEventType GetEventTypeById(int id)
    {
      return Infrastructure.TEventAutoGenerator.GenerateEventTypes.FirstOrDefault(t => t.Id == id);
    }

    public TComputer GetComputerById(int id)
    {
      return computers.FirstOrDefault(c => c.Id == id);
    }
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
      {
        eventService?.Dispose();
      }

      disposed = true;
    }
    #endregion
  }
}
