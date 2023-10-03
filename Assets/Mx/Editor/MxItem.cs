/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System;
using UnityEngine;

namespace Mx
{
    public class MxItem
    {
        /* Variables */

        public static string DEFAULT_ICON = null;

        private Texture mIcon = null;

        private string mSummary = null;
        private string mTooltip = null;

        private bool mEnabled = true;

        /* Setter & Getter */

        public string summary { get { return this.mSummary; } }
        public string tooltip { get { return mTooltip; } }
        public Texture texture { get { return this.mIcon; } }
        public bool Enabled { get { return this.mEnabled; } }

        /* Functions */

        public MxItem(
            string Summary = null,
            string Tooltip = null,
            Texture Icon = null,
            bool Enabled = true)
        {
            this.mSummary = Summary;
            this.mTooltip = Tooltip;
            this.mIcon = Icon;
            this.mEnabled = Enabled;
        }
    }
}
