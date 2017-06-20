using System;

namespace EFFC.ChakraCore.JsRt
{
    /// <summary>
    /// The callback called before collection
    /// </summary>
    /// <param name="callbackState">The state passed to SetBeforeCollectCallback</param>
    public delegate void JsBeforeCollectCallback(IntPtr callbackState);
}