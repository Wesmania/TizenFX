﻿/// Download progress
///
/// Copyright 2016 by Samsung Electronics, Inc.,
///
/// This software is the confidential and proprietary information
/// of Samsung Electronics, Inc. ("Confidential Information"). You
/// shall not disclose such Confidential Information and shall use
/// it only in accordance with the terms of the license agreement
/// you entered into with Samsung.


using System;

namespace Tizen.Multimedia
{

    /// <summary>
    /// DownloadProgress
    /// </summary>
    /// <remarks>
    /// Contains Download progress. Download object is null initially.
    /// It is created only when playback starts.
    /// </remarks>
    public class DownloadProgress
    {
        internal int _start;
        internal int _current;

        internal DownloadProgress(int start, int current)
        {
            _start = start;
            _current = current;
        }

        /// <summary>
        /// Get Start position in percentage.
        /// </summary>
        /// <value> 0 to 100 </value>
        public int Start
        {
            get
            {
                return _start;
            }
        }

        /// <summary>
        /// Get Current position in percentage.
        /// </summary>
        /// <value> 0 to 100 </value>
        public int Current
        {
            get
            {
                return _current;
            }
        }
    }
}