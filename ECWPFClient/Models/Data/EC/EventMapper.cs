namespace ECWPFClient.Models.Data.EC
{
  internal class EventMapper
  {
    public static ECWPFClient.Models.Data.EC.ECEvent Map(ECWPFClient.Models.Data.Orion_SOAP.TEvent parTEvent)
    {
      ECWPFClient.Models.Data.EC.ECEvent _ECEvent = new ECEvent(parTEvent.EventId)
      {
        EventDate = parTEvent.EventDate,
        Description = parTEvent.Description,
      };

      return _ECEvent;
    }
  }
}
