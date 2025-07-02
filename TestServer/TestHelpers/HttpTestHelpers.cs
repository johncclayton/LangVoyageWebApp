using System.Net;
using System.Text;
using System.Text.Json;

namespace TestServer.TestHelpers;

/// <summary>
/// Utilities for HTTP testing with common patterns for API testing.
/// Provides methods for making requests, parsing responses, and asserting HTTP behavior.
/// </summary>
public static class HttpTestHelpers
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Makes a GET request and returns the deserialized response.
    /// </summary>
    public static async Task<T> GetAsync<T>(HttpClient client, string url)
    {
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(content, JsonOptions);
        
        return result ?? throw new InvalidOperationException($"Failed to deserialize response from {url}");
    }

    /// <summary>
    /// Makes a GET request and returns both the response and status code.
    /// Useful for testing error scenarios.
    /// </summary>
    public static async Task<(HttpResponseMessage Response, T? Data)> GetWithStatusAsync<T>(HttpClient client, string url)
    {
        var response = await client.GetAsync(url);
        T? data = default;
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(content))
            {
                data = JsonSerializer.Deserialize<T>(content, JsonOptions);
            }
        }
        
        return (response, data);
    }

    /// <summary>
    /// Makes a PUT request with JSON payload and returns the deserialized response.
    /// </summary>
    public static async Task<T> PutAsync<T>(HttpClient client, string url, object payload)
    {
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await client.PutAsync(url, content);
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(responseContent, JsonOptions);
        
        return result ?? throw new InvalidOperationException($"Failed to deserialize response from PUT {url}");
    }

    /// <summary>
    /// Makes a PUT request with JSON payload and returns both response and status code.
    /// </summary>
    public static async Task<(HttpResponseMessage Response, T? Data)> PutWithStatusAsync<T>(HttpClient client, string url, object payload)
    {
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await client.PutAsync(url, content);
        T? data = default;
        
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(responseContent))
            {
                data = JsonSerializer.Deserialize<T>(responseContent, JsonOptions);
            }
        }
        
        return (response, data);
    }

    /// <summary>
    /// Makes a DELETE request and returns the response.
    /// </summary>
    public static async Task<HttpResponseMessage> DeleteAsync(HttpClient client, string url)
    {
        return await client.DeleteAsync(url);
    }

    /// <summary>
    /// Asserts that the HTTP response has the expected status code.
    /// </summary>
    public static void AssertStatusCode(HttpResponseMessage response, HttpStatusCode expectedStatusCode)
    {
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }

    /// <summary>
    /// Asserts that the HTTP response is successful (2xx status code).
    /// </summary>
    public static void AssertSuccess(HttpResponseMessage response)
    {
        Assert.True(response.IsSuccessStatusCode, 
            $"Expected successful status code but got {response.StatusCode}. Response: {response.Content.ReadAsStringAsync().Result}");
    }

    /// <summary>
    /// Asserts that the HTTP response indicates a client error (4xx status code).
    /// </summary>
    public static void AssertClientError(HttpResponseMessage response)
    {
        Assert.True((int)response.StatusCode >= 400 && (int)response.StatusCode < 500,
            $"Expected client error status code (4xx) but got {response.StatusCode}");
    }

    /// <summary>
    /// Asserts that the HTTP response indicates a server error (5xx status code).
    /// </summary>
    public static void AssertServerError(HttpResponseMessage response)
    {
        Assert.True((int)response.StatusCode >= 500 && (int)response.StatusCode < 600,
            $"Expected server error status code (5xx) but got {response.StatusCode}");
    }

    /// <summary>
    /// Creates a URL with path parameters replaced.
    /// </summary>
    public static string BuildUrl(string template, params object[] args)
    {
        return string.Format(template, args);
    }
}