using ECWPFClient.Data.Orion_SOAP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


    private int _checkInterval;

    /// <summary>
    /// Интервал проверки новых событий Ориона
    /// </summary>
    public int CheckInterval
    {
      get => this._checkInterval;
      set
      {
        _checkInterval = value;
        checkTimer.Interval = _checkInterval;
      }
    }

    /// <summary>
    /// Типы события Орион, которые необходимо отслеживать
    /// </summary>
    public TEventType[] ProcessedEventTypes { get; set; }

    /// <summary>
    /// События Орион отслеживаемые сервисом
    /// </summary>
    public ObservableCollection<TEvent> Events { get; }

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
      checkTimer.Elapsed += OnCheckEvents;
      checkTimer.Start();
    }
    #endregion

    /// <summary>
    /// Проверяет наличие новых событий в Орионе
    /// </summary>
    protected void OnCheckEvents(object source, System.Timers.ElapsedEventArgs e)
    {
      // Останавливаем таймер на время выполнения запроса
      checkTimer.Stop();

      // текущее время для запроса событий
      DateTime currectTime = DateTime.Now;

      TOperationResult<TEvent[]> newEvents = GetEvents(beginTime: previousTime,
                                                       endTime: currectTime,
                                                       eventTypes: ProcessedEventTypes,
                                                       offset: 0,
                                                       count: int.MaxValue);
      Events.Add()
      // здесь обработка полученного результата и выброс события о поступлении новых данных

      previousTime = currectTime;
      // Вновь запускаем таймер
      checkTimer.Start();
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
          checkTimer.Elapsed -= OnCheckEvents;
          checkTimer.Dispose();
        }
        eventGenerator?.Dispose();
      }

      disposed = true;
    }
    #endregion
  }
}
