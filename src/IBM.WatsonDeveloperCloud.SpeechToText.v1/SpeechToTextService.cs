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
using System.IO;
using System.Net.Http;
using System.Text;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using Newtonsoft.Json;
using System;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1
{
    public partial class SpeechToTextService : WatsonService, ISpeechToTextService
    {
        const string SERVICE_NAME = "speech_to_text";
        const string URL = "https://stream.watsonplatform.net/speech-to-text/api";
        public SpeechToTextService() : base(SERVICE_NAME, URL)
        {
            if(!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }


        public SpeechToTextService(string userName, string password) : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);

        }

        public SpeechToTextService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public SpeechModel GetModel(string modelId)
        {
            if (string.IsNullOrEmpty(modelId))
                throw new ArgumentNullException(nameof(modelId));
            SpeechModel result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/models/{modelId}");
                result = request.As<SpeechModel>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public SpeechModels ListModels()
        {
            SpeechModels result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/models");
                result = request.As<SpeechModels>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        
        public LanguageModel CreateLanguageModel(string contentType, CreateLanguageModel createLanguageModel)
        {
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException(nameof(contentType));
            if (createLanguageModel == null)
                throw new ArgumentNullException(nameof(createLanguageModel));
            LanguageModel result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations");
                request.WithHeader("Content-Type", contentType);
                request.WithBody<CreateLanguageModel>(createLanguageModel);
                result = request.As<LanguageModel>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteLanguageModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}");
                result = request.As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public LanguageModel GetLanguageModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            LanguageModel result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}");
                result = request.As<LanguageModel>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public LanguageModels ListLanguageModels(string language = null)
        {
            LanguageModels result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations");
                if(!string.IsNullOrEmpty(language))
                    request.WithArgument("language", language);
                result = request.As<LanguageModels>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object ResetLanguageModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/reset");
                result = request.As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object TrainLanguageModel(string customizationId, string wordTypeToAdd = null, double? customizationWeight = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/train");
                if(!string.IsNullOrEmpty(wordTypeToAdd))
                    request.WithArgument("word_type_to_add", wordTypeToAdd);
                if(customizationWeight != null)
                    request.WithArgument("customization_weight", customizationWeight);
                result = request.As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object UpgradeLanguageModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/upgrade_model");
                result = request.As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public object AddCorpus(string customizationId, string corpusName, System.IO.Stream corpusFile, bool? allowOverwrite = null, string corpusFileContentType = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(corpusName))
                throw new ArgumentNullException(nameof(corpusName));
            if (corpusFile == null)
                throw new ArgumentNullException(nameof(corpusFile));
            object result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (corpusFile != null)
                {
                    var corpusFileContent = new ByteArrayContent((corpusFile as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(corpusFileContentType, out contentType);
                    corpusFileContent.Headers.ContentType = contentType;
                    formData.Add(corpusFileContent, "corpus_file", "filename");
                }

                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora/{corpusName}");
                if(allowOverwrite != null)
                    request.WithArgument("allow_overwrite", allowOverwrite);
                request.WithBodyContent(formData);
                result = request.As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteCorpus(string customizationId, string corpusName)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(corpusName))
                throw new ArgumentNullException(nameof(corpusName));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora/{corpusName}");
                result = request.As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Corpus GetCorpus(string customizationId, string corpusName)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(corpusName))
                throw new ArgumentNullException(nameof(corpusName));
            Corpus result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora/{corpusName}");
                result = request.As<Corpus>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Corpora ListCorpora(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            Corpora result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora");
                result = request.As<Corpora>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public object AddWord(string customizationId, string wordName, string contentType, CustomWord customWord)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(wordName))
                throw new ArgumentNullException(nameof(wordName));
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException(nameof(contentType));
            if (customWord == null)
                throw new ArgumentNullException(nameof(customWord));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PutAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{wordName}");
                request.WithHeader("Content-Type", contentType);
                request.WithBody<CustomWord>(customWord);
                result = request.As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object AddWords(string customizationId, string contentType, CustomWords customWords)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException(nameof(contentType));
            if (customWords == null)
                throw new ArgumentNullException(nameof(customWords));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words");
                request.WithHeader("Content-Type", contentType);
                request.WithBody<CustomWords>(customWords);
                result = request.As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteWord(string customizationId, string wordName)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(wordName))
                throw new ArgumentNullException(nameof(wordName));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{wordName}");
                result = request.As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Word GetWord(string customizationId, string wordName)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(wordName))
                throw new ArgumentNullException(nameof(wordName));
            Word result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{wordName}");
                result = request.As<Word>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Words ListWords(string customizationId, string wordType = null, string sort = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            Words result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words");
                if(!string.IsNullOrEmpty(wordType))
                    request.WithArgument("word_type", wordType);
                if(!string.IsNullOrEmpty(sort))
                    request.WithArgument("sort", sort);
                result = request.As<Words>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public AcousticModel CreateAcousticModel(string contentType, CreateAcousticModel createAcousticModel)
        {
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException(nameof(contentType));
            if (createAcousticModel == null)
                throw new ArgumentNullException(nameof(createAcousticModel));
            AcousticModel result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/acoustic_customizations");
                request.WithHeader("Content-Type", contentType);
                request.WithBody<CreateAcousticModel>(createAcousticModel);
                result = request.As<AcousticModel>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteAcousticModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}");
                result = request.As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public AcousticModel GetAcousticModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            AcousticModel result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}");
                result = request.As<AcousticModel>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public AcousticModels ListAcousticModels(string language = null)
        {
            AcousticModels result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/acoustic_customizations");
                if(!string.IsNullOrEmpty(language))
                    request.WithArgument("language", language);
                result = request.As<AcousticModels>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object ResetAcousticModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/reset");
                result = request.As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object TrainAcousticModel(string customizationId, string customLanguageModelId = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/train");
                if(!string.IsNullOrEmpty(customLanguageModelId))
                    request.WithArgument("custom_language_model_id", customLanguageModelId);
                request.WithArgument("force", true);
                result = request.As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object UpgradeAcousticModel(string customizationId, string customLanguageModelId = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/upgrade_model");
                if(!string.IsNullOrEmpty(customLanguageModelId))
                    request.WithArgument("custom_language_model_id", customLanguageModelId);
                result = request.As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public object AddAudio(string customizationId, string audioName, byte[] audioResource, string contentType, string containedContentType = null, bool? allowOverwrite = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(audioName))
                throw new ArgumentNullException(nameof(audioName));
            if (audioResource == null)
                throw new ArgumentNullException(nameof(audioResource));
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException(nameof(contentType));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio/{audioName}");
                request.WithHeader("Content-Type", contentType);
                request.WithHeader("Contained-Content-Type", containedContentType);
                if (allowOverwrite != null)
                    request.WithArgument("allow_overwrite", allowOverwrite);

                var trainingDataContent = new ByteArrayContent(audioResource);
                System.Net.Http.Headers.MediaTypeHeaderValue audioType;
                System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(contentType, out audioType);
                trainingDataContent.Headers.ContentType = audioType;

                request.WithBodyContent(trainingDataContent);
                result = request.As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteAudio(string customizationId, string audioName)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(audioName))
                throw new ArgumentNullException(nameof(audioName));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio/{audioName}");
                result = request.As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public AudioListing GetAudio(string customizationId, string audioName)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(audioName))
                throw new ArgumentNullException(nameof(audioName));
            AudioListing result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio/{audioName}");
                result = request.As<AudioListing>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public AudioResources ListAudio(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            AudioResources result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio");
                result = request.As<AudioResources>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
