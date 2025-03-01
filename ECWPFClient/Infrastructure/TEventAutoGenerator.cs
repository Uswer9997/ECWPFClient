using ECWPFClient.Models.Orion_SOAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECWPFClient.Infrastructure
{
   public class TEventAutoGenerator
    {
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

    }
  }
}
