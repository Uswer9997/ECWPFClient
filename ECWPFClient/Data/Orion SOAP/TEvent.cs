﻿using System;

namespace ECWPFClient.Data.Orion_SOAP
{
  public class TEvent
  {
    public string EventId { get; set; }
    public int EventTypeId { get; set; }
    public DateTime EventDate { get; set; }
    public string Description { get; set; }
    public int ItemId { get; set; }
    public string ItemType { get; set; }
    public int SectionId { get; set; }

    public TEvent() { }
  }
}
