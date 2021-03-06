# EFFC.JavaScriptEngineSwitcher.Extention
EFFC.JavaScriptEngineSwitcher.Extention是在JavaScriptEngineSwitcher.Chakra和JavaScriptEngineSwitcher.VroomJs基础上，为了配合EFFC框架的数据结构而做的调整

# 环境编译
项目包含两个sln，一个用于.Net工程（vs2015），一个用于.Net core工程（vs2017），取下代码编译完成后的dll或nuget包都会释放到EFFC.Release目录

# 使用说明
因为是JavaScriptEngineSwitcher的框架，所以直接参考JavaScriptEngineSwitcher的相关说明即可，sample如下
``` C#
using EFFC.ChakraCore;
using EFFC.VRoomJs;

 ...

 JsEngineSwitcher engineSwitcher = JsEngineSwitcher.Instance;

 engineSwitcher.EngineFactories
                .AddEFFCChakraCore()
               .AddEFFCVroom(new EFFCVroomSettings
               {
                  MaxYoungSpaceSize = 4194304,
                  MaxOldSpaceSize = 8388608
             });

switch (name.ToLower())
           {

               case "chakra":
                  engineSwitcher.DefaultEngineName = EFFCChakraCoreJsEngine.EngineName;
                  break;

               case "vroomjs":
                  engineSwitcher.DefaultEngineName = EFFCVroomJsEngine.EngineName;
                    break;
              default:
                   engineSwitcher.DefaultEngineName = EFFCChakraCoreJsEngine.EngineName;
                   break;
          }
          js = engineSwitcher.CreateDefaultEngine();
```

# 历史版本
<ul>
 <li>
  1.0.2:更新JavascriptEngineSwitcher.Chakra到2.4.8</li>
 <li>1.0.3:修正获取数组类型的结果时，数据类型未被转化的问题</li>
 <li>1.0.4:更新JavascriptEngineSwitcher.Chakra到2.4.10，修正获取数据时遇到DateTime进入死循环的bug，改为直接抛出异常，目前chakra不支持js的Date转DateTime的能力</li>
 </ul>
