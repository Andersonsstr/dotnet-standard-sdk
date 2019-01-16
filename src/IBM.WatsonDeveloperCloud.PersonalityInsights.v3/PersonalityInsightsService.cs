/**
* Copyright 2018 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using System.Collections.Generic;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.Util;
using System;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3
{
    public partial class PersonalityInsightsService : WatsonService, IPersonalityInsightsService
    {
        const string SERVICE_NAME = "personality_insights";
        const string URL = "https://gateway.watsonplatform.net/personality-insights/api";
        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        public PersonalityInsightsService() : base(SERVICE_NAME, URL)
        {
            if(!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public PersonalityInsightsService(string userName, string password, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
            if(string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;
        }

        public PersonalityInsightsService(TokenOptions options, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(options.IamApiKey) && string.IsNullOrEmpty(options.IamAccessToken))
                throw new ArgumentNullException(nameof(options.IamAccessToken) + ", " + nameof(options.IamApiKey));
            if(string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;

            if (!string.IsNullOrEmpty(options.ServiceUrl))
            {
                this.Endpoint = options.ServiceUrl;
            }
            else
            {
                options.ServiceUrl = this.Endpoint;
            }

            _tokenManager = new TokenManager(options);
        }

        public PersonalityInsightsService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        /// <summary>
        /// Get profile.
        ///
        /// Generates a personality profile for the author of the input text. The service accepts a maximum of 20 MB of
        /// input content, but it requires much less text to produce an accurate profile. The service can analyze text
        /// in Arabic, English, Japanese, Korean, or Spanish. It can return its results in a variety of languages.
        ///
        /// **See also:**
        /// * [Requesting a profile](https://cloud.ibm.com/docs/services/personality-insights/input.html)
        /// * [Providing sufficient
        /// input](https://cloud.ibm.com/docs/services/personality-insights/input.html#sufficient)
        ///
        /// ### Content types
        ///
        ///  You can provide input content as plain text (`text/plain`), HTML (`text/html`), or JSON
        /// (`application/json`) by specifying the **Content-Type** parameter. The default is `text/plain`.
        /// * Per the JSON specification, the default character encoding for JSON content is effectively always UTF-8.
        /// * Per the HTTP specification, the default encoding for plain text and HTML is ISO-8859-1 (effectively, the
        /// ASCII character set).
        ///
        /// When specifying a content type of plain text or HTML, include the `charset` parameter to indicate the
        /// character encoding of the input text; for example, `Content-Type: text/plain;charset=utf-8`.
        ///
        /// **See also:** [Specifying request and response
        /// formats](https://cloud.ibm.com/docs/services/personality-insights/input.html#formats)
        ///
        /// ### Accept types
        ///
        ///  You must request a response as JSON (`application/json`) or comma-separated values (`text/csv`) by
        /// specifying the **Accept** parameter. CSV output includes a fixed number of columns. Set the **csv_headers**
        /// parameter to `true` to request optional column headers for CSV output.
        ///
        /// **See also:**
        /// * [Understanding a JSON profile](https://cloud.ibm.com/docs/services/personality-insights/output.html)
        /// * [Understanding a CSV profile](https://cloud.ibm.com/docs/services/personality-insights/output-csv.html).
        /// </summary>
        /// <param name="content">A maximum of 20 MB of content to analyze, though the service requires much less text;
        /// for more information, see [Providing sufficient
        /// input](https://cloud.ibm.com/docs/services/personality-insights/input.html#sufficient). For JSON input,
        /// provide an object of type `Content`.</param>
        /// <param name="contentType">The type of the input. For more information, see **Content types** in the method
        /// description.
        ///
        /// Default: `text/plain`. (optional)</param>
        /// <param name="contentLanguage">The language of the input text for the request: Arabic, English, Japanese,
        /// Korean, or Spanish. Regional variants are treated as their parent language; for example, `en-US` is
        /// interpreted as `en`.
        ///
        /// The effect of the **Content-Language** parameter depends on the **Content-Type** parameter. When
        /// **Content-Type** is `text/plain` or `text/html`, **Content-Language** is the only way to specify the
        /// language. When **Content-Type** is `application/json`, **Content-Language** overrides a language specified
        /// with the `language` parameter of a `ContentItem` object, and content items that specify a different language
        /// are ignored; omit this parameter to base the language on the specification of the content items. You can
        /// specify any combination of languages for **Content-Language** and **Accept-Language**. (optional, default to
        /// en)</param>
        /// <param name="acceptLanguage">The desired language of the response. For two-character arguments, regional
        /// variants are treated as their parent language; for example, `en-US` is interpreted as `en`. You can specify
        /// any combination of languages for the input and response content. (optional, default to en)</param>
        /// <param name="rawScores">Indicates whether a raw score in addition to a normalized percentile is returned for
        /// each characteristic; raw scores are not compared with a sample population. By default, only normalized
        /// percentiles are returned. (optional, default to false)</param>
        /// <param name="csvHeaders">Indicates whether column labels are returned with a CSV response. By default, no
        /// column labels are returned. Applies only when the response type is CSV (`text/csv`). (optional, default to
        /// false)</param>
        /// <param name="consumptionPreferences">Indicates whether consumption preferences are returned with the
        /// results. By default, no consumption preferences are returned. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Profile" />Profile</returns>
        public Profile Profile(Content content, string contentType = null, string contentLanguage = null, string acceptLanguage = null, bool? rawScores = null, bool? csvHeaders = null, bool? consumptionPreferences = null, Dictionary<string, object> customData = null)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Profile result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v3/profile");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(contentType))
                    restRequest.WithHeader("Content-Type", contentType);
                if (!string.IsNullOrEmpty(contentLanguage))
                    restRequest.WithHeader("Content-Language", contentLanguage);
                if (!string.IsNullOrEmpty(acceptLanguage))
                    restRequest.WithHeader("Accept-Language", acceptLanguage);
                if (rawScores != null)
                    restRequest.WithArgument("raw_scores", rawScores);
                if (csvHeaders != null)
                    restRequest.WithArgument("csv_headers", csvHeaders);
                if (consumptionPreferences != null)
                    restRequest.WithArgument("consumption_preferences", consumptionPreferences);
                restRequest.WithBody<Content>(content);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Profile>().Result;
                if(result == null)
                    result = new Profile();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get profile as csv.
        ///
        /// Generates a personality profile for the author of the input text. The service accepts a maximum of 20 MB of
        /// input content, but it requires much less text to produce an accurate profile. The service can analyze text
        /// in Arabic, English, Japanese, Korean, or Spanish. It can return its results in a variety of languages.
        ///
        /// **See also:**
        /// * [Requesting a profile](https://cloud.ibm.com/docs/services/personality-insights/input.html)
        /// * [Providing sufficient
        /// input](https://cloud.ibm.com/docs/services/personality-insights/input.html#sufficient)
        ///
        /// ### Content types
        ///
        ///  You can provide input content as plain text (`text/plain`), HTML (`text/html`), or JSON
        /// (`application/json`) by specifying the **Content-Type** parameter. The default is `text/plain`.
        /// * Per the JSON specification, the default character encoding for JSON content is effectively always UTF-8.
        /// * Per the HTTP specification, the default encoding for plain text and HTML is ISO-8859-1 (effectively, the
        /// ASCII character set).
        ///
        /// When specifying a content type of plain text or HTML, include the `charset` parameter to indicate the
        /// character encoding of the input text; for example, `Content-Type: text/plain;charset=utf-8`.
        ///
        /// **See also:** [Specifying request and response
        /// formats](https://cloud.ibm.com/docs/services/personality-insights/input.html#formats)
        ///
        /// ### Accept types
        ///
        ///  You must request a response as JSON (`application/json`) or comma-separated values (`text/csv`) by
        /// specifying the **Accept** parameter. CSV output includes a fixed number of columns. Set the **csv_headers**
        /// parameter to `true` to request optional column headers for CSV output.
        ///
        /// **See also:**
        /// * [Understanding a JSON profile](https://cloud.ibm.com/docs/services/personality-insights/output.html)
        /// * [Understanding a CSV profile](https://cloud.ibm.com/docs/services/personality-insights/output-csv.html).
        /// </summary>
        /// <param name="content">A maximum of 20 MB of content to analyze, though the service requires much less text;
        /// for more information, see [Providing sufficient
        /// input](https://cloud.ibm.com/docs/services/personality-insights/input.html#sufficient). For JSON input,
        /// provide an object of type `Content`.</param>
        /// <param name="contentType">The type of the input. For more information, see **Content types** in the method
        /// description.
        ///
        /// Default: `text/plain`. (optional)</param>
        /// <param name="contentLanguage">The language of the input text for the request: Arabic, English, Japanese,
        /// Korean, or Spanish. Regional variants are treated as their parent language; for example, `en-US` is
        /// interpreted as `en`.
        ///
        /// The effect of the **Content-Language** parameter depends on the **Content-Type** parameter. When
        /// **Content-Type** is `text/plain` or `text/html`, **Content-Language** is the only way to specify the
        /// language. When **Content-Type** is `application/json`, **Content-Language** overrides a language specified
        /// with the `language` parameter of a `ContentItem` object, and content items that specify a different language
        /// are ignored; omit this parameter to base the language on the specification of the content items. You can
        /// specify any combination of languages for **Content-Language** and **Accept-Language**. (optional, default to
        /// en)</param>
        /// <param name="acceptLanguage">The desired language of the response. For two-character arguments, regional
        /// variants are treated as their parent language; for example, `en-US` is interpreted as `en`. You can specify
        /// any combination of languages for the input and response content. (optional, default to en)</param>
        /// <param name="rawScores">Indicates whether a raw score in addition to a normalized percentile is returned for
        /// each characteristic; raw scores are not compared with a sample population. By default, only normalized
        /// percentiles are returned. (optional, default to false)</param>
        /// <param name="csvHeaders">Indicates whether column labels are returned with a CSV response. By default, no
        /// column labels are returned. Applies only when the response type is CSV (`text/csv`). (optional, default to
        /// false)</param>
        /// <param name="consumptionPreferences">Indicates whether consumption preferences are returned with the
        /// results. By default, no consumption preferences are returned. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="System.IO.FileStream" />System.IO.FileStream</returns>
        public System.IO.MemoryStream ProfileAsCsv(Content content, string contentType = null, string contentLanguage = null, string acceptLanguage = null, bool? rawScores = null, bool? csvHeaders = null, bool? consumptionPreferences = null, Dictionary<string, object> customData = null)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            System.IO.MemoryStream result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v3/profile");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(contentType))
                    restRequest.WithHeader("Content-Type", contentType);
                if (!string.IsNullOrEmpty(contentLanguage))
                    restRequest.WithHeader("Content-Language", contentLanguage);
                if (!string.IsNullOrEmpty(acceptLanguage))
                    restRequest.WithHeader("Accept-Language", acceptLanguage);
                if (rawScores != null)
                    restRequest.WithArgument("raw_scores", rawScores);
                if (csvHeaders != null)
                    restRequest.WithArgument("csv_headers", csvHeaders);
                if (consumptionPreferences != null)
                    restRequest.WithArgument("consumption_preferences", consumptionPreferences);
                restRequest.WithBody<Content>(content);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = new System.IO.MemoryStream(restRequest.AsByteArray().Result);
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
