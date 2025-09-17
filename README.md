# DevJojo.WApi

A comprehensive WhatsApp API wrapper library for .NET applications, designed to simplify WhatsApp messaging integration.

## Overview

DevJojo.WApi provides a clean, easy-to-use interface for interacting with WhatsApp Business API, enabling developers to send messages, handle webhooks, and manage WhatsApp communications programmatically.

## Features

- 📱 **Message Sending**: Send text, media, and interactive messages
- 🔐 **Webhook Security**: Built-in webhook validation and encryption
- 🎯 **Type-Safe**: Strongly typed models for all WhatsApp message types
- ⚡ **Async/Await**: Full async support for modern .NET applications
- 🔧 **Extensible**: Easy to extend and customize for specific needs
- 📋 **Comprehensive**: Support for all WhatsApp Business API features

## Installation

Install the package via NuGet Package Manager:

```bash
dotnet add package DevJojo.WApi
```

Or via Package Manager Console:

```powershell
Install-Package DevJojo.WApi
```

## Quick Start

### Basic Configuration

```csharp
using Wapi.Services;
using Microsoft.Extensions.DependencyInjection;

// Configure services
services.AddWhatsAppServices(configuration);
```

### Sending Messages

```csharp
using Wapi.Services;

var wapiService = serviceProvider.GetService<IWapiService>();

// Send a text message
await wapiService.SendMessageAsync("Hello, World!", "1234567890");

// Send a media message
await wapiService.SendMediaMessageAsync(
    mediaUrl: "https://example.com/image.jpg",
    recipient: "1234567890",
    caption: "Check out this image!"
);
```

### Handling Webhooks

```csharp
using Wapi.Models;

// In your webhook controller
[HttpPost("webhook")]
public async Task<IActionResult> HandleWebhook([FromBody] WebhookPayload payload)
{
    // Process incoming messages
    foreach (var entry in payload.Entry)
    {
        foreach (var change in entry.Changes)
        {
            if (change.Value.Messages?.Any() == true)
            {
                foreach (var message in change.Value.Messages)
                {
                    // Handle the message
                    await ProcessMessageAsync(message);
                }
            }
        }
    }
    
    return Ok();
}
```

## Configuration

### App Settings

```json
{
  "WhatsApp": {
    "ApiBaseUrl": "https://graph.facebook.com/v17.0",
    "PhoneNumberId": "your-phone-number-id",
    "AccessToken": "your-access-token",
    "WebhookVerifyToken": "your-webhook-verify-token"
  }
}
```

### Dependency Injection

```csharp
using Wapi.Extensions;

public void ConfigureServices(IServiceCollection services)
{
    services.AddWhatsAppApi(configuration);
    
    // Configure HTTP client
    services.AddHttpClient<IWapiService, WapiService>();
}
```

## Advanced Usage

### Custom Message Templates

```csharp
var templateMessage = new TemplateMessage
{
    Name = "hello_world",
    Language = new Language { Code = "en_US" },
    Components = new List<Component>
    {
        new Component
        {
            Type = "body",
            Parameters = new List<Parameter>
            {
                new Parameter { Type = "text", Text = "John Doe" }
            }
        }
    }
};

await wapiService.SendTemplateMessageAsync(templateMessage, "1234567890");
```

### Interactive Messages

```csharp
var interactiveMessage = new InteractiveMessage
{
    Type = "button",
    Body = new Body { Text = "Please choose an option:" },
    Action = new Action
    {
        Buttons = new List<Button>
        {
            new Button { Id = "option1", Title = "Option 1" },
            new Button { Id = "option2", Title = "Option 2" }
        }
    }
};

await wapiService.SendInteractiveMessageAsync(interactiveMessage, "1234567890");
```

### Media Upload

```csharp
// Upload media first
var mediaResponse = await wapiService.UploadMediaAsync(
    mediaBytes: fileBytes,
    mediaType: "image/jpeg",
    fileName: "image.jpg"
);

// Send media message using media ID
await wapiService.SendMediaMessageByIdAsync(
    mediaId: mediaResponse.Id,
    recipient: "1234567890",
    mediaType: "image"
);
```

## Message Types Supported

- **Text Messages**: Plain text with formatting support
- **Media Messages**: Images, videos, documents, audio
- **Interactive Messages**: Buttons, lists, quick replies
- **Template Messages**: Pre-approved business templates
- **Location Messages**: GPS coordinates and addresses
- **Contact Messages**: vCard format contacts

## Error Handling

```csharp
try
{
    await wapiService.SendMessageAsync("Hello", "1234567890");
}
catch (WhatsAppApiException ex)
{
    // Handle WhatsApp API specific errors
    Console.WriteLine($"WhatsApp Error: {ex.ErrorCode} - {ex.Message}");
}
catch (HttpRequestException ex)
{
    // Handle network errors
    Console.WriteLine($"Network Error: {ex.Message}");
}
```

## Webhook Validation

```csharp
using Wapi.Utilities;

public class WebhookController : ControllerBase
{
    private readonly IWebhookValidator _validator;
    
    [HttpPost("webhook")]
    public async Task<IActionResult> HandleWebhook(
        [FromBody] string payload,
        [FromHeader("X-Hub-Signature-256")] string signature)
    {
        if (!_validator.ValidateSignature(payload, signature))
        {
            return Unauthorized();
        }
        
        // Process webhook...
        return Ok();
    }
}
```

## Security Features

- **Signature Validation**: Verify webhook authenticity
- **Encryption Support**: Built-in encryption helpers
- **Token Management**: Secure access token handling
- **Rate Limiting**: Built-in rate limiting support

## Requirements

- .NET 8.0 or later
- WhatsApp Business API account
- Valid access token and phone number ID

## Dependencies

- `BouncyCastle.Cryptography` (2.5.1) - For encryption utilities
- `Newtonsoft.Json` (13.0.3) - For JSON serialization
- `Microsoft.Extensions.Http` (8.0.0) - For HTTP client factory
- `Microsoft.Extensions.Configuration` (8.0.0) - For configuration binding

## Contributing

We welcome contributions! Please feel free to submit issues, feature requests, or pull requests.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For support and questions:
- Create an issue on GitHub
- Check the documentation
- Review the examples in the repository

## Changelog

### v1.0.0
- Initial release
- Core messaging functionality
- Webhook support
- Media handling
- Interactive messages
- Template messages

---

**DevJojo.WApi** - Making WhatsApp integration simple and powerful for .NET developers.