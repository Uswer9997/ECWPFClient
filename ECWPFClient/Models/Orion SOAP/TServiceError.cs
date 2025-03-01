using System;

namespace ECWPFClient.Models.Orion_SOAP
{
  public class TServiceError
  {
    public string ErrorCode { get; set; }
    public string Description { get; set; }
    public string InnerExceptionMessage { get; set; }
  }
}
