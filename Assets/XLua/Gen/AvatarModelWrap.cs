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
			Utils.BeginObjectRegister(type, L, translator, 0, 13, 10, 10);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "init", _m_init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "save", _m_save);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getBonesBySmr", _m_getBonesBySmr);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "saveData", _m_saveData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "configCharater", _m_configCharater);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getAllMeshNameOfPart", _m_getAllMeshNameOfPart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadAssetBundle", _m_LoadAssetBundle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "loadAllSourcesFromAssetBundle", _m_loadAllSourcesFromAssetBundle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getSkinnedMeshByPartAndNum", _m_getSkinnedMeshByPartAndNum);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "loadSourcesFromAssetBundle", _m_loadSourcesFromAssetBundle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getBonesFromObj", _m_getBonesFromObj);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TypeChange", _m_TypeChange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "removeSkinnedMesh", _m_removeSkinnedMesh);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "skinnedSourceDict", _g_get_skinnedSourceDict);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "target", _g_get_target);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetHips", _g_get_targetHips);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetDatas", _g_get_targetDatas);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetSkinned", _g_get_targetSkinned);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ab", _g_get_ab);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetHipsDict", _g_get_targetHipsDict);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "sourceSkinnedMesh", _g_get_sourceSkinnedMesh);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onInstancePart", _g_get_onInstancePart);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "deleteSkinnedMesh", _g_get_deleteSkinnedMesh);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "skinnedSourceDict", _s_set_skinnedSourceDict);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "target", _s_set_target);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetHips", _s_set_targetHips);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetDatas", _s_set_targetDatas);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetSkinned", _s_set_targetSkinned);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ab", _s_set_ab);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetHipsDict", _s_set_targetHipsDict);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "sourceSkinnedMesh", _s_set_sourceSkinnedMesh);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onInstancePart", _s_set_onInstancePart);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "deleteSkinnedMesh", _s_set_deleteSkinnedMesh);
            
			
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
        static int _m_init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.init(  );
                    
                    
                    
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
            
            
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_getBonesBySmr(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.SkinnedMeshRenderer _smr = (UnityEngine.SkinnedMeshRenderer)translator.GetObject(L, 2, typeof(UnityEngine.SkinnedMeshRenderer));
                    
                        var gen_ret = gen_to_be_invoked.getBonesBySmr( _smr );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_saveData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _part = LuaAPI.lua_tostring(L, 2);
                    string _num = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.saveData( _part, _num );
                    
                    
                    
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
        static int _m_getAllMeshNameOfPart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_loadAllSourcesFromAssetBundle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_getSkinnedMeshByPartAndNum(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _part = LuaAPI.lua_tostring(L, 2);
                    string _num = LuaAPI.lua_tostring(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.getSkinnedMeshByPartAndNum( _part, _num );
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
            
            
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_getBonesFromObj(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_TypeChange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_removeSkinnedMesh(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _go = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                        var gen_ret = gen_to_be_invoked.removeSkinnedMesh( _go );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_skinnedSourceDict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.skinnedSourceDict);
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
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
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
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
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
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.targetDatas);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_targetSkinned(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.targetSkinned);
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
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ab);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_targetHipsDict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.targetHipsDict);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_sourceSkinnedMesh(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.sourceSkinnedMesh);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onInstancePart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onInstancePart);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_deleteSkinnedMesh(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.deleteSkinnedMesh);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_skinnedSourceDict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.skinnedSourceDict = (System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, UnityEngine.SkinnedMeshRenderer>>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, UnityEngine.SkinnedMeshRenderer>>));
            
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
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
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
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
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
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.targetDatas = (string[,])translator.GetObject(L, 2, typeof(string[,]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_targetSkinned(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.targetSkinned = (System.Collections.Generic.Dictionary<string, UnityEngine.SkinnedMeshRenderer>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, UnityEngine.SkinnedMeshRenderer>));
            
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
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ab = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_targetHipsDict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.targetHipsDict = (System.Collections.Generic.Dictionary<string, UnityEngine.Transform>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, UnityEngine.Transform>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_sourceSkinnedMesh(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.sourceSkinnedMesh = (System.Collections.Generic.List<UnityEngine.SkinnedMeshRenderer>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.SkinnedMeshRenderer>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onInstancePart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onInstancePart = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_deleteSkinnedMesh(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AvatarModel gen_to_be_invoked = (AvatarModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.deleteSkinnedMesh = translator.GetDelegate<System.Action<UnityEngine.GameObject>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
