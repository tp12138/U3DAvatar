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
    public class AvatarSystemWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(AvatarSystem);
			Utils.BeginObjectRegister(type, L, translator, 0, 14, 8, 8);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadLuaScript", _m_LoadLuaScript);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "initCharacter", _m_initCharacter);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "save", _m_save);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "configCharater", _m_configCharater);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnChangePeople", _m_OnChangePeople);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getAllMeshNameOfPart", _m_getAllMeshNameOfPart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadAssetBundle", _m_LoadAssetBundle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "loadSpriteFromAssetBundle", _m_loadSpriteFromAssetBundle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "loadSourcesFromAssetBundle", _m_loadSourcesFromAssetBundle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "loadAllSourcesFromAssetBundle", _m_loadAllSourcesFromAssetBundle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TypeChange", _m_TypeChange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getDict", _m_getDict);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getBonesFromObj", _m_getBonesFromObj);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "removeMesh", _m_removeMesh);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "skinnedSources", _g_get_skinnedSources);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "target", _g_get_target);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetHips", _g_get_targetHips);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetDatas", _g_get_targetDatas);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetMesh", _g_get_targetMesh);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ab", _g_get_ab);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "prefabsAb", _g_get_prefabsAb);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "sourceSKinnedMesh", _g_get_sourceSKinnedMesh);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "skinnedSources", _s_set_skinnedSources);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "target", _s_set_target);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetHips", _s_set_targetHips);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetDatas", _s_set_targetDatas);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetMesh", _s_set_targetMesh);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ab", _s_set_ab);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "prefabsAb", _s_set_prefabsAb);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "sourceSKinnedMesh", _s_set_sourceSKinnedMesh);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 1, 1);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "_instance", _g_get__instance);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "_instance", _s_set__instance);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new AvatarSystem();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to AvatarSystem constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadLuaScript(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _filename = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.LoadLuaScript( ref _filename );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    LuaAPI.lua_pushstring(L, _filename);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_initCharacter(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.initCharacter(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_save(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _prefabName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.save( _prefabName );
                    
                    
                    
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
            
            
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_OnChangePeople(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _part = LuaAPI.lua_tostring(L, 2);
                    string _num = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.OnChangePeople( _part, _num );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getAllMeshNameOfPart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _partName = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.getAllMeshNameOfPart( _partName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadAssetBundle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _bundleName = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.LoadAssetBundle( _bundleName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_loadSpriteFromAssetBundle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _sourceName = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.loadSpriteFromAssetBundle( _sourceName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_loadSourcesFromAssetBundle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _sourceName = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.loadSourcesFromAssetBundle( _sourceName );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_loadAllSourcesFromAssetBundle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _AssetBundleName = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.loadAllSourcesFromAssetBundle( _AssetBundleName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TypeChange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object __object = translator.GetObject(L, 2, typeof(object));
                    
                        var gen_ret = gen_to_be_invoked.TypeChange( __object );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getDict(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.getDict(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getBonesFromObj(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _tar = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                        var gen_ret = gen_to_be_invoked.getBonesFromObj( _tar );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_removeMesh(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _part = LuaAPI.lua_tostring(L, 2);
                    string _num = LuaAPI.lua_tostring(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.removeMesh( _part, _num );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get__instance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, AvatarSystem._instance);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_skinnedSources(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.skinnedSources);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_target(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.target);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_targetHips(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.targetHips);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_targetDatas(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.targetDatas);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_targetMesh(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.targetMesh);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ab(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ab);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_prefabsAb(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.prefabsAb);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_sourceSKinnedMesh(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.sourceSKinnedMesh);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set__instance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    AvatarSystem._instance = (AvatarSystem)translator.GetObject(L, 1, typeof(AvatarSystem));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_skinnedSources(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.skinnedSources = (System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, UnityEngine.SkinnedMeshRenderer>>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, UnityEngine.SkinnedMeshRenderer>>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_target(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.target = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_targetHips(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.targetHips = (UnityEngine.Transform[])translator.GetObject(L, 2, typeof(UnityEngine.Transform[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_targetDatas(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.targetDatas = (string[,])translator.GetObject(L, 2, typeof(string[,]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_targetMesh(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.targetMesh = (System.Collections.Generic.Dictionary<string, UnityEngine.SkinnedMeshRenderer>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, UnityEngine.SkinnedMeshRenderer>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ab(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ab = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_prefabsAb(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.prefabsAb = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_sourceSKinnedMesh(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarSystem gen_to_be_invoked = (AvatarSystem)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.sourceSKinnedMesh = (System.Collections.Generic.List<UnityEngine.SkinnedMeshRenderer>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.SkinnedMeshRenderer>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
