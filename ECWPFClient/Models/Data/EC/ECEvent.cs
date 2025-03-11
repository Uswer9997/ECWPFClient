using System;
using System.Activities.Presentation.View.OutlineView;

namespace ECWPFClient.Models.Data.EC
{
  /// <summary>
  /// Преобразованное событие Ориона для отображения
  /// </summary>
  public  class ECEvent
    {
    [HidePropertyInOutlineViewAttribute]
    public string AssociatedEventId { get; set; }
    public int EventTypeId { get; set; }
    public DateTime EventDate { get; set; }
    public string Description { get; set; }
    public int ItemId { get; set; }
    public string ItemType { get; set; }
    public int SectionId { get; set; }

    public ECEvent() { }
  }
}
