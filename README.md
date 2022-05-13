# RD.NET

AllDebrid .NET wrapper library written in C#

Supports all API calls and Pin authentication.

## Usage

Create an instance of `AllDebridNETClient` for each user you want to authenticate. If you need to support multiple users you will need to create a new instance every time you switch users.

```csharp
var client = new AllDebridNETClient("agent name", "api key");
```

Pass in the Agent name you wish to use for your application. This can be any meaningfull name.

Pass in the Api Key for the user. If you don't have an API key yet, you can leave this blank and use Pin authentication.

The method naming followings the API documentation closely:
```csharp
var client = new AllDebridNETClient("agent name", "api key");

// https://docs.alldebrid.com/#magnet
var result = await client.Magnet.UploadMagnetAsync(magnet);
```

The following API calls are available:
```csharp
var client = new AllDebridNETClient("agent name", "api key");

client.Authentication.
client.Hosts.
client.Links.
client.Magnet.
client.User.
```

## Authentication

Each user has its own API key, which can be found here: <https://alldebrid.com/apikeys>.

### Get the API token by pin

To authenticate the user call:

```csharp
var result = await client.Authentication.GetPin();
```

This will give you a URL and code to have the user verify their device.

You can poll the result by doing:

```csharp
var result = await client.Authentication.CheckPin(check, pin);
```

If the result does not have an API key the user has not verified the pin yet.

When the API token is set, you can use this token to verify the user. The token is automatically set in the client when this method completes.

## Unit tests

The unit tests are not designed to be ran all at once, they are used to act as a test client.

Create a file `setup.txt` and put your API token in there.

Some functions will need replacement ID's to work properly.