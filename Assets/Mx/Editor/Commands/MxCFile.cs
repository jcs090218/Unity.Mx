#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Mx
{
    public class MxCFile : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        public override bool Enable() { return true; }

        [Interactive(
            Summary: "Find file externally")]
        public static void FindFileExternal()
        {
            List<string> paths = Directory.GetFiles("Assets", "*.*", SearchOption.AllDirectories)
                .Where(name => !name.EndsWith(".meta"))
                .ToList();

            CompletingRead("Find file: ", paths,
                (answer, summary) =>
                {
                    Application.OpenURL(answer);
                });
        }
    }
}
#endif
