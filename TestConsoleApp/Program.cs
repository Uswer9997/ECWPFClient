using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECWPFClient.Data.Orion_SOAP;
using ECWPFClient.Infrastructure;
using ECWPFClient.Services;

namespace TestConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("1 - Test TEventAutoGenerator");
      Console.WriteLine("2 - Test TEventService");
      Console.Write("Enter test code:");
      switch (Console.ReadLine())
      {
        case "1":
          TestTEventAutoGenerator();
          break;
        case "2":
          TestTEventService();
          break;
      }
      Console.Write("For exit press Enter");
      Console.ReadLine();
    }

    #region TEventAutoGenerator

    private static void TestTEventAutoGenerator()
    {
      using (TEventAutoGenerator EvGenerator = new TEventAutoGenerator())
      {
        DateTime startTime = DateTime.Now;
        EvGenerator.MaxEventCount = 10;
        EvGenerator.StartGeneration();
        Console.WriteLine("Please wait ...");
        System.Threading.Thread.Sleep(11000);
        EvGenerator.StopGeneration();
        DateTime endTime = DateTime.Now;
        TEventType[] eventTypes = new TEventType[] { TEventAutoGenerator.DefaultEventType };
        TOperationResult<TEvent[]> eventsRequest = EvGenerator.GetEvents(startTime, endTime, eventTypes, 0, 0);

        if (eventsRequest.Success)
        {
          PrintEvents(eventsRequest.Result);
        }
      }
    }
    #endregion

    private static void PrintEvents(TEvent[] events)
    {
      int n = 0;
      foreach (TEvent ev in events)
      {
        n++;
        Console.WriteLine($"№:{n}, date:{ev.EventDate}, id:{ev.EventId}");
      }
    }


    #region EventService

    private static void TestTEventService()
    {
      using (TEventService EvService = new TEventService(System.Threading.SynchronizationContext.Current))
      {
        EvService.ProcessedEventTypes = new TEventType[] { TEventAutoGenerator.DefaultEventType };
        //EvService.Events.CollectionChanged += EventsChanged;
        Console.WriteLine("Please wait ...");
        System.Threading.Thread.Sleep(12000);
        PrintEvents(EvService.Events.ToArray());
      }
    }


    private static void EventsChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      var se = sender;
    }

    #endregion
  }
}
