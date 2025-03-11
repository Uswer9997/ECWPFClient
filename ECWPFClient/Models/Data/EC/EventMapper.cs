namespace ECWPFClient.Models.Data.EC
{
  internal class EventMapper
  {
    public static ECWPFClient.Models.Data.EC.ECEvent Map(ECWPFClient.Models.Data.Orion_SOAP.TEvent parTEvent)
    {
      ECWPFClient.Models.Data.EC.ECEvent _ECEvent = new ECEvent()
      {
        AssociatedEventId = parTEvent.EventId,
        EventDate = parTEvent.EventDate,
        Description = parTEvent.Description,
        EventTypeId = parTEvent.EventTypeId,
        ItemId = parTEvent.ItemId,
        ItemType = parTEvent.ItemType,
        SectionId = parTEvent.SectionId
      };

      return _ECEvent;
    }
  }
}
