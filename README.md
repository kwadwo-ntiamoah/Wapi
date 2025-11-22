# DevJojo.WApi

A comprehensive WhatsApp API wrapper library for .NET applications, designed to simplify WhatsApp messaging integration.

[![NuGet](https://img.shields.io/nuget/v/DevJojo.WApi.svg)](https://www.nuget.org/packages/DevJojo.WApi/)
[![Downloads](https://img.shields.io/nuget/dt/DevJojo.WApi.svg)](https://www.nuget.org/packages/DevJojo.WApi/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Overview

DevJojo.WApi provides a clean, easy-to-use interface for interacting with WhatsApp Business API, enabling developers to send messages, handle webhooks, and manage WhatsApp communications programmatically.

## Features

• 📱 **Message Sending**: Send text, media, and interactive messages  
• 🔐 **Webhook Security**: Built-in webhook validation and encryption  
• 🎯 **Type-Safe**: Strongly typed models for all WhatsApp message types  
• ⚡ **Async/Await**: Full async support for modern .NET applications  
• 🔧 **Extensible**: Easy to extend and customize for specific needs  
• 📋 **Comprehensive**: Support for all WhatsApp Business API features  
• 🖼️ **Media Retrieval**: Retrieve media URLs and base64 strings from media IDs  
• ✅ **Graceful Status Handling (NEW)**: Delivered/read/etc status webhooks are safely ignored without errors

## Installation

Install the package via NuGet Package Manager:

```bash
dotnet add package DevJojo.WApi
```

Or via Package Manager Console:

```bash
Install-Package DevJojo.WApi
```

## Quick Start

### Basic Configuration

```csharp
using Wapi.src.Extensions;
using Microsoft.Extensions.DependencyInjection;

// Configure services
services.AddWapi(config =>
{
    config.AccessToken = "your-access-token";
    config.PhoneNumberId = "your-phone-number-id";
    config.BaseUrl = "https://graph.facebook.com";
    config.ApiVersion = "v20.0";
    config.VerificationToken = "your-verification-token";
});
```

### Sending Messages

```csharp
public class MessageService
{
    private readonly IWApi _wapi;

    public MessageService(IWApi wapi)
    {
        _wapi = wapi;
    }

    // Send a text message
    public async Task<ErrorOr<OutBoundMessageResponse>> SendTextMessage(string recipient, string text)
    {
        var message = new SendText { Body = text };
        return await _wapi.SendMessage(recipient, message);
    }

    // Send an image message
    public async Task<ErrorOr<OutBoundMessageResponse>> SendImageMessage(string recipient, string imageUrl, string caption)
    {
        var message = new SendImage 
        { 
            Link = imageUrl, 
            Caption = caption 
        };
        return await _wapi.SendMessage(recipient, message);
    }

    // Retrieve media URL and base64 from media ID
    public async Task<ErrorOr<(string mediaUrl, string base64String)>> GetMediaContent(string mediaId)
    {
        return await _wapi.GetMedia(mediaId);
    }
}
```

### Handling Webhooks

```csharp
[ApiController]
[Route("api/[controller]")]
public class WebhookController : ControllerBase
{
    private readonly IWApi _wapi;

    public WebhookController(IWApi wapi)
    {
        _wapi = wapi;
    }

    [HttpGet]
    public IActionResult VerifyWebhook([FromQuery] IQueryCollection queries)
    {
        var result = _wapi.ValidateInboundMessage(queries);
        
        if (result.IsError)
            return Unauthorized();
            
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> HandleWebhook([FromBody] string payload)
    {
        var result = _wapi.DecodeInboundMessage(payload);
        
        if (result.IsError)
            return BadRequest();

        var (displayName, message) = result.Value;

        // Status notifications are represented by MessageStatus and can be ignored
        if (message is MessageStatus status)
        {
            // Optionally log: status.Status
            return Ok();
        }
        
        switch (message.Type.ToLower())
        {
            case "text":
                var textMessage = message as TextMessage;
                break;
            case "image":
                var imageMessage = message as ImageMessage;
                var mediaResult = await _wapi.GetMedia(imageMessage.Image.Id);
                if (!mediaResult.IsError)
                {
                    var (mediaUrl, base64String) = mediaResult.Value;
                }
                break;
            // Handle other message types...
        }
        
        return Ok();
    }
}
```

## Configuration

### App Settings

```json
{
  "WhatsApp": {
    "AccessToken": "your-access-token",
    "PhoneNumberId": "your-phone-number-id",
    "BaseUrl": "https://graph.facebook.com",
    "ApiVersion": "v20.0",
    "VerificationToken": "your-webhook-verify-token",
    "BusinessAccountId": "your-business-account-id"
  }
}
```

### Dependency Injection

```csharp
using Wapi.src.Extensions;

public void ConfigureServices(IServiceCollection services)
{
    services.AddWapi(config =>
    {
        config.AccessToken = Configuration["WhatsApp:AccessToken"];
        config.PhoneNumberId = Configuration["WhatsApp:PhoneNumberId"];
        config.BaseUrl = Configuration["WhatsApp:BaseUrl"];
        config.ApiVersion = Configuration["WhatsApp:ApiVersion"];
        config.VerificationToken = Configuration["WhatsApp:VerificationToken"];
    });
}
```

## Message Types Supported

• **Text Messages**: Plain text with formatting support  
• **Media Messages**: Images, videos, documents, audio  
• **Interactive Messages**: Buttons, lists, quick replies  
• **Location Messages**: GPS coordinates and addresses  
• **Contact Messages**: vCard format contacts  
• **Reaction Messages**: React to existing messages  
• **Flow Messages**: Interactive forms and data collection  
• **Sticker Messages**: Animated and static stickers  
• **Status Notifications**: Delivered / Read events represented by `MessageStatus`

## Advanced Usage

### Interactive Quick Reply Messages

```csharp
var quickReply = new SendQuickReply
{
    Body = new SendQuickReplyBody { Text = "Choose an option:" },
    Action = new SendQuickReplyAction
    {
        Buttons = new List<SendQuickReplyActionButton>
        {
            new SendQuickReplyActionButton
            {
                Reply = new SendQuickReplyActionButtonReply
                {
                    Id = "option1",
                    Title = "Option 1"
                }
            },
            new SendQuickReplyActionButton
            {
                Reply = new SendQuickReplyActionButtonReply
                {
                    Id = "option2", 
                    Title = "Option 2"
                }
            }
        }
    }
};

await _wapi.SendMessage(recipient, quickReply);
```

### List Messages

```csharp
var listMessage = new SendListInteractive
{
    Header = new SendListHeader { Text = "Choose from menu" },
    Body = new SendListBody { Text = "Please select an option" },
    Action = new SendListAction
    {
        Button = "View Options",
        Sections = new List<SendListSection>
        {
            new SendListSection
            {
                Title = "Section 1",
                Rows = new List<SendListRow>
                {
                    new SendListRow { Id = "row1", Title = "Row 1", Description = "Description 1" },
                    new SendListRow { Id = "row2", Title = "Row 2", Description = "Description 2" }
                }
            }
        }
    }
};

await _wapi.SendMessage(recipient, listMessage);
```

### Flow Messages

```csharp
var flowMessage = new SendFlowInteractive
{
    Header = new SendFlowMessageHeader { Text = "Complete Form" },
    Body = new SendFlowMessageBody { Text = "Please fill out this form" },
    Action = new SendFlowInteractiveAction
    {
        FlowToken = "your-flow-token",
        FlowId = "your-flow-id",
        FlowCta = "Start",
        FlowActionPayload = new SendFlowActionPayload
        {
            Screen = "welcome_screen",
            Data = new Dictionary<string, object>
            {
                { "user_name", "John Doe" }
            }
        }
    }
};

await _wapi.SendMessage(recipient, flowMessage);
```

## Error Handling

```csharp
var result = await _wapi.SendMessage(recipient, message);

if (result.IsError)
{
    foreach (var error in result.Errors)
    {
        Console.WriteLine($"Error: {error.Code} - {error.Description}");
        switch (error.Type)
        {
            case ErrorType.Unauthorized:
                break;
            case ErrorType.Validation:
                break;
            case ErrorType.Failure:
                break;
        }
    }
}
else
{
    var response = result.Value;
    Console.WriteLine($"Message sent with ID: {response.Messages.First().Id}");
}
```

## Security Features

• **Signature Validation**: Verify webhook authenticity  
• **Encryption Support**: Built-in encryption helpers for Flow messages  
• **Token Management**: Secure access token handling  
• **Error Handling**: Comprehensive error types with ErrorOr pattern  

## Requirements

• .NET 8.0 or later  
• WhatsApp Business API account  
• Valid access token and phone number ID  

## Dependencies

• `BouncyCastle.Cryptography` (2.5.1) - For encryption utilities  
• `ErrorOr` (2.0.1) - For functional error handling  
• `Newtonsoft.Json` (13.0.3) - For JSON serialization  
• `Microsoft.Extensions.Http` (8.0.0) - For HTTP client factory  
• `Microsoft.Extensions.Configuration` (8.0.0) - For configuration binding  
• `Microsoft.AspNetCore.Http.Features` (5.0.17) - For HTTP features  

## Changelog

### v1.0.2 (Latest)
• **NEW**: Graceful handling of message status updates via `MessageStatus` type (delivered/read/etc)  
• **IMPROVEMENT**: Webhook decode no longer errors on status-only payloads  

### v1.0.1
• Added `GetMedia` method to retrieve media URL and base64 string from media ID  
• Fixed return type issue in media retrieval methods  
• Enhanced error handling in media operações  

### v1.0.0
• Initial release with core messaging, webhook support, media handling, interactive & flow messages, encryption utilities  

## Contributing

1. Fork the repository  
2. Create a feature branch  
3. Make your changes  
4. Add tests if applicable  
5. Submit a pull request  

## License

MIT License - see the [LICENSE](LICENSE) file for details.

## Support

• [Create an issue on GitHub](https://github.com/kwadwo-ntiamoah/Wapi/issues)  
• Check the documentation  
• Review examples in the repository  

## Links

• [NuGet Package](https://www.nuget.org/packages/DevJojo.WApi/)  
• [GitHub Repository](https://github.com/kwadwo-ntiamoah/Wapi)  
• [WhatsApp Business API Documentation](https://developers.facebook.com/docs/whatsapp)  

---

**DevJojo.WApi** - Making WhatsApp integration simple and powerful for .NET developers.