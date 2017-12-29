using EFFC.ChakraCore;
using EFFC.VRoomJs;
using JavaScriptEngineSwitcher.Core;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            JsEngineSwitcher engineSwitcher = JsEngineSwitcher.Instance;
            engineSwitcher.EngineFactories
               .AddEFFCChakraCore()
                .AddEFFCVroom(new EFFCVroomSettings
                {
                    MaxYoungSpaceSize = 4194304,
                    MaxOldSpaceSize = 8388608
                }); ;
            engineSwitcher.DefaultEngineName = EFFCVroomJsEngine.EngineName;
            using (var js = engineSwitcher.CreateDefaultEngine())
            {
                js.Evaluate(@"var out={
$acttype : 'Delete',
$table : 'FunctionInfo',
$where : {
    $or:[{
        FunctionNo:'1'
    },
    {
        ParentNo:'1'
    },
    ]
}
}
");
                var re = js.GetVariableValue("out");
            }

        }
    }
}