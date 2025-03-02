using ECWPFClient.Models.Orion_SOAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECWPFClient.Infrastructure
{
  /// <summary>
  /// Класс заглушка вместо реального SOAP-сервиса.
  /// Позволяет имитировать возникновение событий Ориона,
  /// то есть генерирует с определённой периодичностью события TEvent.
  /// </summary>
  public class TEventAutoGenerator : IDisposable
  {
    /// <summary>
    /// Список событий
    /// </summary>
    private List<TEvent> events;

    /// <summary>
    /// Интервал генерации событий в мсек
    /// </summary>
    public int GenerateInterval { get; set; }

    /// <summary>
    /// Максимальное количество событий
    /// </summary>
    public int MaxEventCount { get; set; }

    public TEventType GenerateEventType { get; set; }

    /// <summary>
    /// Таймер автогенерации событий
    /// </summary>
    private System.Timers.Timer generateTimer;


    #region Constructor

    public TEventAutoGenerator()
    {
      GenerateInterval = 1000;
      MaxEventCount = 100;
      events = new List<TEvent>();
      GenerateEventType = new TEventType()
      {
        Id = 1,
        Category = "TestCategory",
        CharId = "",
        Comments = "TestComments",
        Description = "TestDescription",
        HexCode = "",
        IsAlarm = true
      };
      generateTimer = new System.Timers.Timer();
      generateTimer.Interval = GenerateInterval;
      generateTimer.Elapsed += OnTimedEvent;
    }
    #endregion



    private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
    {
      if (events.Count >= MaxEventCount)
      {
        events.RemoveAt(0);
      }

      Guid guid = Guid.NewGuid();

      TEvent newTEvent = new TEvent()
      {
        EventId = guid.ToString(),
        Description = "TestEvent",
        EventDate = DateTime.Now,
        EventTypeId = GenerateEventType.Id,
        ItemId = events.Count + 1,
        ItemType = "LOOP",
        SectionId = 1
      };

      events.Add(newTEvent);
    }

    public void StartGeneration()
    {
      generateTimer.Start();
    }

    public void StopGeneration()
    {
      generateTimer.Stop();
    }

    /// <summary>
    /// Возвращает события согласно переданному фильтру
    /// </summary>
    /// <returns></returns>
    public TOperationResult<TEvent[]> GetEvents(DateTime beginTime,
                                                DateTime endTime,
                                                TEventType[] eventTypes,
                                                int offset,
                                                int count)
    {
      TEvent[] resultEvetns = new TEvent[] { };
      int eventCount = 0;

      foreach (TEvent elTEvent in events)
      {
        if (eventTypes.First(te => (te.Id == elTEvent.EventTypeId)) != null)
        {
          if ((elTEvent.EventDate > beginTime) & (elTEvent.EventDate < endTime))
          {
            Array.Resize(ref resultEvetns, eventCount + 1);
            resultEvetns[eventCount] = elTEvent;
            eventCount++;
          }
        }
      }

      TOperationResult<TEvent[]> tOResult = new TOperationResult<TEvent[]>();
      tOResult.Result = resultEvetns;
      tOResult.Success = true;
      tOResult.Error = new TServiceError() { Description = "", ErrorCode = "", InnerExceptionMessage = "" };

      return tOResult;
    }

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
        if (generateTimer.Enabled)
        {
          generateTimer.Stop();
          generateTimer.Elapsed -= OnTimedEvent;
        }
      }

      disposed = true;
    }
    #endregion
  }
}
