using EventAggregator.Models;
using EventAggregator.Aggregation;
using Microsoft.AspNetCore.Mvc;

namespace EventAggregator.Controllers;

[ApiController]
[Route("[controller]")]
public class EventAggregationController : ControllerBase
{
    //POST create an "event" 
    [HttpPost("{eventName}")]
    public IActionResult AddEvent([FromRoute]string eventName)
    {
        EventAggregation.AddEvent(eventName);
        return NoContent();
    }
    //GET return aggregated data of given "event" in certain period of time
    [HttpGet("/{name}from={UNIXfrom}&to={UNIXto}&interval={interval}")]
    public ActionResult<List<string>> Get(string name,int UNIXfrom,int UNIXto,int interval)
    {
        List<string> AggregatedEvents = EventAggregation.AggregateEvents(name,UNIXfrom,UNIXto,interval);
        if(AggregatedEvents == null){
            return NotFound();
        }
        return AggregatedEvents;
    }
}