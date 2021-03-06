#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class AvatarModelWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(AvatarModel);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 5, 5);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Awake", _m_Awake);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "updateData", _m_updateData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "configCharater", _m_configCharater);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "configRoleDate", _m_configRoleDate);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetDatas", _g_get_targetDatas);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onUpdatePart", _g_get_onUpdatePart);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onAddNewPart", _g_get_onAddNewPart);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "scriptEnv", _g_get_scriptEnv);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "luaScript", _g_get_luaScript);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetDatas", _s_set_targetDatas);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onUpdatePart", _s_set_onUpdatePart);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onAddNewPart", _s_set_onAddNewPart);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "scriptEnv", _s_set_scriptEnv);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "luaScript", _s_set_luaScript);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new AvatarModel();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to AvatarModel constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Awake(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Awake(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_updateData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _part = LuaAPI.lua_tostring(L, 2);
                    string _num = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.updateData( _part, _num );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_configCharater(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string[] _part = (string[])translator.GetObject(L, 2, typeof(string[]));
                    string[] _num = (string[])translator.GetObject(L, 3, typeof(string[]));
                    
                    gen_to_be_invoked.configCharater( _part, _num );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_configRoleDate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    string _part = LuaAPI.lua_tostring(L, 3);
                    string _num = LuaAPI.lua_tostring(L, 4);
                    
                    gen_to_be_invoked.configRoleDate( _index, _part, _num );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_targetDatas(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.targetDatas);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onUpdatePart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onUpdatePart);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onAddNewPart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onAddNewPart);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_scriptEnv(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.scriptEnv);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_luaScript(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.luaScript);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_targetDatas(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.targetDatas = (string[,])translator.GetObject(L, 2, typeof(string[,]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onUpdatePart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onUpdatePart = translator.GetDelegate<System.Action<string, string, string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onAddNewPart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onAddNewPart = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_scriptEnv(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.scriptEnv = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_luaScript(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.luaScript = (UnityEngine.TextAsset)translator.GetObject(L, 2, typeof(UnityEngine.TextAsset));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
