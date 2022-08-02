using EventAggregator.Models;

namespace EventAggregator.Aggregation;

public static class EventAggregation
{
    static List<Event>? Events {get;}
    static Int32 unixTimestamp (){
       Int32 unixTimestamp = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        return unixTimestamp;
    }
    static EventAggregation()
    {
        Events = new List<Event>
        {
            //TEST (can delete)
            new Event{Name = "test",UnixTime =1659169308},
            new Event{Name = "test",UnixTime =1659169310},
        };
    }
    public static void AddEvent(string name)
    {
        Events?.Add(new Event{Name = name,UnixTime = unixTimestamp()});
        //TEST (can delete)
        Console.WriteLine(Events[3].Name);
    }
    
    public static List<string> AggregateEvents(string name, int UNIXfrom,int UNIXto,int interval)
    {
        int eventcount = 0;
        int lastInterval = UNIXfrom;
        int intervalCount = (UNIXto-UNIXfrom)/interval;
        if ((UNIXto-UNIXfrom)%interval>0){
            intervalCount++;
        }
        List<string> AggData = new List<string>();
        for (int i = 0;i<intervalCount;i++)
        {
            foreach (Event e in Events)
            {
                if(e.Name == name)
                {
                    if(e.UnixTime >UNIXfrom && e.UnixTime < UNIXto)
                    {
                        if(e.UnixTime - lastInterval <= interval && e.UnixTime - lastInterval >0)
                        {
                            eventcount++;
                        } 
                    }
                }
            }
            AggData.Add (name+": "+ eventcount.ToString());
            lastInterval += interval;
            eventcount = 0;
        }
        return AggData;
    }
}