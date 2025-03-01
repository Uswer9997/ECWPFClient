using ECWPFClient.Models.Orion_SOAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECWPFClient.Services
{
  /// <summary>
  /// Сервис генерирующий с определённой периодичностью события TEvent.
  /// Также сервис предоставляет события TEvent по запросу.
  /// </summary>
  internal class TEventService
  {
    /// <summary>
    /// Имитатор работы SOAP-сервиса
    /// </summary>
    private Infrastructure.TEventAutoGenerator eventGenerator;

    #region Constructor

    public TEventService()
    {
      eventGenerator = new Infrastructure.TEventAutoGenerator();
    }
    #endregion

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

  }
}
