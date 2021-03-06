﻿
namespace NomadCode.BotFramework
{
	using Microsoft.Rest;
	using Newtonsoft.Json;
	using System.Collections;
	using System.Collections.Generic;
	using System.IO;
	using System.Net;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading;
	using System.Threading.Tasks;

	/// <summary>
	/// Conversations operations.
	/// </summary>
	public partial class Conversations : IServiceOperations<DirectLineClient>, IConversations
	{
		/// <summary>
		/// Initializes a new instance of the Conversations class.
		/// </summary>
		/// <param name='client'>
		/// Reference to the service client.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when a required parameter is null
		/// </exception>
		public Conversations (DirectLineClient client)
		{
			if (client == null)
			{
				throw new System.ArgumentNullException ("client");
			}
			Client = client;
		}

		/// <summary>
		/// Gets a reference to the DirectLineClient
		/// </summary>
		public DirectLineClient Client { get; private set; }

		/// <summary>
		/// Start a new conversation
		/// </summary>
		/// <param name='customHeaders'>
		/// Headers that will be added to request.
		/// </param>
		/// <param name='cancellationToken'>
		/// The cancellation token.
		/// </param>
		/// <exception cref="HttpOperationException">
		/// Thrown when the operation returned an invalid status code
		/// </exception>
		/// <exception cref="SerializationException">
		/// Thrown when unable to deserialize the response
		/// </exception>
		/// <return>
		/// A response object containing the response body and response headers.
		/// </return>
		public async Task<HttpOperationResponse<Conversation>> StartConversationWithHttpMessagesAsync (Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default (CancellationToken))
		{
			// Tracing
			bool _shouldTrace = ServiceClientTracing.IsEnabled;
			string _invocationId = null;
			if (_shouldTrace)
			{
				_invocationId = ServiceClientTracing.NextInvocationId.ToString ();
				Dictionary<string, object> tracingParameters = new Dictionary<string, object> ();
				tracingParameters.Add ("cancellationToken", cancellationToken);
				ServiceClientTracing.Enter (_invocationId, this, "StartConversation", tracingParameters);
			}
			// Construct URL
			var _baseUrl = Client.BaseUri.AbsoluteUri;
			var _url = new System.Uri (new System.Uri (_baseUrl + (_baseUrl.EndsWith ("/") ? "" : "/")), "v3/directline/conversations").ToString ();
			// Create HTTP transport objects
			var _httpRequest = new HttpRequestMessage ();
			HttpResponseMessage _httpResponse = null;
			_httpRequest.Method = new HttpMethod ("POST");
			_httpRequest.RequestUri = new System.Uri (_url);
			// Set Headers


			if (customHeaders != null)
			{
				foreach (var _header in customHeaders)
				{
					if (_httpRequest.Headers.Contains (_header.Key))
					{
						_httpRequest.Headers.Remove (_header.Key);
					}
					_httpRequest.Headers.TryAddWithoutValidation (_header.Key, _header.Value);
				}
			}

			// Serialize Request
			string _requestContent = null;
			// Set Credentials
			if (Client.Credentials != null)
			{
				cancellationToken.ThrowIfCancellationRequested ();
				await Client.Credentials.ProcessHttpRequestAsync (_httpRequest, cancellationToken).ConfigureAwait (false);
			}
			// Send Request
			if (_shouldTrace)
			{
				ServiceClientTracing.SendRequest (_invocationId, _httpRequest);
			}
			cancellationToken.ThrowIfCancellationRequested ();
			_httpResponse = await Client.HttpClient.SendAsync (_httpRequest, cancellationToken).ConfigureAwait (false);
			if (_shouldTrace)
			{
				ServiceClientTracing.ReceiveResponse (_invocationId, _httpResponse);
			}
			HttpStatusCode _statusCode = _httpResponse.StatusCode;
			cancellationToken.ThrowIfCancellationRequested ();
			string _responseContent = null;
			if ((int)_statusCode != 200 && (int)_statusCode != 201 && (int)_statusCode != 401 && (int)_statusCode != 403 && (int)_statusCode != 404 && (int)_statusCode != 409)
			{
				var ex = new HttpOperationException (string.Format ("Operation returned an invalid status code '{0}'", _statusCode));
				if (_httpResponse.Content != null)
				{
					_responseContent = await _httpResponse.Content.ReadAsStringAsync ().ConfigureAwait (false);
				}
				else
				{
					_responseContent = string.Empty;
				}
				ex.Request = new HttpRequestMessageWrapper (_httpRequest, _requestContent);
				ex.Response = new HttpResponseMessageWrapper (_httpResponse, _responseContent);
				if (_shouldTrace)
				{
					ServiceClientTracing.Error (_invocationId, ex);
				}
				_httpRequest.Dispose ();
				if (_httpResponse != null)
				{
					_httpResponse.Dispose ();
				}
				throw ex;
			}
			// Create Result
			var _result = new HttpOperationResponse<Conversation> ();
			_result.Request = _httpRequest;
			_result.Response = _httpResponse;
			// Deserialize Response
			if ((int)_statusCode == 200)
			{
				_responseContent = await _httpResponse.Content.ReadAsStringAsync ().ConfigureAwait (false);
				try
				{
					_result.Body = Microsoft.Rest.Serialization.SafeJsonConvert.DeserializeObject<Conversation> (_responseContent, Client.DeserializationSettings);
				}
				catch (JsonException ex)
				{
					_httpRequest.Dispose ();
					if (_httpResponse != null)
					{
						_httpResponse.Dispose ();
					}
					throw new SerializationException ("Unable to deserialize the response.", _responseContent, ex);
				}
			}
			// Deserialize Response
			if ((int)_statusCode == 201)
			{
				_responseContent = await _httpResponse.Content.ReadAsStringAsync ().ConfigureAwait (false);
				try
				{
					_result.Body = Microsoft.Rest.Serialization.SafeJsonConvert.DeserializeObject<Conversation> (_responseContent, Client.DeserializationSettings);
				}
				catch (JsonException ex)
				{
					_httpRequest.Dispose ();
					if (_httpResponse != null)
					{
						_httpResponse.Dispose ();
					}
					throw new SerializationException ("Unable to deserialize the response.", _responseContent, ex);
				}
			}
			if (_shouldTrace)
			{
				ServiceClientTracing.Exit (_invocationId, _result);
			}
			return _result;
		}

		/// <summary>
		/// Get information about an existing conversation
		/// </summary>
		/// <param name='conversationId'>
		/// </param>
		/// <param name='watermark'>
		/// </param>
		/// <param name='customHeaders'>
		/// Headers that will be added to request.
		/// </param>
		/// <param name='cancellationToken'>
		/// The cancellation token.
		/// </param>
		/// <exception cref="HttpOperationException">
		/// Thrown when the operation returned an invalid status code
		/// </exception>
		/// <exception cref="SerializationException">
		/// Thrown when unable to deserialize the response
		/// </exception>
		/// <exception cref="ValidationException">
		/// Thrown when a required parameter is null
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when a required parameter is null
		/// </exception>
		/// <return>
		/// A response object containing the response body and response headers.
		/// </return>
		public async Task<HttpOperationResponse<Conversation>> ReconnectToConversationWithHttpMessagesAsync (string conversationId, string watermark = default (string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default (CancellationToken))
		{
			if (conversationId == null)
			{
				throw new ValidationException (ValidationRules.CannotBeNull, "conversationId");
			}
			// Tracing
			bool _shouldTrace = ServiceClientTracing.IsEnabled;
			string _invocationId = null;
			if (_shouldTrace)
			{
				_invocationId = ServiceClientTracing.NextInvocationId.ToString ();
				Dictionary<string, object> tracingParameters = new Dictionary<string, object> ();
				tracingParameters.Add ("conversationId", conversationId);
				tracingParameters.Add ("watermark", watermark);
				tracingParameters.Add ("cancellationToken", cancellationToken);
				ServiceClientTracing.Enter (_invocationId, this, "ReconnectToConversation", tracingParameters);
			}
			// Construct URL
			var _baseUrl = Client.BaseUri.AbsoluteUri;
			var _url = new System.Uri (new System.Uri (_baseUrl + (_baseUrl.EndsWith ("/") ? "" : "/")), "v3/directline/conversations/{conversationId}").ToString ();
			_url = _url.Replace ("{conversationId}", System.Uri.EscapeDataString (conversationId));
			List<string> _queryParameters = new List<string> ();
			if (watermark != null)
			{
				_queryParameters.Add (string.Format ("watermark={0}", System.Uri.EscapeDataString (watermark)));
			}
			if (_queryParameters.Count > 0)
			{
				_url += "?" + string.Join ("&", _queryParameters);
			}
			// Create HTTP transport objects
			var _httpRequest = new HttpRequestMessage ();
			HttpResponseMessage _httpResponse = null;
			_httpRequest.Method = new HttpMethod ("GET");
			_httpRequest.RequestUri = new System.Uri (_url);
			// Set Headers


			if (customHeaders != null)
			{
				foreach (var _header in customHeaders)
				{
					if (_httpRequest.Headers.Contains (_header.Key))
					{
						_httpRequest.Headers.Remove (_header.Key);
					}
					_httpRequest.Headers.TryAddWithoutValidation (_header.Key, _header.Value);
				}
			}

			// Serialize Request
			string _requestContent = null;
			// Set Credentials
			if (Client.Credentials != null)
			{
				cancellationToken.ThrowIfCancellationRequested ();
				await Client.Credentials.ProcessHttpRequestAsync (_httpRequest, cancellationToken).ConfigureAwait (false);
			}
			// Send Request
			if (_shouldTrace)
			{
				ServiceClientTracing.SendRequest (_invocationId, _httpRequest);
			}
			cancellationToken.ThrowIfCancellationRequested ();
			_httpResponse = await Client.HttpClient.SendAsync (_httpRequest, cancellationToken).ConfigureAwait (false);
			if (_shouldTrace)
			{
				ServiceClientTracing.ReceiveResponse (_invocationId, _httpResponse);
			}
			HttpStatusCode _statusCode = _httpResponse.StatusCode;
			cancellationToken.ThrowIfCancellationRequested ();
			string _responseContent = null;
			if ((int)_statusCode != 200 && (int)_statusCode != 401 && (int)_statusCode != 403 && (int)_statusCode != 404)
			{
				var ex = new HttpOperationException (string.Format ("Operation returned an invalid status code '{0}'", _statusCode));
				if (_httpResponse.Content != null)
				{
					_responseContent = await _httpResponse.Content.ReadAsStringAsync ().ConfigureAwait (false);
				}
				else
				{
					_responseContent = string.Empty;
				}
				ex.Request = new HttpRequestMessageWrapper (_httpRequest, _requestContent);
				ex.Response = new HttpResponseMessageWrapper (_httpResponse, _responseContent);
				if (_shouldTrace)
				{
					ServiceClientTracing.Error (_invocationId, ex);
				}
				_httpRequest.Dispose ();
				if (_httpResponse != null)
				{
					_httpResponse.Dispose ();
				}
				throw ex;
			}
			// Create Result
			var _result = new HttpOperationResponse<Conversation> ();
			_result.Request = _httpRequest;
			_result.Response = _httpResponse;
			// Deserialize Response
			if ((int)_statusCode == 200)
			{
				_responseContent = await _httpResponse.Content.ReadAsStringAsync ().ConfigureAwait (false);
				try
				{
					_result.Body = Microsoft.Rest.Serialization.SafeJsonConvert.DeserializeObject<Conversation> (_responseContent, Client.DeserializationSettings);
				}
				catch (JsonException ex)
				{
					_httpRequest.Dispose ();
					if (_httpResponse != null)
					{
						_httpResponse.Dispose ();
					}
					throw new SerializationException ("Unable to deserialize the response.", _responseContent, ex);
				}
			}
			if (_shouldTrace)
			{
				ServiceClientTracing.Exit (_invocationId, _result);
			}
			return _result;
		}

		/// <summary>
		/// Get activities in this conversation. This method is paged with the
		/// 'watermark' parameter.
		/// </summary>
		/// <param name='conversationId'>
		/// Conversation ID
		/// </param>
		/// <param name='watermark'>
		/// (Optional) only returns activities newer than this watermark
		/// </param>
		/// <param name='customHeaders'>
		/// Headers that will be added to request.
		/// </param>
		/// <param name='cancellationToken'>
		/// The cancellation token.
		/// </param>
		/// <exception cref="HttpOperationException">
		/// Thrown when the operation returned an invalid status code
		/// </exception>
		/// <exception cref="SerializationException">
		/// Thrown when unable to deserialize the response
		/// </exception>
		/// <exception cref="ValidationException">
		/// Thrown when a required parameter is null
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when a required parameter is null
		/// </exception>
		/// <return>
		/// A response object containing the response body and response headers.
		/// </return>
		public async Task<HttpOperationResponse<ActivitySet>> GetActivitiesWithHttpMessagesAsync (string conversationId, string watermark = default (string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default (CancellationToken))
		{
			if (conversationId == null)
			{
				throw new ValidationException (ValidationRules.CannotBeNull, "conversationId");
			}
			// Tracing
			bool _shouldTrace = ServiceClientTracing.IsEnabled;
			string _invocationId = null;
			if (_shouldTrace)
			{
				_invocationId = ServiceClientTracing.NextInvocationId.ToString ();
				Dictionary<string, object> tracingParameters = new Dictionary<string, object> ();
				tracingParameters.Add ("conversationId", conversationId);
				tracingParameters.Add ("watermark", watermark);
				tracingParameters.Add ("cancellationToken", cancellationToken);
				ServiceClientTracing.Enter (_invocationId, this, "GetActivities", tracingParameters);
			}
			// Construct URL
			var _baseUrl = Client.BaseUri.AbsoluteUri;
			var _url = new System.Uri (new System.Uri (_baseUrl + (_baseUrl.EndsWith ("/") ? "" : "/")), "v3/directline/conversations/{conversationId}/activities").ToString ();
			_url = _url.Replace ("{conversationId}", System.Uri.EscapeDataString (conversationId));
			List<string> _queryParameters = new List<string> ();
			if (watermark != null)
			{
				_queryParameters.Add (string.Format ("watermark={0}", System.Uri.EscapeDataString (watermark)));
			}
			if (_queryParameters.Count > 0)
			{
				_url += "?" + string.Join ("&", _queryParameters);
			}
			// Create HTTP transport objects
			var _httpRequest = new HttpRequestMessage ();
			HttpResponseMessage _httpResponse = null;
			_httpRequest.Method = new HttpMethod ("GET");
			_httpRequest.RequestUri = new System.Uri (_url);
			// Set Headers


			if (customHeaders != null)
			{
				foreach (var _header in customHeaders)
				{
					if (_httpRequest.Headers.Contains (_header.Key))
					{
						_httpRequest.Headers.Remove (_header.Key);
					}
					_httpRequest.Headers.TryAddWithoutValidation (_header.Key, _header.Value);
				}
			}

			// Serialize Request
			string _requestContent = null;
			// Set Credentials
			if (Client.Credentials != null)
			{
				cancellationToken.ThrowIfCancellationRequested ();
				await Client.Credentials.ProcessHttpRequestAsync (_httpRequest, cancellationToken).ConfigureAwait (false);
			}
			// Send Request
			if (_shouldTrace)
			{
				ServiceClientTracing.SendRequest (_invocationId, _httpRequest);
			}
			cancellationToken.ThrowIfCancellationRequested ();
			_httpResponse = await Client.HttpClient.SendAsync (_httpRequest, cancellationToken).ConfigureAwait (false);
			if (_shouldTrace)
			{
				ServiceClientTracing.ReceiveResponse (_invocationId, _httpResponse);
			}
			HttpStatusCode _statusCode = _httpResponse.StatusCode;
			cancellationToken.ThrowIfCancellationRequested ();
			string _responseContent = null;
			if ((int)_statusCode != 200 && (int)_statusCode != 401 && (int)_statusCode != 403 && (int)_statusCode != 404)
			{
				var ex = new HttpOperationException (string.Format ("Operation returned an invalid status code '{0}'", _statusCode));
				if (_httpResponse.Content != null)
				{
					_responseContent = await _httpResponse.Content.ReadAsStringAsync ().ConfigureAwait (false);
				}
				else
				{
					_responseContent = string.Empty;
				}
				ex.Request = new HttpRequestMessageWrapper (_httpRequest, _requestContent);
				ex.Response = new HttpResponseMessageWrapper (_httpResponse, _responseContent);
				if (_shouldTrace)
				{
					ServiceClientTracing.Error (_invocationId, ex);
				}
				_httpRequest.Dispose ();
				if (_httpResponse != null)
				{
					_httpResponse.Dispose ();
				}
				throw ex;
			}
			// Create Result
			var _result = new HttpOperationResponse<ActivitySet> ();
			_result.Request = _httpRequest;
			_result.Response = _httpResponse;
			// Deserialize Response
			if ((int)_statusCode == 200)
			{
				_responseContent = await _httpResponse.Content.ReadAsStringAsync ().ConfigureAwait (false);
				try
				{
					_result.Body = Microsoft.Rest.Serialization.SafeJsonConvert.DeserializeObject<ActivitySet> (_responseContent, Client.DeserializationSettings);
				}
				catch (JsonException ex)
				{
					_httpRequest.Dispose ();
					if (_httpResponse != null)
					{
						_httpResponse.Dispose ();
					}
					throw new SerializationException ("Unable to deserialize the response.", _responseContent, ex);
				}
			}
			if (_shouldTrace)
			{
				ServiceClientTracing.Exit (_invocationId, _result);
			}
			return _result;
		}

		/// <summary>
		/// Send an activity
		/// </summary>
		/// <param name='conversationId'>
		/// Conversation ID
		/// </param>
		/// <param name='activity'>
		/// Activity to send
		/// </param>
		/// <param name='customHeaders'>
		/// Headers that will be added to request.
		/// </param>
		/// <param name='cancellationToken'>
		/// The cancellation token.
		/// </param>
		/// <exception cref="HttpOperationException">
		/// Thrown when the operation returned an invalid status code
		/// </exception>
		/// <exception cref="SerializationException">
		/// Thrown when unable to deserialize the response
		/// </exception>
		/// <exception cref="ValidationException">
		/// Thrown when a required parameter is null
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when a required parameter is null
		/// </exception>
		/// <return>
		/// A response object containing the response body and response headers.
		/// </return>
		public async Task<HttpOperationResponse<ResourceResponse>> PostActivityWithHttpMessagesAsync (string conversationId, Activity activity, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default (CancellationToken))
		{
			if (conversationId == null)
			{
				throw new ValidationException (ValidationRules.CannotBeNull, "conversationId");
			}
			if (activity == null)
			{
				throw new ValidationException (ValidationRules.CannotBeNull, "activity");
			}
			// Tracing
			bool _shouldTrace = ServiceClientTracing.IsEnabled;
			string _invocationId = null;
			if (_shouldTrace)
			{
				_invocationId = ServiceClientTracing.NextInvocationId.ToString ();
				Dictionary<string, object> tracingParameters = new Dictionary<string, object> ();
				tracingParameters.Add ("conversationId", conversationId);
				tracingParameters.Add ("activity", activity);
				tracingParameters.Add ("cancellationToken", cancellationToken);
				ServiceClientTracing.Enter (_invocationId, this, "PostActivity", tracingParameters);
			}
			// Construct URL
			var _baseUrl = Client.BaseUri.AbsoluteUri;
			var _url = new System.Uri (new System.Uri (_baseUrl + (_baseUrl.EndsWith ("/") ? "" : "/")), "v3/directline/conversations/{conversationId}/activities").ToString ();
			_url = _url.Replace ("{conversationId}", System.Uri.EscapeDataString (conversationId));
			// Create HTTP transport objects
			var _httpRequest = new HttpRequestMessage ();
			HttpResponseMessage _httpResponse = null;
			_httpRequest.Method = new HttpMethod ("POST");
			_httpRequest.RequestUri = new System.Uri (_url);
			// Set Headers


			if (customHeaders != null)
			{
				foreach (var _header in customHeaders)
				{
					if (_httpRequest.Headers.Contains (_header.Key))
					{
						_httpRequest.Headers.Remove (_header.Key);
					}
					_httpRequest.Headers.TryAddWithoutValidation (_header.Key, _header.Value);
				}
			}

			// Serialize Request
			string _requestContent = null;
			if (activity != null)
			{
				_requestContent = Microsoft.Rest.Serialization.SafeJsonConvert.SerializeObject (activity, Client.SerializationSettings);
				_httpRequest.Content = new StringContent (_requestContent, System.Text.Encoding.UTF8);
				_httpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse ("application/json; charset=utf-8");
			}
			// Set Credentials
			if (Client.Credentials != null)
			{
				cancellationToken.ThrowIfCancellationRequested ();
				await Client.Credentials.ProcessHttpRequestAsync (_httpRequest, cancellationToken).ConfigureAwait (false);
			}
			// Send Request
			if (_shouldTrace)
			{
				ServiceClientTracing.SendRequest (_invocationId, _httpRequest);
			}
			cancellationToken.ThrowIfCancellationRequested ();
			_httpResponse = await Client.HttpClient.SendAsync (_httpRequest, cancellationToken).ConfigureAwait (false);
			if (_shouldTrace)
			{
				ServiceClientTracing.ReceiveResponse (_invocationId, _httpResponse);
			}
			HttpStatusCode _statusCode = _httpResponse.StatusCode;
			cancellationToken.ThrowIfCancellationRequested ();
			string _responseContent = null;
			if ((int)_statusCode != 200 && (int)_statusCode != 204 && (int)_statusCode != 400 && (int)_statusCode != 401 && (int)_statusCode != 403 && (int)_statusCode != 404 && (int)_statusCode != 500 && (int)_statusCode != 502)
			{
				var ex = new HttpOperationException (string.Format ("Operation returned an invalid status code '{0}'", _statusCode));
				if (_httpResponse.Content != null)
				{
					_responseContent = await _httpResponse.Content.ReadAsStringAsync ().ConfigureAwait (false);
				}
				else
				{
					_responseContent = string.Empty;
				}
				ex.Request = new HttpRequestMessageWrapper (_httpRequest, _requestContent);
				ex.Response = new HttpResponseMessageWrapper (_httpResponse, _responseContent);
				if (_shouldTrace)
				{
					ServiceClientTracing.Error (_invocationId, ex);
				}
				_httpRequest.Dispose ();
				if (_httpResponse != null)
				{
					_httpResponse.Dispose ();
				}
				throw ex;
			}
			// Create Result
			var _result = new HttpOperationResponse<ResourceResponse> ();
			_result.Request = _httpRequest;
			_result.Response = _httpResponse;
			// Deserialize Response
			if ((int)_statusCode == 200)
			{
				_responseContent = await _httpResponse.Content.ReadAsStringAsync ().ConfigureAwait (false);
				try
				{
					_result.Body = Microsoft.Rest.Serialization.SafeJsonConvert.DeserializeObject<ResourceResponse> (_responseContent, Client.DeserializationSettings);
				}
				catch (JsonException ex)
				{
					_httpRequest.Dispose ();
					if (_httpResponse != null)
					{
						_httpResponse.Dispose ();
					}
					throw new SerializationException ("Unable to deserialize the response.", _responseContent, ex);
				}
			}
			if (_shouldTrace)
			{
				ServiceClientTracing.Exit (_invocationId, _result);
			}
			return _result;
		}

		/// <summary>
		/// Upload file(s) and send as attachment(s)
		/// </summary>
		/// <param name='conversationId'>
		/// </param>
		/// <param name='file'>
		/// </param>
		/// <param name='userId'>
		/// </param>
		/// <param name='customHeaders'>
		/// Headers that will be added to request.
		/// </param>
		/// <param name='cancellationToken'>
		/// The cancellation token.
		/// </param>
		/// <exception cref="HttpOperationException">
		/// Thrown when the operation returned an invalid status code
		/// </exception>
		/// <exception cref="SerializationException">
		/// Thrown when unable to deserialize the response
		/// </exception>
		/// <exception cref="ValidationException">
		/// Thrown when a required parameter is null
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when a required parameter is null
		/// </exception>
		/// <return>
		/// A response object containing the response body and response headers.
		/// </return>
		public async Task<HttpOperationResponse<ResourceResponse>> UploadWithHttpMessagesAsync (string conversationId, Stream file, string userId = default (string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default (CancellationToken))
		{
			if (conversationId == null)
			{
				throw new ValidationException (ValidationRules.CannotBeNull, "conversationId");
			}
			if (file == null)
			{
				throw new ValidationException (ValidationRules.CannotBeNull, "file");
			}
			// Tracing
			bool _shouldTrace = ServiceClientTracing.IsEnabled;
			string _invocationId = null;
			if (_shouldTrace)
			{
				_invocationId = ServiceClientTracing.NextInvocationId.ToString ();
				Dictionary<string, object> tracingParameters = new Dictionary<string, object> ();
				tracingParameters.Add ("conversationId", conversationId);
				tracingParameters.Add ("userId", userId);
				tracingParameters.Add ("file", file);
				tracingParameters.Add ("cancellationToken", cancellationToken);
				ServiceClientTracing.Enter (_invocationId, this, "Upload", tracingParameters);
			}
			// Construct URL
			var _baseUrl = Client.BaseUri.AbsoluteUri;
			var _url = new System.Uri (new System.Uri (_baseUrl + (_baseUrl.EndsWith ("/") ? "" : "/")), "v3/directline/conversations/{conversationId}/upload").ToString ();
			_url = _url.Replace ("{conversationId}", System.Uri.EscapeDataString (conversationId));
			List<string> _queryParameters = new List<string> ();
			if (userId != null)
			{
				_queryParameters.Add (string.Format ("userId={0}", System.Uri.EscapeDataString (userId)));
			}
			if (_queryParameters.Count > 0)
			{
				_url += "?" + string.Join ("&", _queryParameters);
			}
			// Create HTTP transport objects
			var _httpRequest = new HttpRequestMessage ();
			HttpResponseMessage _httpResponse = null;
			_httpRequest.Method = new HttpMethod ("POST");
			_httpRequest.RequestUri = new System.Uri (_url);
			// Set Headers


			if (customHeaders != null)
			{
				foreach (var _header in customHeaders)
				{
					if (_httpRequest.Headers.Contains (_header.Key))
					{
						_httpRequest.Headers.Remove (_header.Key);
					}
					_httpRequest.Headers.TryAddWithoutValidation (_header.Key, _header.Value);
				}
			}

			// Serialize Request
			string _requestContent = null;
			MultipartFormDataContent _multiPartContent = new MultipartFormDataContent ();
			if (file != null)
			{
				StreamContent _file = new StreamContent (file);
				_file.Headers.ContentType = new MediaTypeHeaderValue ("application/octet-stream");
				FileStream _fileAsFileStream = file as FileStream;
				if (_fileAsFileStream != null)
				{
					ContentDispositionHeaderValue _contentDispositionHeaderValue = new ContentDispositionHeaderValue ("form-data");
					_contentDispositionHeaderValue.Name = "file";
					_contentDispositionHeaderValue.FileName = _fileAsFileStream.Name;
					_file.Headers.ContentDisposition = _contentDispositionHeaderValue;
				}
				_multiPartContent.Add (_file, "file");
			}
			_httpRequest.Content = _multiPartContent;
			// Set Credentials
			if (Client.Credentials != null)
			{
				cancellationToken.ThrowIfCancellationRequested ();
				await Client.Credentials.ProcessHttpRequestAsync (_httpRequest, cancellationToken).ConfigureAwait (false);
			}
			// Send Request
			if (_shouldTrace)
			{
				ServiceClientTracing.SendRequest (_invocationId, _httpRequest);
			}
			cancellationToken.ThrowIfCancellationRequested ();
			_httpResponse = await Client.HttpClient.SendAsync (_httpRequest, cancellationToken).ConfigureAwait (false);
			if (_shouldTrace)
			{
				ServiceClientTracing.ReceiveResponse (_invocationId, _httpResponse);
			}
			HttpStatusCode _statusCode = _httpResponse.StatusCode;
			cancellationToken.ThrowIfCancellationRequested ();
			string _responseContent = null;
			if ((int)_statusCode != 200 && (int)_statusCode != 202 && (int)_statusCode != 204 && (int)_statusCode != 400 && (int)_statusCode != 401 && (int)_statusCode != 403 && (int)_statusCode != 404 && (int)_statusCode != 500 && (int)_statusCode != 502)
			{
				var ex = new HttpOperationException (string.Format ("Operation returned an invalid status code '{0}'", _statusCode));
				if (_httpResponse.Content != null)
				{
					_responseContent = await _httpResponse.Content.ReadAsStringAsync ().ConfigureAwait (false);
				}
				else
				{
					_responseContent = string.Empty;
				}
				ex.Request = new HttpRequestMessageWrapper (_httpRequest, _requestContent);
				ex.Response = new HttpResponseMessageWrapper (_httpResponse, _responseContent);
				if (_shouldTrace)
				{
					ServiceClientTracing.Error (_invocationId, ex);
				}
				_httpRequest.Dispose ();
				if (_httpResponse != null)
				{
					_httpResponse.Dispose ();
				}
				throw ex;
			}
			// Create Result
			var _result = new HttpOperationResponse<ResourceResponse> ();
			_result.Request = _httpRequest;
			_result.Response = _httpResponse;
			// Deserialize Response
			if ((int)_statusCode == 200)
			{
				_responseContent = await _httpResponse.Content.ReadAsStringAsync ().ConfigureAwait (false);
				try
				{
					_result.Body = Microsoft.Rest.Serialization.SafeJsonConvert.DeserializeObject<ResourceResponse> (_responseContent, Client.DeserializationSettings);
				}
				catch (JsonException ex)
				{
					_httpRequest.Dispose ();
					if (_httpResponse != null)
					{
						_httpResponse.Dispose ();
					}
					throw new SerializationException ("Unable to deserialize the response.", _responseContent, ex);
				}
			}
			if (_shouldTrace)
			{
				ServiceClientTracing.Exit (_invocationId, _result);
			}
			return _result;
		}

	}
}
