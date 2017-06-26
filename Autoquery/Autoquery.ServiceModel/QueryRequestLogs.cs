using System;
using ServiceStack;

namespace Autoquery.ServiceModel
{
    [Route("/query/requestlogs")]
    [Route("/query/requestlogs/{Date}")]
    public class QueryRequestLogs : QueryData<RequestLogEntry>
    {
        public DateTime? Date { get; set; }
        public bool ViewErrors { get; set; }
    }
    [Route("/logs/today")]
    public class TodayLogs : QueryData<RequestLogEntry> { }
    //[Route("/logs/today/errors")]
    //public class TodayErrorLogs : QueryData<RequestLogEntry> { }

    //[Route("/logs/yesterday")]
    //public class YesterdayLogs : QueryData<RequestLogEntry> { }
    //[Route("/logs/yesterday/errors")]
    //public class YesterdayErrorLogs : QueryData<RequestLogEntry> { }
}