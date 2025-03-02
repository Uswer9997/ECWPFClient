using ECWPFClient.Models.Orion_SOAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECWPFClient.Services 
{
  /// <summary>
  /// Сервис проверяющий с определённой периодичностью появление событий Ориона (TEvent).
  /// Также сервис предоставляет события TEvent по запросу.
  /// </summary>
  internal class TEventService : IDisposable
  {
    /// <summary>
    /// Имитатор работы SOAP-сервиса
    /// </summary>
    private Infrastructure.TEventAutoGenerator eventGenerator;

    /// <summary>
    /// Интервал проверки новых событий Ориона
    /// </summary>
    public int CheckInterval
    {
      get => _checkInterval;
      set
      {
        _checkInterval = value;
        checkTimer.Interval = _checkInterval;
      }
    }

    public ProcessedEventTypes {get;set;}
    /// <summary>
    /// Таймер проверки новых событий
    /// </summary>
    private System.Timers.Timer checkTimer;

    /// <summary>
    /// Время предыдущего запроса событий
    /// </summary>
    private DateTime previousTime;


    #region Constructor

    public TEventService()
    {
      // временное решение
      eventGenerator = new Infrastructure.TEventAutoGenerator();

      previousTime = DateTime.Now;
      checkTimer = new System.Timers.Timer();
      CheckInterval = 10000; // default value
      checkTimer.Elapsed += OnCheckEvetns;
      checkTimer.Start();
    }
    #endregion

    /// <summary>
    /// Проверяет наличие новых событий в Орионе
    /// </summary>
    protected void OnCheckEvetns(Object source, System.Timers.ElapsedEventArgs e)
    {
      // текущее время для запроса событий
      DateTime currectTime = DateTime.Now;

      GetEvents(beginTime:previousTime, endTime:currectTime,)
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
      return eventGenerator.GetEvents(beginTime, endTime, eventTypes, offset, count);
    }


    #region Disposing
    // Flag: Has Dispose already been called?
    bool disposed = false;
    private int _checkInterval;

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
        if (checkTimer.Enabled)
        {
          checkTimer.Stop();
          checkTimer.Elapsed -= OnCheckEvetns;
          checkTimer.Dispose();
        }
        eventGenerator?.Dispose();
      }

      disposed = true;
    }
    #endregion
  }
}
