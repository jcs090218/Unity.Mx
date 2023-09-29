#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System.Collections.Generic;
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

        [Interactive(Summary: "Find the file and open it externally")]
        public static void FindFileExternal()
        {
            List<string> paths = MxUtil.GetFiles("*.*")
                .Where(name => !name.EndsWith(".meta"))
                .ToList();

            CompletingRead("Find file externally: ", paths,
                (answer, summary) =>
                {
                    Application.OpenURL(answer);
                });
        }
    }
}
#endif
