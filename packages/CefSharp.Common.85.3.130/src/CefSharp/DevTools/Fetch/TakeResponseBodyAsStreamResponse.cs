// Copyright © 2020 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.
namespace CefSharp.DevTools.Fetch
{
    /// <summary>
    /// TakeResponseBodyAsStreamResponse
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute]
    public class TakeResponseBodyAsStreamResponse : CefSharp.DevTools.DevToolsDomainResponseBase
    {
        [System.Runtime.Serialization.DataMemberAttribute]
        internal string stream
        {
            get;
            set;
        }

        /// <summary>
        /// stream
        /// </summary>
        public string Stream
        {
            get
            {
                return stream;
            }
        }
    }
}