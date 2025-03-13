using ECWPFClient.Models.Data.Orion_SOAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECWPFClient.Models.Services
{
  /// <summary>
  /// Является связующим звеном между SOAP сервисом Ориона и ViewModel главного окна
  /// </summary>
 public class OrionService
  {



    public TEventType GetEventTypeById(int id)
    {
      return Infrastructure.TEventAutoGenerator.GenerateEventTypes.First(t => t.Id == id);
    }
  }
}
