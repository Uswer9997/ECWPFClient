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
      Console.Write("For exit press any key");
      Console.ReadLine();
    }

    private static void TestTEventAutoGenerator()
    {
      using (TEventAutoGenerator EvGenerator = new TEventAutoGenerator())
      {
        DateTime startTime = DateTime.Now;
        EvGenerator.MaxEventCount = 10;
        EvGenerator.StartGeneration();
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

    private static void PrintEvents(TEvent[] events)
    {
      foreach (TEvent ev in events)
      {
        Console.WriteLine($"date:{ev.EventDate}, id:{ev.EventId}, desc:{ev.Description}");
      }
    }


    #region EventService

    private static void TestTEventService()
    {
      using (TEventService EvService = new TEventService())
      {
        EvService.ProcessedEventTypes = new TEventType[] { TEventAutoGenerator.DefaultEventType };
        EvService.Events.CollectionChanged += EventsChanged;
      }
    }


    private static void EventsChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      var se = sender;
    }

    #endregion
  }
}
