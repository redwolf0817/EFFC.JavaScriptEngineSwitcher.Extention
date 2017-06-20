using System;

namespace EFFC.ChakraCore.JsRt
{
    /// <summary>
    /// The finalization callback
    /// </summary>
    /// <param name="data">The external data that was passed in when creating the object being finalized</param>
    public delegate void JsObjectFinalizeCallback(IntPtr data);
}