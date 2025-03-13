using System;
using System.Activities.Presentation.View.OutlineView;

namespace ECWPFClient.Models.Data.EC
{
  /// <summary>
  /// Преобразованное событие Ориона для отображения
  /// </summary>
  public class ECEvent
  {
    [HidePropertyInOutlineView]
    public string AssociatedEventId { get; private set; }
    public DateTime EventDate { get; set; }
    public Orion_SOAP.TEventType EventType { get; set; } //Тип события
    public string Description { get; set; }
    public Orion_SOAP.TComputer Computer { get; set; } // Рабочее место
    public string Section { get; set; } // Раздел

    public ECEvent(string associatedEventId)
    {
      this.AssociatedEventId = associatedEventId;
    }
  }
}
