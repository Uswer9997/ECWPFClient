using System;

namespace ECWPFClient.Models.Data.Orion_SOAP
{
  public class TOperationResult<T>
  {
    public bool Success { get; set; }
    public T Result { get; set; }
    public TServiceError Error {get; set;}
    }
}
