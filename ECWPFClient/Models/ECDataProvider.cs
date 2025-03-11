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
  /// Класс являющийся центральным звеном, связывающим сервисы 
  /// и предоставляющий централизованный доступ к данным Ориона.
  /// </summary>
  /// <remarks>Помимо централизованного доступа внутри класса производится преобразование типов.</remarks>
 internal class ECDataProvider 
  {
    #region Fields

    /// <summary>
    /// Сервис предоставляющий события Ориона
    /// </summary>
    private TEventService eventService { get; }

    #endregion

    #region Properties
    /// <summary>
    /// События Ориона преобразованные для отображения
    /// </summary>
    public ObservableCollection<ECEvent> ECEvents { get; }


    #endregion

    #region Constructor

    public ECDataProvider()
    {
      eventService = new TEventService(System.Threading.SynchronizationContext.Current);
      eventService.ProcessedEventTypes = new TEventType[] { Infrastructure.TEventAutoGenerator.DefaultEventType };
      eventService.Events.CollectionChanged += TEventsChangedHandler;

      ECEvents = new ObservableCollection<ECEvent>();
    }

    private void TEventsChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
    {
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
          foreach (TEvent elTEvent in e.NewItems)
            ECEvents.Add(EventMapper.Map(elTEvent));
          break;
        case NotifyCollectionChangedAction.Remove:
          ECEvents.RemoveAt(e.OldStartingIndex);
          break;
        //case NotifyCollectionChangedAction.Move:
        //  foreach (TEvent elTevent in e.OldItems)
        //  {

        //  }
      }

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
