// Copyright © 2020 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.
namespace CefSharp.DevTools.CSS
{
    /// <summary>
    /// SetRuleSelectorResponse
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute]
    public class SetRuleSelectorResponse : CefSharp.DevTools.DevToolsDomainResponseBase
    {
        [System.Runtime.Serialization.DataMemberAttribute]
        internal CefSharp.DevTools.CSS.SelectorList selectorList
        {
            get;
            set;
        }

        /// <summary>
        /// selectorList
        /// </summary>
        public CefSharp.DevTools.CSS.SelectorList SelectorList
        {
            get
            {
                return selectorList;
            }
        }
    }
}