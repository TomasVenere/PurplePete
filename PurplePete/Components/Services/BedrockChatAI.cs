using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PurplePete.Services
{
    public class BedrockChatAI
    {
        private const string MassTransitHelpResponse = """
Understanding Common RabbitMQ Issues with MassTransit:
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

Step-by-Step Debugging Process:
1. Verify RabbitMQ Configuration:
   - Access the RabbitMQ Management Console (http://localhost:15672).
   - Confirm your exchange and queue setup.
   - Ensure the exchange type aligns with your routing strategy.
2. Check Producer Settings (MassTransit).
3. Check Consumer Settings (MassTransit).
4. Test with a Direct Queue (Basic Setup).
5. Review Logs and Errors.

Advanced Debugging with MassTransit Logging:
Use Serilog or Diagnostic Source Logging.

Enhancing Debugging with Custom Middleware:
Use `MessageLoggingFilter<T>` to log message content.

Quick Troubleshooting Checklist:
✅ Verify Exchange and Queue Names.
✅ Check Exchange Type.
✅ Confirm Routing Key.
✅ Ensure Queue Exists.
✅ Check Consumer Config.
✅ Review Connection (vHost, credentials).
✅ Inspect Logs.
✅ Test Manually.

Conclusion:
Debugging RabbitMQ and MassTransit issues can be challenging, but this guide helps identify and fix problems effectively.
""";

        private const string DefaultResponse = "Hi! I'm Purple Pete. We're currently under maintenance, but feel free to leave your question!";

        public Task<string> GetResponseAsync(string message)
        {
            Console.WriteLine($"[BedrockChatAI] Received message: {message}");

            if (string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine("[BedrockChatAI] Empty message. Returning default.");
                return Task.FromResult(DefaultResponse);
            }

            // Match variations like: masstransit, mass transit, mass-transit
            var pattern = @"\bmass[\s\-]?transit\b";
            if (Regex.IsMatch(message, pattern, RegexOptions.IgnoreCase))
            {
                Console.WriteLine("[BedrockChatAI] MassTransit keyword detected!");
                return Task.FromResult(MassTransitHelpResponse);
            }

            Console.WriteLine("[BedrockChatAI] No keyword match. Returning default.");
            return Task.FromResult(DefaultResponse);
        }
    }
}
