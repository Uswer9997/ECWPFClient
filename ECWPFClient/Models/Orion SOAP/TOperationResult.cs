using System;

namespace ECWPFClient.Models.Orion_SOAP
{
  public class TOperationResult<T>
  {
    public bool Success { get; set; }
    public T Result { get; }
    public TServiceError Error {get; set;}
    }
}
