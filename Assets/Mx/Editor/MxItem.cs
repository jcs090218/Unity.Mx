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

        private Texture mIcon = null;

        public string summary = null;
        public string tooltip = null;

        public bool enabled = true;

        /* Setter & Getter */

        public Texture texture { get { return this.mIcon; } }

        /* Functions */

        public MxItem(
            string summary = null,
            string tooltip = null,
            Texture icon = null,
            bool enabled = true)
        {
            this.summary = summary;
            this.tooltip = tooltip;
            this.mIcon = icon;
            this.enabled = enabled;
        }
    }
}
