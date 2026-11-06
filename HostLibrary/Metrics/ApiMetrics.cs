using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace HostLibrary.Metrics
{
    public class ApiMetrics
    {
        public const string MeterName = "SomeApp.Api";

        private Counter<long> forecastsGiven;

        public ApiMetrics(IMeterFactory meterFactory)
        {
            var meter = meterFactory.Create(MeterName);

            forecastsGiven = meter.CreateCounter<long>(
                "forecasts.requests",
                unit: "{request}",
                description: "Number of times the weather forecasts endpoint has been hit");
        }

        public void ForecastsHit(string method) =>
            forecastsGiven.Add(1, new KeyValuePair<string, object?>("http.method", method));


    }
}
