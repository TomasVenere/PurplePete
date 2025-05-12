using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PurplePete.Services
{
    public class ConfluenceService
    {
        private const string RawMassTransitHelpResponse = """
Debugging RabbitMQ and MassTransit configurations can be a daunting task, especially when messages are mysteriously lost or queues seem misconfigured. This guide will walk you through a structured approach to identify and resolve common MassTransit and RabbitMQ issues. Whether you are a seasoned developer or just starting, this guide will help you quickly diagnose and fix your message delivery problems.

1. Understanding Common RabbitMQ Issues with MassTransit:
Incorrect Binding (Exchange-Queue Binding):
The queue may not be properly bound to the exchange, or the routing key might be incorrect.
✅ Solution:
Verify the exchange name in the producer matches the exchange the queue is bound to in RabbitMQ.
Ensure the routing key in the producer matches the queue’s binding key.

Incorrect Exchange Type:
The exchange type (direct, topic, fanout, headers) might not match your routing strategy.
✅ Solution:
Confirm that the exchange type in the producer matches your use case (e.g., direct for direct routing).

Queue Not Created or Declared Properly:
The consumer may not be creating or declaring the queue correctly.
✅ Solution:
Use the RabbitMQ Management Console to verify the queue exists.

Misconfigured Connection or Credentials:
The producer and consumer may be using different credentials or servers.
✅ Solution:
Double-check that both are pointing to the same RabbitMQ server with the correct credentials.

Incorrect Virtual Host (vHost):
Different vHosts will isolate queues and exchanges.
✅ Solution:
Ensure the producer and consumer are using the same vHost.

Consumer Misconfiguration:
The consumer may not be properly subscribing to the queue.
✅ Solution:
Make sure the consumer is correctly subscribing to the intended queue.
Avoid using auto-delete or exclusive queues unless necessary.

Message TTL or Expiration:
A Time-To-Live (TTL) setting might cause messages to expire before consumption.
✅ Solution:
Check TTL settings for the queue or messages.

2. Step-by-Step Debugging Process:
Step 1: Verify RabbitMQ Configuration:
Access the RabbitMQ Management Console (http://localhost:15672).
Confirm your exchange and queue setup:
Are they correctly named?
Is the routing key accurate?
Ensure the exchange type aligns with your routing strategy.

Step 2: Check Producer Settings (MassTransit):
Confirm the producer is using the correct exchange and routing key.
await bus.Publish(new YourMessage { Text = "Hello" });
Ensure the exchange and queue names are consistent.

Step 3: Check Consumer Settings (MassTransit):
Verify the consumer is subscribing to the correct queue.
cfg.ReceiveEndpoint("your_queue_name", e =>
{
    e.Consumer<YourConsumer>();
});
Ensure the consumer is registered properly.

Step 4: Test with a Direct Queue (Basic Setup):
Set up a basic direct exchange and queue manually in RabbitMQ.
Test message publishing and consuming without MassTransit for simplicity.

Step 5: Review Logs and Errors:
Check RabbitMQ logs for routing or connection issues.
Review MassTransit logs for subscription or consumer errors.

3. Advanced Debugging with MassTransit Logging:
Enable detailed logging using Serilog or another logging provider.
For even more insights, use Diagnostic Source Logging:
using System.Diagnostics;
var listener = new DiagnosticListener("MassTransit");
listener.Subscribe(new MassTransitDiagnosticListener());
Monitor all MassTransit events, including message publication and consumption.

4. Enhancing Debugging with Custom Middleware:
Add a custom middleware filter to log message details before they are consumed:
public class MessageLoggingFilter<T> : IFilter<ConsumeContext<T>> where T : class
{
    private readonly ILogger<MessageLoggingFilter<T>> _logger;
    public MessageLoggingFilter(ILogger<MessageLoggingFilter<T>> logger)
    {
        _logger = logger;
    }
    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        _logger.LogInformation($"Message Received: {context.Message}");
        await next.Send(context);
    }
    public void Probe(ProbeContext context)
    {
        context.CreateFilterScope("MessageLoggingFilter");
    }
}
This allows you to see every message passing through the queue, making it easier to diagnose issues.

5. Quick Troubleshooting Checklist
✅ Verify Exchange and Queue Names.
✅ Check Exchange Type (Direct, Topic, Fanout).
✅ Confirm Routing Key Matches.
✅ Ensure Queue Exists in RabbitMQ.
✅ Check Consumer Configuration (Subscriptions).
✅ Review Connection Settings (vHost, Credentials).
✅ Inspect Logs for Errors.
✅ Test with Direct Queue Setup.

6. Conclusion
Debugging RabbitMQ and MassTransit issues can be challenging, but with a clear process and the right tools, you can quickly identify and fix problems. This guide should give you the confidence to efficiently troubleshoot your message delivery issues.
""";

        public Task<string?> GetConfluenceMatchAsync(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return Task.FromResult<string?>(null);

            var pattern = @"\bmass[\s\-]?transit\b";
            if (Regex.IsMatch(message, pattern, RegexOptions.IgnoreCase))
            {
                Console.WriteLine("[ConfluenceService] MassTransit keyword detected!");
                return Task.FromResult<string?>(RawMassTransitHelpResponse);
            }

            return Task.FromResult<string?>(null);
        }
    }
}
