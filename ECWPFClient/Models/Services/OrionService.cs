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

    private TComputer[] computers;

    #region Constructor

    public OrionService()
    {
      computers = new TComputer[] { new TComputer() { Id = 1, Name = "This copm", Ip = "127.0.0.1" } };
    }
    #endregion

    public TEventType GetEventTypeById(int id)
    {
      return Infrastructure.TEventAutoGenerator.GenerateEventTypes.FirstOrDefault(t => t.Id == id);
    }

    public TComputer GetComputerById(int id)
    {
      return computers.FirstOrDefault(c => c.Id == id);
    }
  }
}
