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
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    /// <summary>
    /// Word.
    /// </summary>
    public class Word
    {
        /// <summary>
        /// A word from the custom model's words resource. The spelling of the word is used to train the model.
        /// </summary>
        /// <value>A word from the custom model's words resource. The spelling of the word is used to train the model.</value>
        [JsonProperty("word", NullValueHandling = NullValueHandling.Ignore)]
        public string Word { get; set; }
        /// <summary>
        /// An array of pronunciations for the word. The array can include the sounds-like pronunciation automatically generated by the service if none is provided for the word; the service adds this pronunciation when it finishes processing the word.
        /// </summary>
        /// <value>An array of pronunciations for the word. The array can include the sounds-like pronunciation automatically generated by the service if none is provided for the word; the service adds this pronunciation when it finishes processing the word.</value>
        [JsonProperty("sounds_like", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> SoundsLike { get; set; }
        /// <summary>
        /// The spelling of the word that the service uses to display the word in a transcript. The field contains an empty string if no display-as value is provided for the word, in which case the word is displayed as it is spelled.
        /// </summary>
        /// <value>The spelling of the word that the service uses to display the word in a transcript. The field contains an empty string if no display-as value is provided for the word, in which case the word is displayed as it is spelled.</value>
        [JsonProperty("display_as", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayAs { get; set; }
        /// <summary>
        /// A sum of the number of times the word is found across all corpora. For example, if the word occurs five times in one corpus and seven times in another, its count is `12`. If you add a custom word to a model before it is added by any corpora, the count begins at `1`; if the word is added from a corpus first and later modified, the count reflects only the number of times it is found in corpora.
        /// </summary>
        /// <value>A sum of the number of times the word is found across all corpora. For example, if the word occurs five times in one corpus and seven times in another, its count is `12`. If you add a custom word to a model before it is added by any corpora, the count begins at `1`; if the word is added from a corpus first and later modified, the count reflects only the number of times it is found in corpora.</value>
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long? Count { get; set; }
        /// <summary>
        /// An array of sources that describes how the word was added to the custom model's words resource. For OOV words added from a corpus, includes the name of the corpus; if the word was added by multiple corpora, the names of all corpora are listed. If the word was modified or added by the user directly, the field includes the string `user`.
        /// </summary>
        /// <value>An array of sources that describes how the word was added to the custom model's words resource. For OOV words added from a corpus, includes the name of the corpus; if the word was added by multiple corpora, the names of all corpora are listed. If the word was modified or added by the user directly, the field includes the string `user`.</value>
        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Source { get; set; }
        /// <summary>
        /// If the service discovered one or more problems that you need to correct for the word's definition, an array that describes each of the errors.
        /// </summary>
        /// <value>If the service discovered one or more problems that you need to correct for the word's definition, an array that describes each of the errors.</value>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public List<WordError> Error { get; set; }
    }

}
