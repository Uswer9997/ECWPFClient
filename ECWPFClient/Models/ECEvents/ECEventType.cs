using System;

namespace ECWPFClient.Models.ECEvents
{
  public  class ECEventType
    {
    public int Id { get; set; }
    public string CharId { get; set; }
    public string Description { get; set; }
    public string Comments { get; set; }
    public string Category { get; set; }
    public string HexCode { get; set; }
    public bool IsAlarm { get; set; }
  }
}
